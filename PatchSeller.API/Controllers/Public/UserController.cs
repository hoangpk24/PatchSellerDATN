using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using Pro219.API.DTOs;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Public
{
    [Route("user")]
    [ApiController]

    public class UserController : ControllerBase
    {
        UserRepository _userRepository;
        CartRepository _cartRepository;
        RankRepository _rankRepository;
        public UserController()
        {
            _userRepository = new UserRepository();
            _cartRepository = new CartRepository();
            _rankRepository = new RankRepository();
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
       
    }
}
