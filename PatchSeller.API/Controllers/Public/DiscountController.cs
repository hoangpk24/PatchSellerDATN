using Microsoft.AspNetCore.Mvc;
using PatchSeller.DAL.Repository;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Public
{
    [Route("/discount")]
    [ApiController]
    public class DiscountController : ControllerBase
    {

        DiscountRepository discountCodeRepository;
        public DiscountController()
        {
            discountCodeRepository = new DiscountRepository();
        }

        [HttpGet("ApplyDiscountCodeValue")]
        public async Task<ActionResult<decimal>> ApplyDiscountCodeValue(string code, double totalAmount)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.SerialNumber)?.Value;

                if (userIdClaim == null)
                {
                    return BadRequest("Chỉ áp dụng cho khách hàng đã đăng nhập");
                }

                var discountCode = await discountCodeRepository.GetDiscountCodeByCode(code);
                if (discountCode == null)
                {
                    return NotFound(Constant.ErrorCode.DataNotFound);
                }

                int userTimeUsed = await discountCodeRepository.GetUserTimeUsed(code, int.Parse(userIdClaim));

                if (discountCode.LimitPerUser <= userTimeUsed)
                {
                    return BadRequest("Mã giảm giá không thể sử dụng nữa");
                }
                else if (discountCode.UsedCount >= discountCode.UsageLimit)
                {
                    return BadRequest("Mã giảm giá đã hết lượt sử dụng");
                }

                if (discountCode.StartDate > DateTime.Now)
                {
                    return BadRequest("Mã giảm giá chưa khả dụng");
                }
                if (discountCode.EndDate < DateTime.Now)
                {
                    return BadRequest("Mã giảm giá đã hết hạn");
                }
                if (discountCode.Status !=1 )
                {
                    return BadRequest("Mã giảm giá chưa khả dụng");
                }
                if (discountCode.UsedCount >= discountCode.UsageLimit)
                {
                    return BadRequest("Mã giảm giá đã hết lượt sử dụng");

                }
                if (discountCode.MinOrderValue != null && totalAmount < discountCode.MinOrderValue)
                {
                    return BadRequest("Đơn hàng không đủ giá trị để sử dụng mã giảm giá");
                }

               

                if (discountCode.DiscountType == Constant.DiscountType.Percent) // Percent
                {

                  
                        var discount = totalAmount * (discountCode.Value / 100);
                        if (discountCode.MaxDiscount != null && discountCode.MaxDiscount < discount)
                        {
                            discount = discountCode.MaxDiscount ?? discount;
                        }
                       double finalDiscount = discount > totalAmount ? totalAmount : discount;
                   
                   
                    return Ok(finalDiscount);
                }
                else if (discountCode.DiscountType == Constant.DiscountType.Fixed) // Fixed Amount
                {


                    double finalDiscount = discountCode.Value > totalAmount ? totalAmount : discountCode.Value;
                                  
                    return Ok(finalDiscount);
                }
                else
                {
                    return BadRequest("Mã giảm giá không hợp lệ");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }
    }
}
