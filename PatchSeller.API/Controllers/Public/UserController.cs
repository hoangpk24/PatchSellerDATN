using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatchSeller.API.DTOs;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Public
{
    [Route("user")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly CartRepository _cartRepository;
        private readonly RankRepository _rankRepository;
        private readonly OrderRepository _orderRepository;

        public UserController()
        {
            _userRepository = new UserRepository();
            _cartRepository = new CartRepository();
            _rankRepository = new RankRepository();
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
                        GameThumbnail = od.Patch?.Game?.Thumbnail ?? string.Empty,
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

        [HttpGet("get-user-by-id/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                var result = await _userRepository.GetById(id);
                if (result == null)
                {
                    return NotFound(Constant.ErrorCode.NotFound);
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("get-me-detail/{userId}")]
        public async Task<ActionResult<UserMeDetailDTO>> GetMeDetail(int userId)
        {
            try
            {
                var user = await _userRepository.GetById(userId);
                if (user == null)
                {
                    return NotFound(Constant.ErrorCode.NotFound);
                }

                Rank? rank = null;
                if (user.RankId.HasValue)
                {
                    rank = await _rankRepository.GetById(user.RankId.Value);
                }

                var orders = await _orderRepository.GetAllByUserId(userId) ?? new List<Order>();

                var orderDtos = new List<OrderDetailResponseDTO>();
                var purchasedPatches = new List<UserPurchasePatchDTO>();
                double totalSpent = 0;

                foreach (var order in orders.OrderByDescending(o => o.OrderDate))
                {
                    var fullOrder = await _orderRepository.GetByIdWithDetails(order.OrderId);
                    if (fullOrder == null) continue;

                    orderDtos.Add(MapToOrderDetailResponse(fullOrder));

                    if (fullOrder.Status == Constant.OrderStatus.OrderDone &&
                        fullOrder.PaymentStatus == Constant.PaymentStatus.PaymentCompleted)
                    {
                        totalSpent += fullOrder.FinalAmount;

                        if (fullOrder.OrderDetails != null)
                        {
                            foreach (var od in fullOrder.OrderDetails)
                            {
                                if (od.Patch != null &&
                                    od.Patch.Delete != true &&
                                    od.Patch.Game != null &&
                                    od.Patch.Game.Delete != true)
                                {
                                    purchasedPatches.Add(new UserPurchasePatchDTO
                                    {
                                        PatchId = od.PatchId,
                                        GameId = od.Patch.GameId,
                                        GameName = od.Patch.Game.Title,
                                        PatchName = od.Patch.Name,
                                        PurchasedAt = fullOrder.OrderDate
                                    });
                                }
                            }
                        }
                    }
                }

                var response = new UserMeDetailDTO
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    RewardPoint = user.RewardPoint,
                    RankId = user.RankId,
                    RankName = rank?.RankName ?? string.Empty,
                    TotalSpent = totalSpent,
                    Orders = orderDtos,
                    PurchasedPatches = purchasedPatches
                };

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }
    }
}
