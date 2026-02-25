using PatchSeller.DAL.Models;
using PatchSeller.Web.DTOs;

namespace PatchSeller.Web.Services.Customer
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<Category>>> GetAll(string? keyword = null)
        {
            var url = string.IsNullOrEmpty(keyword)
                      ? Constant.EndPointApi.Customer.CategoryGetAll
                      : $"{Constant.EndPointApi.Customer.CategoryGetAll}?keyword={keyword}";

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
    }
}
