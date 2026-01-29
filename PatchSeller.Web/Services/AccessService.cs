using Microsoft.AspNetCore.Identity.Data;
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

            var response = await _httpClient.PostAsJsonAsync("/Access/LoginCustomer", requestPayload);

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

            var response = await _httpClient.PostAsJsonAsync("/Access/LoginStaff", requestPayload);

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

        public async Task<MineResponse> AccessCheck(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/Access/Check");

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
    }
}
