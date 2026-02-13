using Newtonsoft.Json.Linq;
using PatchSeller.Web.DTOs;
using PatchSeller.Web.Models;

namespace PatchSeller.Web.Services.Admin
{
    public class AdminOrderService
    {
        private readonly HttpClient _httpClient;

        public AdminOrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<OrderWithDetailResponse>>> GetAll(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Constant.EndPointApi.Admin.Orders);


            if (!string.IsNullOrEmpty(token))
            {
                var formatToken = token.Trim('"');
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", formatToken);
            }

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<OrderWithDetailResponse>>();
                return ServiceResult<List<OrderWithDetailResponse>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<List<OrderWithDetailResponse>>.Failure(result, errorMess, response.StatusCode.ToString());

            }
        }

        public async Task<ServiceResult<OrderWithDetailResponse>> GetById(int id, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Constant.EndPointApi.Admin.OrderDetail.Replace(":id", id.ToString()));


            if (!string.IsNullOrEmpty(token))
            {
                var formatToken = token.Trim('"');
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", formatToken);
            }

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OrderWithDetailResponse>();
                return ServiceResult<OrderWithDetailResponse>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<OrderWithDetailResponse>.Failure(result, errorMess, response.StatusCode.ToString());

            }
        }
    }
}
