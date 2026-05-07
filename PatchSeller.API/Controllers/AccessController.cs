using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PatchSeller.API;
using PatchSeller.API.DTOs;
using PatchSeller.API.Utilities;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using Pro219.API.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PatchSeller.API.Controllers
{
    [Route("Access")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        UserRepository _customerRepository;
        CartRepository cartRepository;
        StaffRepository _staffRepository;

        private readonly IConfiguration _configuration;
        private readonly TimeZoneInfo _gmtPlus7 = TimeZoneInfo.CreateCustomTimeZone("GMT+7", TimeSpan.FromHours(7), "GMT+7", "GMT+7");

        public AccessController(IConfiguration configuration, UserRepository customerRepository, StaffRepository staffRepository)
        {
            _configuration = configuration;
            _customerRepository = customerRepository;
            _staffRepository = staffRepository;
            cartRepository = new CartRepository();
        }

        [HttpPost("LoginCustomer")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            string username = loginModel.Username;
            string passwordHash = loginModel.PasswordHash;
            _customerRepository = new UserRepository();
            PatchSeller.DAL.Models.User customer = _customerRepository.GetByKeyAndPassword(username, passwordHash).Result;
            if (customer != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, loginModel.Username),
                  new Claim(ClaimTypes.SerialNumber, customer.UserId.ToString()),
                new Claim(ClaimTypes.Role, "Customer"),
                 new Claim(ClaimTypes.Email, customer.Email),
                  new Claim(ClaimTypes.Name, customer.FullName),
                     new Claim(ClaimTypes.MobilePhone, customer.PhoneNumber),
                      new Claim(ClaimTypes.Surname, customer.RankId.ToString()),
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var gmtPlus7Now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _gmtPlus7);
                var expirationGmt7 = gmtPlus7Now.AddMinutes(50);
                var expirationUtc = TimeZoneInfo.ConvertTimeToUtc(expirationGmt7, _gmtPlus7);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Issuer"],
                    claims: claims,
                    expires: expirationUtc,
                    signingCredentials: creds
                );

                return Ok(new LoginResponseDTO
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = expirationGmt7,
                    LoginSuccess = true,
                    FirstLogin = customer.LastLogin == null,
                });
            }
            else
            {
                return StatusCode(403, Constant.ErrorCode.Unauthorized);
            }
        }

        [HttpPost("LoginStaff")]
        public IActionResult LoginStaff([FromBody] LoginModel loginModel)
        {
            string username = loginModel.Username;
            string passwordHash = loginModel.PasswordHash;
            _staffRepository = new StaffRepository();
            Staff staff = _staffRepository.GetByKeyAndPassword(username, passwordHash).Result;
            if (staff != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, loginModel.Username),
                   new Claim(ClaimTypes.SerialNumber, staff.StaffId.ToString()),
                new Claim(ClaimTypes.Role, staff.Role),
                 new Claim(ClaimTypes.Name, staff.UserName)
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var gmtPlus7Now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _gmtPlus7);
                var expirationGmt7 = gmtPlus7Now.AddMinutes(180);
                var expirationUtc = TimeZoneInfo.ConvertTimeToUtc(expirationGmt7, _gmtPlus7);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Issuer"],
                    claims: claims,
                    expires: expirationUtc,
                    signingCredentials: creds
                );

                return Ok(new LoginResponseDTO
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = expirationGmt7,
                    LoginSuccess = true
                });
            }
            else
            {
                return StatusCode(403, Constant.ErrorCode.Unauthorized);
            }
        }

        [HttpGet("Check")]
        [Authorize]
        public async Task<IActionResult> GetSecureData()
        {
            try
            {
                DateTime? expirationTime = null;
                bool isExpired = false;
                RankRepository rankRepository = new RankRepository();
               
                var authHeader = Request.Headers["Authorization"].ToString();
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                {
                    var token = authHeader.Replace("Bearer ", "");
                    if (!string.IsNullOrEmpty(token))
                    {
                        var handler = new JwtSecurityTokenHandler();
                        var jsonToken = handler.ReadJwtToken(token);
                        var expirationUtc = jsonToken.ValidTo;
                        expirationTime = TimeZoneInfo.ConvertTimeFromUtc(expirationUtc, _gmtPlus7);
                        isExpired = expirationUtc < DateTime.UtcNow;

                        if (isExpired)
                        {
                            return StatusCode(403, Constant.ErrorCode.TokenExpired);
                        }
                    }
                }
                int rankId = string.IsNullOrEmpty(User.FindFirst(ClaimTypes.Surname)?.Value) ? -1 : int.Parse(User.FindFirst(ClaimTypes.Surname)?.Value);
                string rankName = string.Empty;
                if(rankId != -1)
                {
                    rankName = rankRepository.GetById(rankId).Result.RankName;
                }

                var userId = int.Parse(User.FindFirst(ClaimTypes.SerialNumber)?.Value);
                var user = await _customerRepository.GetById(userId);
                var cart = cartRepository.GetCartByUserId(userId);

                var userInfo = new
                {
                    id = userId,
                    username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    role = User.FindFirst(ClaimTypes.Role)?.Value,
                    email = User.FindFirst(ClaimTypes.Email)?.Value,
                    fullName = User.FindFirst(ClaimTypes.Name)?.Value,
                    phoneNumber = User.FindFirst(ClaimTypes.MobilePhone)?.Value,
                    rankId = User.FindFirst(ClaimTypes.Surname)?.Value,
                    cartId = cart != null ? cart.Id : 0,
                    rankName = rankName,
                    rewardPoint = user != null ? user.RewardPoint : 0,
                    expirationTime = expirationTime,
                    isExpired = isExpired
                };

                return Ok(userInfo);
            }
            catch (SecurityTokenExpiredException)
            {
                return StatusCode(403, Constant.ErrorCode.TokenExpired);
            }
            catch
            {
                return StatusCode(403, Constant.ErrorCode.InvalidToken);
            }
        }

        [HttpPost("customer-register")]
        public async Task<ActionResult<bool>> CustomerRegister([FromBody] RegisterModel registerModel) { 
            var existingByEmail = await _customerRepository.FindUserExistByKeyWord(registerModel.Email);
            var existingByUsername = await _customerRepository.FindUserExistByKeyWord(registerModel.UserName);
            if (existingByEmail != null || existingByUsername != null)
            {
                return BadRequest(Constant.ErrorCode.EmailOrUsernameAlreadyExit);
            }

            var newUser = new User
            {
                FullName = registerModel.FullName,
                UserName = registerModel.UserName,
                Email = registerModel.Email,
                PhoneNumber = registerModel.PhoneNumber,
                PasswordHash = registerModel.PasswordHash,
                RewardPoint = 0,
                RankId = 1,
                Status = 1,
                Delete = false,
                LastLogin = DateTime.Now,
            };

            var customerResult = await _customerRepository.Create(newUser);

            if(customerResult == null)
            {
                return StatusCode(500, Constant.ErrorCode.DatabaseError);
            }

            var newCart = new Cart
            {
                UserId = customerResult.UserId,
                Delete = false,
            };

            var cartResult = await cartRepository.Create(newCart);

            if(cartResult == null)
            {
                return StatusCode(500, Constant.ErrorCode.DatabaseError);
            }

            return Ok(true);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<bool>> ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            if (string.IsNullOrEmpty(resetPasswordModel.Email))
            {
                return BadRequest(Constant.ErrorCode.EmailOrUsernameRequired);
            }

            var customer = await _customerRepository.FindUserByEmailAndPhoneAndUserName(resetPasswordModel.Email, string.Empty, string.Empty);

            if (customer == null)
            {
                return BadRequest(Constant.ErrorCode.EmailOrUsernameNotFound);
            }

            UtilityFunc utilityFunc = new UtilityFunc();
            string newPassword = utilityFunc.GenerateRandomString(16);
            //string newPassword = "User@12345";

            customer.PasswordHash = utilityFunc.HashPassword(newPassword);
            customer.LastLogin = null;

            var updatedCustomer = await _customerRepository.Update(customer);

            if (updatedCustomer == null)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append($"Kính chào quý khách hàng <b>{customer.FullName}</b><br><br>Mật khẩu truy cập vào tài khoản ITeam Store đã được thay đổi thành <b>{newPassword} </b> Vui lòng truy cập trang web và thay đổi mật khẩu, xin trân trọng cám ơn!<br><br>Đội ngũ ITeam");
            if (updatedCustomer == null)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
            else
            {
                bool re = await utilityFunc.SendEmailToAddress(customer.Email, customer.FullName, "Khôi phục mật khẩu tài khoản ITeam", "", sb.ToString());
            }

            return Ok(true);
        }

        [HttpPost("change-password")]
        public async Task<ActionResult<bool>> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            if(changePasswordDTO.CurrentPassword == changePasswordDTO.NewHashPassword)
            {
                return BadRequest(Constant.ErrorCode.PasswordIsTheSame);
            }

            string userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (userName == null && email == null)
            {
                return BadRequest(Constant.ErrorCode.CustomerNotFound);
            }
            else
            {
                var customer = await _customerRepository.FindUserByEmailAndPhoneAndUserName(email, string.Empty, userName);

                if (customer == null)
                {
                    return NotFound(Constant.ErrorCode.CustomerNotFound);
                }

                if(customer.PasswordHash != changePasswordDTO.CurrentPassword)
                {
                    return BadRequest(Constant.ErrorCode.CurrentPasswordFailed);
                }

                customer.PasswordHash = changePasswordDTO.NewHashPassword;
                if (customer.LastLogin == null)
                {
                    customer.LastLogin = DateTime.Now;
                }
                var updatedCustomer = await _customerRepository.Update(customer);

                if (updatedCustomer == null)
                {
                    return StatusCode(500, Constant.ErrorCode.OtherError);
                }

                return Ok(true);
            }
        }

        [HttpPost("staff-change-password")]
        public async Task<ActionResult<bool>> StaffChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            if (changePasswordDTO.CurrentPassword == changePasswordDTO.NewHashPassword)
            {
                return BadRequest(Constant.ErrorCode.PasswordIsTheSame);
            }

            string userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userName == null)
            {
                return BadRequest(Constant.ErrorCode.StaffNotFound);
            }
            else
            {
                var staff = await _staffRepository.GetByKeyAndPassword(userName, changePasswordDTO.CurrentPassword);
                if (staff == null)
                {
                    return NotFound(Constant.ErrorCode.CurrentPasswordFailed);
                }
                staff.PasswordHash = changePasswordDTO.NewHashPassword;
                var updatedStaff = await _staffRepository.Update(staff);
                if (updatedStaff == null)
                {
                    return StatusCode(500, Constant.ErrorCode.OtherError);
                }
                return Ok(true);
            }
        }
    }

}
