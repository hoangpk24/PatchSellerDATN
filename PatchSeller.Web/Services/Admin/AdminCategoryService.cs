using PatchSeller.DAL.Models;
using PatchSeller.Web.DTOs;

namespace PatchSeller.Web.Services.Admin
{
    public class AdminCategoryService
    {
        private readonly HttpClient _httpClient;

        public AdminCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<Category>>> GetAll(string? keyword = null)
        {
            var url = string.IsNullOrEmpty(keyword)
                      ? Constant.EndPointApi.Admin.CategoryGetAll
                      : $"{Constant.EndPointApi.Admin.CategoryGetAll}?keyword={keyword}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<Category>>();
                return ServiceResult<List<Category>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<List<Category>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<Category>> GetById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Constant.EndPointApi.Admin.CategoryGetById.Replace(":id", id.ToString()));

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Category>();
                return ServiceResult<Category>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<Category>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<Category>> CreateCategory(Category category)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Constant.EndPointApi.Admin.CategoryCreate);
            request.Content = JsonContent.Create(category);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Category>();
                return ServiceResult<Category>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;
                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<Category>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<Category>> UpdateCategory(Category category)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, Constant.EndPointApi.Admin.CategoryUpdate);
            request.Content = JsonContent.Create(category);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Category>();
                return ServiceResult<Category>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;
                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<Category>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<bool> Delete(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, Constant.EndPointApi.Admin.CategoryDelete.Replace(":id", id.ToString()));

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
