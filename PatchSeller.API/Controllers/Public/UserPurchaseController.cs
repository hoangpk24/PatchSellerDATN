using Hangfire.Server;
using Microsoft.AspNetCore.Mvc;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Public
{
    [Route("/user-purchase")]
    [ApiController]
    public class UserPurchaseController : ControllerBase
    {
        UserPurchaseRepository userPurchaseRepository;
        public UserPurchaseController()
        {
            userPurchaseRepository = new UserPurchaseRepository();
        }

        [HttpGet("get-purchase-by-user-id/{userId}")]
        public async Task<ActionResult<List<UserPurchase>>> GetPurchaseByUserId(int userId)
        {
            try
            {                
                var purchases = await userPurchaseRepository.GetPurchaseByUserId(userId);

                if (purchases == null || !purchases.Any())
                {
                    return NotFound(Constant.ErrorCode.NotFound);
                }

                return Ok(purchases);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("check-patch-purchased/{patchId}")]
        public async Task<ActionResult<bool>> CheckingPatchPurchased(int patchId)
        {
            try
            {

                var userIdClaim = User.FindFirst(ClaimTypes.SerialNumber)?.Value;
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }

                var listRecord = await userPurchaseRepository.GetPurchaseByUserId(int.Parse(userIdClaim));
                if (listRecord == null)
                {
                    return NotFound(Constant.ErrorCode.NotFound);

                }

                var foundRecord = listRecord.FirstOrDefault(x => x.PatchId == patchId);

                if (foundRecord == null)
                {
                    return false;
                }

                if (foundRecord.UserId != int.Parse(userIdClaim))
                {
                    return Forbid();
                }

                return Ok(true);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }
    }
}
