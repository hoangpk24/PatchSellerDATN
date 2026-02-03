using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using Pro219.API.DTOs;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Admin
{
    [Route("admin/platform")]
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

        [HttpGet("get-platform-by-id/{id}")]
        public async Task<ActionResult<Platform>> GetCategoryById(int id)
        {
            try
            {
                var result = await _platformRepository.GetById(id);
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
        public async Task<ActionResult<Platform>> CreatePlatform([FromBody] Platform platform)
        {
            try
            {
                if (platform == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                platform.Status = 1;
                platform.Delete = false;
                var result = await _platformRepository.Create(platform);

                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (InvalidOperationException ex) when (ex.Message == "DUPLICATE_NAME")
            {
                return BadRequest(Constant.ErrorCode.NameAlreadyExit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<Platform>> UpdatePlatform([FromBody] Platform platform)
        {
            try
            {
                if (platform == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var newPlatform = new Platform
                {
                    PlatformId = platform.PlatformId,
                    Name = platform.Name,
                    Description = platform.Description,
                    Status = platform.Status,
                    Delete = platform.Delete
                };

                var result = await _platformRepository.Update(platform);

                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (InvalidOperationException ex) when (ex.Message == "DUPLICATE_NAME")
            {
                return BadRequest(Constant.ErrorCode.NameAlreadyExit);
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
        public async Task<ActionResult<bool>> DeletePlatform(int id)
        {
            try
            {
                var result = await _platformRepository.Delete(id);

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
