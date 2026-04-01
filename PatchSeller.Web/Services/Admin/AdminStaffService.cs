using Microsoft.AspNetCore.WebUtilities;
using PatchSeller.DAL.Models;
using PatchSeller.Web.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace PatchSeller.Web.Services.Admin
{
    public class AdminStaffService
    {
        private readonly HttpClient _httpClient;

        public AdminStaffService(HttpClient httpClient)
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

        public async Task<ServiceResult<List<Staff>>> GetAll(string? keyword = null, string? role = null)
        {
            var queryParams = new Dictionary<string, string?>();
            if (!string.IsNullOrEmpty(keyword))
            {
                queryParams.Add("keyword", keyword);
            }
            if(!string.IsNullOrEmpty(role))
            {
                queryParams.Add("role", role);
            }

            var url = QueryHelpers.AddQueryString(Constant.EndPointApi.Admin.StaffGetAll, queryParams);
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<Staff>>();
                return ServiceResult<List<Staff>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<List<Staff>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<Staff>> GetById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Constant.EndPointApi.Admin.StaffGetById.Replace(":id", id.ToString()));

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Staff>();
                return ServiceResult<Staff>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<Staff>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<Staff>> CreateStaff(Staff staff)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Constant.EndPointApi.Admin.StaffCreate);
            if(!string.IsNullOrEmpty(staff.PasswordHash))
            {
                string passwordHash = HashPassword(staff.PasswordHash);
                staff.PasswordHash = passwordHash;
            }
            request.Content = JsonContent.Create(staff);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Staff>();
                return ServiceResult<Staff>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;
                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<Staff>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<Staff>> UpdateStaff(Staff staff)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, Constant.EndPointApi.Admin.StaffUpdate);
            request.Content = JsonContent.Create(staff);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Staff>();
                return ServiceResult<Staff>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;
                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<Staff>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<bool> Delete(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, Constant.EndPointApi.Admin.StaffDelete.Replace(":id", id.ToString()));

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
