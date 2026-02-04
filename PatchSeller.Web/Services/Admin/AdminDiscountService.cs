using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using PatchSeller.DAL.Models;
using PatchSeller.Web.DTOs;
using PatchSeller.Web.Models;

namespace PatchSeller.Web.Services.Admin
{
    public class AdminDiscountService
    {
        private readonly HttpClient _httpClient;

        public AdminDiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<Discount>>> GetAll(
            string? keyword = null,
            string? discountType = null,
            int? rankId = 0,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var queryParams = new Dictionary<string, string?>();

            if(!string.IsNullOrEmpty(keyword))
            {
                queryParams.Add("keyword", keyword);
            }
            if (!string.IsNullOrEmpty(discountType)) { 
                queryParams.Add("discountType", discountType);
            }
            if (rankId != null && rankId > 0)
            {
                queryParams.Add("rankId", rankId.ToString());
            }
            if (startDate.HasValue)
            {
                queryParams.Add("startDate", startDate?.ToString("o"));
            }
            if (endDate.HasValue)
            {
                queryParams.Add("endDate", endDate?.ToString("o"));
            }

            var url = QueryHelpers.AddQueryString(Constant.EndPointApi.Admin.VoucherGetAll, queryParams);

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<Discount>>();
                return ServiceResult<List<Discount>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<List<Discount>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<DiscountModel>> GetById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Constant.EndPointApi.Admin.VoucherGetById.Replace(":id", id.ToString()));

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<DiscountModel>();
                return ServiceResult<DiscountModel>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<DiscountModel>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<Discount>> CreateDiscount(DiscountModel discount)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Constant.EndPointApi.Admin.VoucherCreate);
            request.Content = JsonContent.Create(discount);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Discount>();
                return ServiceResult<Discount>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;
                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<Discount>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<Discount>> UpdateDiscount(DiscountModel discount)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, Constant.EndPointApi.Admin.VoucherUpdate);
            request.Content = JsonContent.Create(discount);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Discount>();
                return ServiceResult<Discount>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;
                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<Discount>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<bool> DeleteDiscount(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, Constant.EndPointApi.Admin.VoucherDelete.Replace(":id", id.ToString()));

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
