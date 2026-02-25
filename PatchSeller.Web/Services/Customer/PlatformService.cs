using PatchSeller.DAL.Models;
using PatchSeller.Web.DTOs;

namespace PatchSeller.Web.Services.Customer
{
    public class PlatformService
    {
        private readonly HttpClient _httpClient;

        public PlatformService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<Platform>>> GetAll(string? keyword = null)
        {
            var url = string.IsNullOrEmpty(keyword)
                      ? Constant.EndPointApi.Customer.PlatformGetAll
                      : $"{Constant.EndPointApi.Customer.PlatformGetAll}?keyword={keyword}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<Platform>>();
                return ServiceResult<List<Platform>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<List<Platform>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }
    }
}
