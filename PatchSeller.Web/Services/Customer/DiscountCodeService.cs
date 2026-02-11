using Microsoft.AspNetCore.WebUtilities;
using PatchSeller.Web.DTOs;

namespace PatchSeller.Web.Services.Customer
{
    public class DiscountCodeService
    {
        private readonly HttpClient _httpClient;

        public DiscountCodeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<DiscountResponse>> ApplyDiscountCode(string discountCode, double totalAmount, string token)
        {
            var baseUrl = Constant.EndPointApi.Customer.ApplyDiscountCode;

            var queryParams = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(discountCode))
            {
                queryParams.Add("code", discountCode);
            }

            if (totalAmount > 0) { 
                queryParams.Add("totalAmount", totalAmount.ToString());
            }

            string url = QueryHelpers.AddQueryString(baseUrl, queryParams!);

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            if (!string.IsNullOrEmpty(token))
            {
                var formatToken = token.Trim('"');
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", formatToken);
            }

            var response = await _httpClient.SendAsync(request);

            if(response.IsSuccessStatusCode)
            {
                var discountValue = await response.Content.ReadFromJsonAsync<DiscountResponse>();
                return ServiceResult<DiscountResponse>.Success(discountValue);
            } else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<DiscountResponse>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }
    }
}
