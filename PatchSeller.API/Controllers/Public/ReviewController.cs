using Microsoft.AspNetCore.Mvc;
using PatchSeller.API.DTOs;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;

namespace PatchSeller.API.Controllers.Public
{
    [Route("/review")]
    [ApiController]

    public class ReviewController : ControllerBase
    {
        ReviewRepository _reviewRepository;

        public ReviewController()
        {
            _reviewRepository = new ReviewRepository();
        }

        [HttpGet("get-all-review")]
        public async Task<ActionResult<List<Review>>> GetAllReview()
        {
            try
            {
                List<Review> result = await _reviewRepository.GetAll();

                if (result.Any())
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

        [HttpGet("get-all-review-by-patch-id/{patchId}")]
        public async Task<ActionResult<List<Review>>> GetAllReview(int patchId)
        {
            try
            {
                List<Review> result = await _reviewRepository.GetAllByPatchId(patchId);

                if (result.Any())
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

        [HttpPost("create")]
        public async Task<ActionResult<Review>> Create(ReviewCreateUpdateDTO payload)
        {
            try
            {
                if(payload == null) return BadRequest();

                var review = new Review() { 
                    Content = payload.Content,
                    Overall = payload.Overall,
                    PatchId = payload.PatchId,
                    Title = payload.Title,
                    UserId = payload.UserId,
                    UserName = payload.UserName,
                };

                var result = await _reviewRepository.Create(review);

                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.OtherError);
                }

                return Ok(result);
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
                    Content = payload.Content,
                    Overall = payload.Overall,
                    PatchId = payload.PatchId,
                    Title = payload.Title,
                    UserId = payload.UserId,
                    UserName = payload.UserName,
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

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var result = await _reviewRepository.Delete(id);

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
