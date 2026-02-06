using Hangfire;
using Hangfire.Server;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Net.payOS.Types;
using PatchSeller.API.DTOs;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Public
{

    [Route("/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        OrderRepository _orderRepository;
        public OrderController()
        {
            _orderRepository = new OrderRepository();
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutDTO>> GetCheckoutUrl([FromBody] CheckoutParamsDTO checkoutParam)
        {

            var userIdClaim = User.FindFirst(ClaimTypes.SerialNumber)?.Value;
            UserRepository userRepository = new UserRepository();
            if (userIdClaim == null)
            {
                return BadRequest("Âu nâu, đăng nhập đi bạn êi");
            }
            DiscountRepository discountRepository = new DiscountRepository();

            PayOS payOS = new PayOS("09b8a42b-6105-4cd4-a4ee-8492e42e909c", "15cfbaf8-79a4-48a0-908f-248c30538001", "00b20c6b94e21bf27e6cb0ae2f26515637c93d70b2eeb832e7b51e299cba433d");
            List<ItemData> items = new List<ItemData>();
            foreach (var product in checkoutParam.ListItemCheckout)
            {
                ItemData item = new ItemData(product.PatchName, 1, (int)product.Price);
                items.Add(item);
            }

            double totalPrice = checkoutParam.ListItemCheckout.Sum(p => p.Price);
            double finalAmount = totalPrice;
            if (checkoutParam.DiscountApplydId>0)
            {
                finalAmount -= checkoutParam.DiscountAmount;
            }
            if(checkoutParam.UsedRewardPoint>0)
            {
                var user = await userRepository.GetById(int.Parse(userIdClaim));
                if(user != null)
                {
                    if(user.RewardPoint >= checkoutParam.UsedRewardPoint)
                    {
                        finalAmount -= (int)checkoutParam.UsedRewardPoint;
                        user.RewardPoint -= checkoutParam.UsedRewardPoint;
                        await userRepository.Update(user);
                    }
                    else
                    {
                        return BadRequest(Constant.ErrorCode.NotEnoughRewardPoint);
                    }
                }
            }
           

            int ordCode = new Random().Next(1, int.MaxValue);

            Order tempOrder = new Order();
            tempOrder.OrderCode = "INVP" + ordCode.ToString();
            tempOrder.PaymentStatus = Constant.PaymentStatus.WaitingForPayment;
            tempOrder.OrderDate = DateTime.Now;
            tempOrder.UsedRewardPoint = checkoutParam.UsedRewardPoint;
            tempOrder.TotalAmount = totalPrice;
            tempOrder.FinalAmount = finalAmount;
            tempOrder.DiscountAmount = checkoutParam.DiscountAmount;
            tempOrder.Status = Constant.OrderStatus.OrderWaitingForPayment;
            tempOrder.UserId = int.Parse(userIdClaim);
            tempOrder.DiscountId = checkoutParam.DiscountApplydId;
            tempOrder.Note = "";
            tempOrder.PaymentLink = "";

            var result = await _orderRepository.Create(tempOrder);
            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                OrderDetailRepository orderDetailRepository = new OrderDetailRepository();
                foreach (var patch in checkoutParam.ListItemCheckout)
                {
                    OrderDetail orderItem = new OrderDetail();
                    orderItem.OrderId = result.OrderId;
                    orderItem.PatchId = patch.PatchId;
                    orderItem.Price = patch.Price;
                    var resultItem = await orderDetailRepository.Create(orderItem);
                    if (resultItem == null)
                    {
                        return BadRequest();
                    }
                }



                if (checkoutParam.DiscountApplydId != null && checkoutParam.DiscountApplydId > 0)
                {
                    var discountCode = await discountRepository.GetById((int)checkoutParam.DiscountApplydId);
                    if (discountCode != null)
                    {
                        discountCode.UsedCount++;
                        var resultUpdate = await discountRepository.Update(discountCode);
                        if (resultUpdate == null)
                        {
                            return BadRequest(Constant.ErrorCode.DatabaseError);
                        }

                    }
                }
                    DateTimeOffset utcNow = DateTimeOffset.UtcNow;
                    DateTimeOffset expirationTime = utcNow.AddMinutes(15);
                    long expiredAt = expirationTime.ToUnixTimeSeconds();
                    PaymentData paymentData = new PaymentData(ordCode, (int)finalAmount, "ITeam Thanh toán", items, "http://localhost:5001/order/payment-cancelled?order-id=" + tempOrder.OrderId, "http://localhost:5001/order/payment-success?order-id=" + tempOrder.OrderId, null, null, null, null, null, expiredAt);

                    CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);

                    if (createPayment.status == "PENDING")
                    {                       
                    tempOrder.PaymentExpiration = DateTime.Now.AddMinutes(15);
                    tempOrder.PaymentLink = createPayment.checkoutUrl;
                    await _orderRepository.Update(tempOrder);
                    CheckoutDTO checkoutDTO = new CheckoutDTO();
                    checkoutDTO.OrderCode = tempOrder.OrderCode;
                    checkoutDTO.OrderId = tempOrder.OrderId;
                    checkoutDTO.PaymentLink = createPayment.checkoutUrl;                 
                    return Ok(checkoutDTO);
                    }
                    else
                    {
                        return BadRequest();
                    }               

            }


        }

        [HttpPut("payment-cancelled")]
         public async Task<ActionResult<Order>> PaymentCanceled([FromQuery] int orderId)
         {
            try
            {
                DiscountRepository discountRepository = new DiscountRepository();
                var order = await _orderRepository.GetById(orderId);
                if(order == null)
                {
                    return BadRequest(Constant.ErrorCode.NotFound);
                }
                order.PaymentStatus = Constant.PaymentStatus.PaymentCanceled;
                order.Status = Constant.OrderStatus.OrderCanceled;                
                await _orderRepository.Update(order);

                if(order.DiscountId!=null && order.DiscountId>0)
                {
                    var discount = await discountRepository.GetById((int)order.DiscountId);
                    if(discount != null)
                    {
                        discount.UsedCount--;
                        await discountRepository.Update(discount);
                    }
                }

                if(order.UsedRewardPoint>0)
                {
                    UserRepository userRepository = new UserRepository();
                    var user = await userRepository.GetById(order.UserId);
                    if(user != null)
                    {
                        user.RewardPoint += order.UsedRewardPoint;
                        await userRepository.Update(user);
                    }
                }
                await _orderRepository.Update(order);
                return Ok(order);
            }
            catch (Exception)
            {
                return BadRequest(Constant.ErrorCode.DatabaseError);
            }
        }

        [HttpPut("payment-success")]
        public async Task<ActionResult<Order>> PaymentSuccess([FromQuery] int orderId)
        {
            try
            {
                OrderDetailRepository orderDetailRepository = new OrderDetailRepository();
                var order = await _orderRepository.GetById(orderId);
                if(order == null)
                {
                    return BadRequest(Constant.ErrorCode.NotFound);
                }
                order.PaymentStatus = Constant.PaymentStatus.PaymentCompleted;
                order.Status = Constant.OrderStatus.OrderDone;
                await _orderRepository.Update(order);

                UserPurchaseRepository userPurchaseRepository = new UserPurchaseRepository();
                List<OrderDetail> listOrderDetail = await orderDetailRepository.GetByOrderId(orderId);
                foreach (var orderDetail in listOrderDetail)
                {
                    UserPurchase userPurchase = new UserPurchase();
                    userPurchase.UserId = order.UserId;
                    userPurchase.PatchId = orderDetail.PatchId;
                    userPurchase.PurchasedAt = DateTime.Now;
                    await userPurchaseRepository.Create(userPurchase);
                }
                return Ok(order);
            }
            catch (Exception)
            {
                return BadRequest(Constant.ErrorCode.OtherError);
            }
        }
    }
}
