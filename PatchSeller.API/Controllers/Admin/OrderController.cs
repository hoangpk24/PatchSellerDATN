using Hangfire;
using Hangfire.Server;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Net.payOS.Types;
using PatchSeller.API.DTOs;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Admin
{

    [Route("admin/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        OrderRepository _orderRepository;
        public OrderController()
        {
            _orderRepository = new OrderRepository();
        }

        private static OrderDetailResponseDTO MapToOrderDetailResponse(Order order)
        {
            if (order == null) return null!;
            return new OrderDetailResponseDTO
            {
                OrderId = order.OrderId,
                PaymentStatus = order.PaymentStatus ?? string.Empty,
                OrderCode = order.OrderCode ?? string.Empty,
                OrderDate = order.OrderDate,
                UsedRewardPoint = order.UsedRewardPoint,
                TotalAmount = order.TotalAmount,
                DiscountAmount = order.DiscountAmount,
                FinalAmount = order.FinalAmount,
                PaymentLink = order.PaymentLink,
                PaymentExpiration = order.PaymentExpiration,
                Note = order.Note,
                Status = order.Status,
                UserId = order.UserId,
                DiscountId = order.DiscountId,
                OrderDetails = order.OrderDetails?
                    .Select(od => new OrderDetailItemDTO
                    {
                        OrderDetailId = od.OrderDetailID,
                        OrderId = od.OrderId,
                        PatchId = od.PatchId,
                        GameId = od.Patch?.GameId ?? 0,
                        GameName = od.Patch?.Game?.Title ?? string.Empty,
                        PatchName = od.Patch?.Name ?? string.Empty,
                        Price = od.Price,
                        GameImages = od.Patch?.Game?.GameImages?
                            .Where(gi => gi.Delete != true)
                            .Select(gi => new GameImageBasicDTO
                            {
                                GameImageId = gi.GameImageId,
                                URL = gi.URL,
                                Name = gi.Name,
                                Description = gi.Description,
                                Status = gi.Status
                            }).ToList() ?? new List<GameImageBasicDTO>(),
                        PatchImages = od.Patch?.PatchImages?
                            .Where(pi => pi.Delete != true)
                            .Select(pi => new PatchImageBasicDTO
                            {
                                PatchImageId = pi.PatchImageId,
                                URL = pi.URL,
                                Name = pi.Name,
                                Description = pi.Description,
                                IsThumbnail = pi.IsThumbnail
                            }).ToList() ?? new List<PatchImageBasicDTO>()
                    }).ToList() ?? new List<OrderDetailItemDTO>()
            };
        }

        [HttpGet("get-order-detail/{orderId}")]
        public async Task<ActionResult<OrderDetailResponseDTO>> GetOrderDetail(int orderId)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.SerialNumber)?.Value;
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }
                else
                {
                    StaffRepository staffRepository = new StaffRepository();

                    var user = staffRepository.GetById(int.Parse(userIdClaim));
                    if (user == null)
                    {
                        return Unauthorized();
                    }
                }

                var order = await _orderRepository.GetByIdWithDetails(orderId);
                if (order == null)
                {
                    return NotFound(Constant.ErrorCode.NotFound);
                }
               
                return Ok(MapToOrderDetailResponse(order));
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<OrderDetailResponseDTO>>> GetAllOrder()
        {

            var userIdClaim = User.FindFirst(ClaimTypes.SerialNumber)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            else
            {
                StaffRepository staffRepository = new StaffRepository();

                var user = staffRepository.GetById(int.Parse(userIdClaim));
                if (user == null)
                {
                    return Unauthorized();
                }
            }
            var orders = await _orderRepository.GetAll();
            List<OrderDetailResponseDTO> listOrderDetail = new List<OrderDetailResponseDTO>();
            foreach (var order in orders)
            {
                var obj = await _orderRepository.GetByIdWithDetails(order.OrderId);
                listOrderDetail.Add(MapToOrderDetailResponse(obj));
            }
            return Ok(listOrderDetail);
        }



    }
}
