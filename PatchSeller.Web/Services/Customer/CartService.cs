using PatchSeller.DAL.Models;
using PatchSeller.Web.Components.Pages.Customer.Cart;
using PatchSeller.Web.DTOs;
using PatchSeller.Web.Models;

namespace PatchSeller.Web.Services.Customer
{
    public class CartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<CartItemDetailResponse>>> GetCart(int userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Constant.EndPointApi.Customer.getCartByUserId + $"?userId={userId}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<CartItemDetailResponse>>();
                return ServiceResult<List<CartItemDetailResponse>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<List<CartItemDetailResponse>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<bool> AddToCart(AddCartModel addCartModel, string token)
        {
            var url = Constant.EndPointApi.Customer.addCart;

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            if (!string.IsNullOrEmpty(token))
            {
                var formatToken = token.Trim('"');
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", formatToken);
            }

            request.Content = JsonContent.Create(addCartModel);

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
