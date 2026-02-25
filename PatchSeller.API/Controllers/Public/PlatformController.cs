using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using Pro219.API.DTOs;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Public
{
    [Route("/platform")]
    [ApiController]

    public class PlatformController : ControllerBase
    {
        PlatformRepository _platformRepository;

        public PlatformController()
        {
            _platformRepository = new PlatformRepository();
        }

        [HttpGet("get-all-platforms")]
        public async Task<ActionResult<List<Platform>>> GetAllPlatform(string? keyword)
        {
            try
            {
                List<Platform> result = await _platformRepository.GetAll(keyword);
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
    }
}
