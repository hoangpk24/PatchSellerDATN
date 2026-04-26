using Microsoft.AspNetCore.Mvc;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using Pro219.API.DTOs;

namespace PatchSeller.API.Controllers.Admin
{
    [Route("admin/page-permission")]
    [ApiController]
    public class PagePermissionController : ControllerBase
    {
        private readonly PagePermissionRepository _pagePermissionRepository;

        public PagePermissionController()
        {
            _pagePermissionRepository = new PagePermissionRepository();
        }

        [HttpGet("get-all-page-permissions")]
        public async Task<ActionResult<List<PagePermission>>> GetAllPagePermissions(string? keyword)
        {
            try
            {
                var result = await _pagePermissionRepository.GetAll(keyword);
                if (result == null)
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("get-page-permission-by-id/{id}")]
        public async Task<ActionResult<PagePermission>> GetPagePermissionById(int id)
        {
            try
            {
                var result = await _pagePermissionRepository.GetById(id);
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

        [HttpPost("create")]
        public async Task<ActionResult<PagePermission>> CreatePagePermission([FromBody] PagePermission pagePermission)
        {
            try
            {
                if (pagePermission == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var result = await _pagePermissionRepository.Create(pagePermission);
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
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<PagePermission>> UpdatePagePermission([FromBody] PagePermission pagePermission)
        {
            try
            {
                if (pagePermission == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var result = await _pagePermissionRepository.Update(pagePermission);
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
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<bool>> DeletePagePermission(int id)
        {
            try
            {
                var result = await _pagePermissionRepository.Delete(id);
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
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }
    }
}
