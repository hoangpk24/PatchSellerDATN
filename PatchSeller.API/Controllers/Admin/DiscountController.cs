using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using Pro219.API.DTOs;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Admin
{
    [Route("admin/discount")]
    [ApiController]

    public class DiscountController : ControllerBase
    {
        DiscountRepository _discountRepository;

        public DiscountController()
        {
            _discountRepository = new DiscountRepository();
        }

        [HttpGet("get-all-discounts")]
        public async Task<ActionResult<List<Discount>>> GetAllDiscounts(
            [FromQuery] string? keyword = null,
            [FromQuery] string? discountType = null,
            [FromQuery]  int? rankId = 0, 
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            try
            {
                List<Discount> result = await _discountRepository.GetAll(keyword, discountType, rankId, startDate, endDate);
                if (result == null)
                {
                    return NoContent();
                }

                if(result.Any())
                {
                    result = result.OrderByDescending(c => c.CreatedAt).ToList();
                }    
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("get-discount-by-id/{id}")]
        public async Task<ActionResult<Discount>> GetDiscountById(int id)
        {
            try
            {
                var result = await _discountRepository.GetById(id);
                if (result == null)
                {
                    return NotFound(Constant.ErrorCode.NotFound);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<Discount>> CreateDiscount([FromBody] Discount discount)
        {
            try
            {
                if (discount == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                discount.Status = 1;
                discount.Delete = false;
                var result = await _discountRepository.Create(discount);

                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (InvalidOperationException ex) when (ex.Message == "DUPLICATE_CODE")
            {
                return BadRequest(Constant.ErrorCode.CodeAlreadyExit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<Discount>> UpdateDiscount([FromBody] Discount discount)
        {
            try
            {
                if (discount == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var newDiscount = new Discount
                {
                    DiscountId = discount.DiscountId,
                    Code = discount.Code,
                    DiscountType = discount.DiscountType,
                    Value = discount.Value,
                    MaxDiscount = discount.MaxDiscount,
                    MinOrderValue = discount.MinOrderValue,
                    UsageLimit = discount.UsageLimit,
                    LimitPerUser = discount.LimitPerUser,
                    UsedCount = discount.UsedCount,
                    StartDate = discount.StartDate,
                    EndDate = discount.EndDate,
                    Status = discount.Status,
                    Delete = discount.Delete,
                    RankId = discount.RankId
                };

                var result = await _discountRepository.Update(discount);

                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (InvalidOperationException ex) when (ex.Message == "DUPLICATE_CODE")
            {
                return BadRequest(Constant.ErrorCode.CodeAlreadyExit);
            }
            catch (InvalidOperationException ex) when (ex.Message == "NOT_FOUND")
            {
                return BadRequest(Constant.ErrorCode.NotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<bool>> DeleteDiscount(int id)
        {
            try
            {
                var result = await _discountRepository.Delete(id);

                if (!result)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }
                return Ok(true);
            }
            catch (InvalidOperationException ex) when (ex.Message == "NOT_FOUND")
            {
                return BadRequest(Constant.ErrorCode.NotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }
    }
}
