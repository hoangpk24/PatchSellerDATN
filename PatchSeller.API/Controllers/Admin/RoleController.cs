using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PatchSeller.API.DTOs;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using Pro219.API.DTOs;

namespace PatchSeller.API.Controllers.Admin
{
    [Route("admin/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleRepository _roleRepository;
        private readonly StaffRepository _staffRepository;
        private readonly StaffPagePermissionRepository _staffPagePermissionRepository;

        public RoleController()
        {
            _roleRepository = new RoleRepository();
            _staffRepository = new StaffRepository();
            _staffPagePermissionRepository = new StaffPagePermissionRepository();
        }

        [HttpGet("get-all-roles")]
        public async Task<ActionResult<List<Role>>> GetAllRoles()
        {
            try
            {
                var result = await _roleRepository.GetAll();
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

        [HttpGet("get-role-by-id/{id}")]
        public async Task<ActionResult<Role>> GetRoleById(int id)
        {
            try
            {
                var result = await _roleRepository.GetById(id);
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
        public async Task<ActionResult<Role>> CreateRole([FromBody] Role role)
        {
            try
            {
                if (role == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var result = await _roleRepository.Create(role);
                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<Role>> UpdateRole([FromBody] Role role)
        {
            try
            {
                if (role == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var existingRole = await _roleRepository.GetByIdIncludeInactive(role.Id);
                if (existingRole == null)
                {
                    return BadRequest(Constant.ErrorCode.NotFound);
                }

                var staffsInRole = await _staffRepository.GetAllByRoleId(role.Id) ?? new List<Staff>();
                var staffIds = staffsInRole.Select(x => x.StaffId).ToList();
                var isStatusChanged = existingRole.Status != role.Status;
                var isPagesPermissionChanged = (existingRole.PagesPermission ?? string.Empty) != (role.PagesPermission ?? string.Empty);

                var result = await _roleRepository.Update(role);
                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                if (isStatusChanged || isPagesPermissionChanged)
                {
                    var removeResult = await _staffPagePermissionRepository.DeleteByStaffIds(staffIds);
                    if (!removeResult)
                    {
                        return StatusCode(500, Constant.ErrorCode.DatabaseError);
                    }
                }

                if (result.Status && (isStatusChanged || isPagesPermissionChanged))
                {
                    var pagePermissionDTOs = JsonConvert.DeserializeObject<List<PagePermissionDTO>>(result.PagesPermission ?? string.Empty) ?? new List<PagePermissionDTO>();

                    foreach (var staffId in staffIds)
                    {
                        var addResult = await _staffPagePermissionRepository.CreateRangeByPagePermissions(
                            pagePermissionDTOs.Select(x => (x.PageCode, x.PagePermissions)).ToList(),
                            staffId);

                        if (!addResult)
                        {
                            return StatusCode(500, Constant.ErrorCode.DatabaseError);
                        }
                    }
                }

                return Ok(result);
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

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<bool>> DeleteRole(int id)
        {
            try
            {
                var existingRole = await _roleRepository.GetById(id);
                if (existingRole == null)
                {
                    return BadRequest(Constant.ErrorCode.NotFound);
                }

                var result = await _roleRepository.Delete(id);
                if (!result)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
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
