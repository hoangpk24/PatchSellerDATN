using Microsoft.AspNetCore.Mvc;
using PatchSeller.API.DTOs;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;

namespace PatchSeller.API.Controllers.Admin
{
    [Route("/admin/review")]
    [ApiController]

    public class ReviewController : ControllerBase
    {
        ReviewRepository _reviewRepository;

        public ReviewController()
        {
            _reviewRepository = new ReviewRepository();
        }

        private static ReviewDetailDTO MapToReviewDetail(Review review)
        {
            if (review == null) return null;

            return new ReviewDetailDTO
            {
                ReviewId = review.ReviewId,
                Content = review.Content,
                CreatedAt = review.CreatedAt,
                Overall = review.Overall,
                PatchId = review.PatchId,
                PatchName = review.Patch.Name,
                Status = review.Status,
                Title = review.Title,
                UserId = review.UserId,
                UserName = review.UserName,
            };
        }

        [HttpGet("get-all-review")]
        public async Task<ActionResult<List<Review>>> GetAllReview(string? keyword = null)
        {
            try
            {
                List<Review> result = await _reviewRepository.GetAllDetail(keyword);

                if (result.Any())
                {
                    result = result.OrderByDescending(c => c.CreatedAt).ToList();
                }

                var dto = result.Select(MapToReviewDetail).ToList();
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<Review>> Update(ReviewCreateUpdateDTO payload)
        {
            try
            {
                if (payload == null) return BadRequest();

                var review = new Review()
                {
                    ReviewId = payload.ReviewId,
                    Content = payload.Content,
                    Overall = payload.Overall,
                    PatchId = payload.PatchId,
                    Title = payload.Title,
                    UserId = payload.UserId,
                    UserName = payload.UserName,
                    Status = payload.Status,
                };

                var result = await _reviewRepository.Update(review);

                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.OtherError);
                }

                return Ok(result);
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
