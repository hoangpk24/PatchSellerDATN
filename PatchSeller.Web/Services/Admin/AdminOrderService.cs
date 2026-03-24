using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using PatchSeller.DAL.Models;
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

        public async Task<ServiceResult<List<OrderWithDetailResponse>>> GetAllByKeyword(string token, int? status = null, string? keyword = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var queryParams = new Dictionary<string, string?>();

            if (!string.IsNullOrEmpty(keyword))
            {
                queryParams.Add("keyword", keyword);
            }
            if (status != null && status > 0)
            {
                queryParams.Add("status", status.ToString());
            }
            if (startDate.HasValue)
            {
                queryParams.Add("startDate", startDate?.ToString("o"));
            }
            if (endDate.HasValue)
            {
                queryParams.Add("endDate", endDate?.ToString("o"));
            }

            var url = QueryHelpers.AddQueryString(Constant.EndPointApi.Admin.Orders, queryParams);

            var request = new HttpRequestMessage(HttpMethod.Get, url);

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
