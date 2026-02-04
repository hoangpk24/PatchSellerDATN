using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using Pro219.API.DTOs;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Admin
{
    [Route("admin/user")]
    [ApiController]

    public class UserController : ControllerBase
    {
        UserRepository _userRepository;
        CartRepository _cartRepository;
        public UserController()
        {
            _userRepository = new UserRepository();
            _cartRepository = new CartRepository();
        }

        [HttpGet("get-all-users")]
        public async Task<ActionResult<List<User>>> GetAllUsers(string? keyword, int? rankId)
        {
            try
            {
                List<User> result = await _userRepository.GetAll(keyword);
                if (result == null)
                {
                    return NoContent();
                }

                if(result.Any())
                {
                    if(rankId != null && rankId > 0)
                    {
                        result = result.Where(c => c.RankId != null && c.RankId == rankId).ToList();
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

        [HttpGet("get-user-by-id/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                var result = await _userRepository.GetById(id);
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
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                user.CreatedAt = DateTime.Now;
                var result = await _userRepository.Create(user);

                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                var newCart = new Cart
                {
                    UserId = result.UserId,
                    Delete = false,
                };

                var cartResult = await _cartRepository.Create(newCart);

                if (cartResult == null)
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
        public async Task<ActionResult<User>> UpdateUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var newStaff = new User
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    PasswordHash = user.PasswordHash,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    RewardPoint = user.RewardPoint,
                    Status = user.Status,
                    RankId = user.RankId,
                    Delete = user.Delete
                };

                var result = await _userRepository.Update(user);

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
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            try
            {
                var result = await _userRepository.Delete(id);

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
