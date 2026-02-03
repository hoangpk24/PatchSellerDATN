using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using Pro219.API.DTOs;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Admin
{
    [Route("admin/staff")]
    [ApiController]

    public class StaffController : ControllerBase
    {
        StaffRepository _staffRepository;

        public StaffController()
        {
            _staffRepository = new StaffRepository();
        }

        [HttpGet("get-all-staffs")]
        public async Task<ActionResult<List<Staff>>> GetAllStaffs(string? keyword, string? role)
        {
            try
            {
                List<Staff> result = await _staffRepository.GetAll(keyword);
                if (result == null)
                {
                    return NoContent();
                }

                if(result.Any())
                {
                    if(!string.IsNullOrEmpty(role))
                    {
                        result = result.Where(c => c.Role != null && c.Role.Equals(role, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    result = result.OrderByDescending(c => c.CreatedAt).ToList();
                }    

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("get-staff-by-id/{id}")]
        public async Task<ActionResult<Staff>> GetStaffById(int id)
        {
            try
            {
                var result = await _staffRepository.GetById(id);
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
        public async Task<ActionResult<Staff>> CreateStaff([FromBody] Staff staff)
        {
            try
            {
                if (staff == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var result = await _staffRepository.Create(staff);

                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (InvalidOperationException ex) when (ex.Message == "DUPLICATE_NAME_OR_EMAIL")
            {
                return BadRequest(Constant.ErrorCode.UserNameOrEmailAlreadyExit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<Staff>> UpdateStaff([FromBody] Staff staff)
        {
            try
            {
                if (staff == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var newStaff = new Staff
                {
                    StaffId = staff.StaffId,
                    FullName = staff.FullName,
                    UserName = staff.UserName,
                    PasswordHash = staff.PasswordHash,
                    Email = staff.Email,
                    PhoneNumber = staff.PhoneNumber,
                    Role = staff.Role
                };

                var result = await _staffRepository.Update(staff);

                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (InvalidOperationException ex) when (ex.Message == "DUPLICATE_NAME_OR_EMAIL")
            {
                return BadRequest(Constant.ErrorCode.UserNameOrEmailAlreadyExit);
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
        public async Task<ActionResult<bool>> DeleteCategory(int id)
        {
            try
            {
                var result = await _staffRepository.Delete(id);

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
