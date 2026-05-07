using PatchSeller.DAL.Models;
using PatchSeller.Web.Constant;
using PatchSeller.Web.DTOs;
using PatchSeller.Web.Models;

namespace PatchSeller.Web.Services.Admin
{
    public class AdminRoleService
    {
        private readonly HttpClient _httpClient;

        public AdminRoleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<RoleResponse>>> GetAllRoles()
        {
            var response = await _httpClient.GetAsync(Constant.EndPointApi.Admin.GetAllRole);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<RoleResponse>>();
                return ServiceResult<List<RoleResponse>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<List<RoleResponse>>.Failure(result, errorMess, response.StatusCode.ToString());

            }
        }

        public async Task<ServiceResult<RoleResponse>> GetRoleById(int id)
        {
            var response = await _httpClient.GetAsync(Constant.EndPointApi.Admin.RoleGetById.Replace(":id", id.ToString()));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<RoleResponse>();
                return ServiceResult<RoleResponse>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;
                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<RoleResponse>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<RoleResponse>> CreateRole(RoleModel request)
        {
            var response = await _httpClient.PostAsJsonAsync(Constant.EndPointApi.Admin.RoleCreate, request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<RoleResponse>();
                return ServiceResult<RoleResponse>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;
                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<RoleResponse>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<RoleResponse>> UpdateRole(RoleModel request)
        {
            var response = await _httpClient.PutAsJsonAsync(Constant.EndPointApi.Admin.RoleUpdate, request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<RoleResponse>();
                return ServiceResult<RoleResponse>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;
                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<RoleResponse>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<bool> DeleteRole(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, Constant.EndPointApi.Admin.RoleDelete.Replace(":id", id.ToString()));

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
