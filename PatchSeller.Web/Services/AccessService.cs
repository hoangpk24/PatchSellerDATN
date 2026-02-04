using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using PatchSeller.Web.Constant;
using PatchSeller.Web.DTOs;
using PatchSeller.Web.Models;
using System.Security.Cryptography;
using System.Text;

namespace PatchSeller.Web.Services
{
    public class AccessService
    {
        private readonly HttpClient _httpClient;

        public AccessService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string HashPassword(string password)
        {
            // 4297F44B13955235245B2497399D7A93 (123123)
            // 26dc318942685872cf79c5eb96c9bb13 (Admin@12345)
            // b855e41c5c5f5061ecba4fd8613a7760 (User@12345)
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            md5.Clear();
            return sb.ToString();

        }

        public async Task<LoginResponse> LoginCustomer(LoginModel payload)
        {
            var requestPayload = new LoginModel
            {
                UserName = payload.UserName,
                PasswordHash = HashPassword(payload.PasswordHash)
            };

            var response = await _httpClient.PostAsJsonAsync(EndPointApi.AccessLoginCustomer, requestPayload);

            if (response.IsSuccessStatusCode)
            {
                var responseDTO = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return responseDTO;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var responseDTO = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return responseDTO ?? new LoginResponse { LoginSuccess = false };
            }
            else
            {
                return new LoginResponse { LoginSuccess = false };
            }
        }

        public async Task<LoginResponse> LoginStaff(LoginModel payload)
        {
            var requestPayload = new LoginModel
            {
                UserName = payload.UserName,
                PasswordHash = HashPassword(payload.PasswordHash)
            };

            var response = await _httpClient.PostAsJsonAsync(EndPointApi.AccessLoginStaff, requestPayload);

            if (response.IsSuccessStatusCode)
            {
                var responseDTO = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return responseDTO;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var responseDTO = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return responseDTO ?? new LoginResponse { LoginSuccess = false };
            }
            else
            {
                return new LoginResponse { LoginSuccess = false };
            }
        }
        public async Task<ServiceResult<bool>> RegisterCustomer(RegisterModel payload)
        {
            var request = new RegisterModel()
            {
                UserName = payload.UserName.Trim(),
                PasswordHash = HashPassword(payload.PasswordHash.Trim()),
                Email = payload.Email.Trim(),
                FullName = payload.FullName.Trim(),
                PhoneNumber = payload.PhoneNumber.Trim(),
            };

            var response = await _httpClient.PostAsJsonAsync(EndPointApi.AccessRegisterCustomer, request);

            if (response.IsSuccessStatusCode)
            {
                return ServiceResult<bool>.Success(true);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : "Đã có lỗi xảy ra. Vui lòng thử lại.";
                return ServiceResult<bool>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<MineResponse> AccessCheck(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, EndPointApi.AccessCheck);

            if (!string.IsNullOrEmpty(token))
            {
                var formatToken = token.Trim('"');
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", formatToken);
            }

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseDTO = await response.Content.ReadFromJsonAsync<MineResponse>();
                return responseDTO;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var responseDTO = await response.Content.ReadFromJsonAsync<MineResponse>();
                return responseDTO ?? new MineResponse { IsExpired = true };
            }
            else
            {
                var responseDTO = await response.Content.ReadFromJsonAsync<MineResponse>();
                return responseDTO ?? new MineResponse { IsExpired = true };
            }
        }

        public async Task<ServiceResult<bool>> ForgotPassword(ForgotModel request)
        {
            var response = await _httpClient.PostAsJsonAsync(EndPointApi.AccessResetPassword, request);

            if (response.IsSuccessStatusCode)
            {
                return ServiceResult<bool>.Success(true);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : "Đã có lỗi xảy ra. Vui lòng thử lại.";
                return ServiceResult<bool>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<bool>> ChangePassword(ChangePasswordModel request, string token)
        {

            var payload = new ChangePasswordModel()
            {
                CurrentPassword = HashPassword(request.CurrentPassword.Trim()),
                NewHashPassword = HashPassword(request.NewHashPassword.Trim())
            };

            var r = new HttpRequestMessage(HttpMethod.Post, EndPointApi.AccessChangePassword);

            r.Content = JsonContent.Create(payload);

            if (!string.IsNullOrEmpty(token))
            {
                var formatToken = token.Trim('"');
                r.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", formatToken);
            }

            var response = await _httpClient.SendAsync(r);

            if (response.IsSuccessStatusCode)
            {
                return ServiceResult<bool>.Success(true);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : "Đã có lỗi xảy ra. Vui lòng thử lại.";
                return ServiceResult<bool>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<bool>> StaffChangePassword(StaffChangePasswordModel request, string token)
        {

            var payload = new ChangePasswordModel()
            {
                CurrentPassword = HashPassword(request.CurrentPassword.Trim()),
                NewHashPassword = HashPassword(request.NewHashPassword.Trim())
            };

            var r = new HttpRequestMessage(HttpMethod.Post, EndPointApi.StaffChangePassword);

            r.Content = JsonContent.Create(payload);

            if (!string.IsNullOrEmpty(token))
            {
                var formatToken = token.Trim('"');
                r.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", formatToken);
            }

            var response = await _httpClient.SendAsync(r);

            if (response.IsSuccessStatusCode)
            {
                return ServiceResult<bool>.Success(true);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : errorCode;
                return ServiceResult<bool>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }
    }
}
