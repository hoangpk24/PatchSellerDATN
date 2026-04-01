using Newtonsoft.Json.Linq;
using PatchSeller.Web.Components.Pages.Customer.Checkout;
using PatchSeller.Web.DTOs;
using PatchSeller.Web.Models;

namespace PatchSeller.Web.Services.Customer
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<OrderWithDetailResponse>>> GetAll(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Constant.EndPointApi.Customer.GetAllOrder);


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
            var request = new HttpRequestMessage(HttpMethod.Get, Constant.EndPointApi.Customer.GetOrderDetail.Replace(":id", id.ToString()));


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

        public async Task<ServiceResult<CheckoutResponse>> Checkout(CheckoutModel checkout, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Constant.EndPointApi.Customer.Checkout);

            request.Content = JsonContent.Create(checkout);

            if (!string.IsNullOrEmpty(token))
            {
                var formatToken = token.Trim('"');
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", formatToken);
            }

            var response = await _httpClient.SendAsync(request);

            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<CheckoutResponse>();
                return ServiceResult<CheckoutResponse>.Success(result);
            } else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<CheckoutResponse>.Failure(result, errorMess, response.StatusCode.ToString());
               
            }
        }

        public async Task<ServiceResult<DAL.Models.Order>> PaymentSuccess(int orderId, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, Constant.EndPointApi.Customer.PaymentSuccess + $"?orderId={orderId}");
            if(!string.IsNullOrEmpty(token))
            {
                var formatToken = token.Trim('"');
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", formatToken);
            }
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<DAL.Models.Order>();
                return ServiceResult<DAL.Models.Order>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<DAL.Models.Order>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<DAL.Models.Order>> PaymentCanceled(int orderId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, Constant.EndPointApi.Customer.PaymentCancelled + $"?orderId={orderId}");
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<DAL.Models.Order>();
                return ServiceResult<DAL.Models.Order>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<DAL.Models.Order>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }
    }
}
