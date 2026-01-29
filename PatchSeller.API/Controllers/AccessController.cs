using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PatchSeller.API.Utilities;
using PatchSeller.API.DTOs;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PatchSeller.API;
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
                      new Claim(ClaimTypes.Surname, customer.RankId.ToString())
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

                var userInfo = new
                {
                    id = User.FindFirst(ClaimTypes.SerialNumber)?.Value,
                    username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    role = User.FindFirst(ClaimTypes.Role)?.Value,
                    email = User.FindFirst(ClaimTypes.Email)?.Value,
                    fullName = User.FindFirst(ClaimTypes.Name)?.Value,
                    phoneNumber = User.FindFirst(ClaimTypes.MobilePhone)?.Value,
                    rankId = User.FindFirst(ClaimTypes.Surname)?.Value,
                    rankName = rankName,
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

    }

}
