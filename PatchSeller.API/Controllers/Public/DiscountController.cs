using Microsoft.AspNetCore.Mvc;
using PatchSeller.API.DTOs;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Public
{
    [Route("/discount")]
    [ApiController]
    public class DiscountController : ControllerBase
    {

        DiscountRepository discountCodeRepository;
        RankRepository rankRepository;

        public DiscountController()
        {
            discountCodeRepository = new DiscountRepository();
            rankRepository = new RankRepository();
        }

        [HttpGet("ApplyDiscountCodeValue")]
        public async Task<ActionResult<DiscountCodeDTO>> ApplyDiscountCodeValue(string code, double totalAmount)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.SerialNumber)?.Value;

                if (userIdClaim == null)
                {
                    return BadRequest("Chỉ áp dụng cho khách hàng đã đăng nhập");
                }
                UserRepository userRepository = new UserRepository();
                var user = await userRepository.GetById(int.Parse(userIdClaim));
                if (user == null)
                {
                    return BadRequest("Khách hàng không tồn tại");
                }
           

                var discountCode = await discountCodeRepository.GetDiscountCodeByCode(code);
                if (discountCode == null)
                {
                    return NotFound(Constant.ErrorCode.DataNotFound);
                }

                if (discountCode.RankId != null && discountCode.RankId >user.RankId)
                {
                    return BadRequest("Mã giảm giá không áp dụng cho rank này");
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

                    DiscountCodeDTO result = new DiscountCodeDTO
                    {
                        Amount = finalDiscount,
                        Code = discountCode.Code,
                        Id = discountCode.DiscountId
                    };

                    return Ok(result);
                }
                else if (discountCode.DiscountType == Constant.DiscountType.Fixed) // Fixed Amount
                {


                    double finalDiscount = discountCode.Value > totalAmount ? totalAmount : discountCode.Value;

                    DiscountCodeDTO result = new DiscountCodeDTO
                    {
                        Amount = finalDiscount,
                        Code = discountCode.Code,
                        Id = discountCode.DiscountId
                    };

                    return Ok(result);
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
        [HttpGet("discount-available")]
        public async Task<ActionResult<List<Discount>>> GetDiscountsAvailableBaseOnUser()
        {

            var userIdClaim = User.FindFirst(ClaimTypes.SerialNumber)?.Value;

            if (userIdClaim == null)
            {
                return BadRequest(Constant.ErrorCode.DataNotFound);
            }
            UserRepository userRepository = new UserRepository();
            var user = await userRepository.GetById(int.Parse(userIdClaim));
            if (user == null)
            {
                return BadRequest(Constant.ErrorCode.DataNotFound);
            }
            var rankByUser = await rankRepository.GetById(user.RankId ?? -1);

            if(rankByUser == null)
            {     
                return BadRequest(Constant.ErrorCode.DataNotFound);
            }

            DiscountRepository discountRepository = new DiscountRepository();
            var listDiscount = await discountCodeRepository.GetAllListDiscount();
            listDiscount = listDiscount.Where(x => x.RankId == null || (x.RankId != null && x.Rank != null && x.Rank.MiniumSpend <= rankByUser.MiniumSpend)).ToList();
            return Ok(listDiscount);
        }
    }
}
