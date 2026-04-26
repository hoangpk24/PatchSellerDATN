using Microsoft.AspNetCore.Mvc;
using PatchSeller.API.DTOs;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using Pro219.API.DTOs;

namespace PatchSeller.API.Controllers.Admin
{
    [Route("admin/staff-page-permission")]
    [ApiController]
    public class StaffPagePermissionController : ControllerBase
    {
        private readonly StaffPagePermissionRepository _staffPagePermissionRepository;

        public StaffPagePermissionController()
        {
            _staffPagePermissionRepository = new StaffPagePermissionRepository();
        }

        [HttpGet("get-all-staff-page-permissions")]
        public async Task<ActionResult<List<StaffPagePermission>>> GetAllStaffPagePermissions()
        {
            try
            {
                var result = await _staffPagePermissionRepository.GetAll();
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

        [HttpGet("get-all-by-staff-id/{staffId}")]
        public async Task<ActionResult<List<StaffPagePermission>>> GetAllByStaffId(int staffId)
        {
            try
            {
                var result = await _staffPagePermissionRepository.GetAllByStaffId(staffId);
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

        [HttpGet("get-staff-page-permission-by-id/{id}")]
        public async Task<ActionResult<StaffPagePermission>> GetStaffPagePermissionById(int id)
        {
            try
            {
                var result = await _staffPagePermissionRepository.GetById(id);
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
        public async Task<ActionResult<StaffPagePermission>> CreateStaffPagePermission([FromBody] StaffPagePermissionCreateDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }
                if (dto.StaffId <= 0 || string.IsNullOrWhiteSpace(dto.PageCode) || string.IsNullOrWhiteSpace(dto.PermissionCode))
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var result = await _staffPagePermissionRepository.Create(dto.StaffId, dto.PageCode, dto.PermissionCode);
                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (InvalidOperationException ex) when (ex.Message == "DUPLICATE_DATA")
            {
                return BadRequest(Constant.ErrorCode.DataAlreadyExit);
            }
            catch (InvalidOperationException ex) when (ex.Message == "PAGE_CODE_NOT_FOUND" || ex.Message == "STAFF_NOT_FOUND")
            {
                return BadRequest(Constant.ErrorCode.InvalidData);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<StaffPagePermission>> UpdateStaffPagePermission([FromBody] StaffPagePermissionUpdateDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }
                if (dto.Id <= 0 || dto.StaffId <= 0 || string.IsNullOrWhiteSpace(dto.PageCode) || string.IsNullOrWhiteSpace(dto.PermissionCode))
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var result = await _staffPagePermissionRepository.Update(dto.Id, dto.StaffId, dto.PageCode, dto.PermissionCode);
                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (InvalidOperationException ex) when (ex.Message == "DUPLICATE_DATA")
            {
                return BadRequest(Constant.ErrorCode.DataAlreadyExit);
            }
            catch (InvalidOperationException ex) when (ex.Message == "PAGE_CODE_NOT_FOUND" || ex.Message == "STAFF_NOT_FOUND")
            {
                return BadRequest(Constant.ErrorCode.InvalidData);
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
        public async Task<ActionResult<bool>> DeleteStaffPagePermission(int id)
        {
            try
            {
                var result = await _staffPagePermissionRepository.Delete(id);
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
