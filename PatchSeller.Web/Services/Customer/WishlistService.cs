using Newtonsoft.Json.Linq;
using PatchSeller.DAL.Models;
using PatchSeller.Web.Constant;
using PatchSeller.Web.DTOs;

namespace PatchSeller.Web.Services.Customer
{
    public class WishlistService
    {
        private readonly HttpClient _httpClient;

        public WishlistService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<GameDetailResponse>>> GetAllWishListByUserId(int customerId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Constant.EndPointApi.Customer.GetAllWishListByUserId + $"?userId={customerId}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<GameDetailResponse>>();
                return ServiceResult<List<GameDetailResponse>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<List<GameDetailResponse>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<Wishlist>> GetByCustomerAndGameId(int customerId, int gameId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Constant.EndPointApi.Customer.GetByCustomerAndGameId.Replace(":customerId", customerId.ToString()).Replace(":gameId", gameId.ToString()));

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Wishlist>();
                return ServiceResult<Wishlist>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<Wishlist>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<DAL.Models.Wishlist>> Create(DAL.Models.Wishlist wishlist, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Constant.EndPointApi.Customer.CreateWishList);

            if (!string.IsNullOrEmpty(token))
            {
                var formatToken = token.Trim('"');
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", formatToken);
            }

            request.Content = JsonContent.Create(wishlist);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<DAL.Models.Wishlist>();
                return ServiceResult<DAL.Models.Wishlist>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<DAL.Models.Wishlist>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<bool> Delete(int id, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, EndPointApi.Customer.DeleteWishList.Replace(":id", id.ToString()));

            if (!string.IsNullOrEmpty(token))
            {
                var formatToken = token.Trim('"');
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", formatToken);
            }

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

        public async Task<ServiceResult<bool>> ToggleWishlist(int customerId, int gameId, string token)
        {
            try
            {
                var existingWishlist = await GetByCustomerAndGameId(customerId, gameId);

                if (existingWishlist.IsSuccess && existingWishlist.Data != null)
                {
                    var deleteResult = await Delete(existingWishlist.Data.Id, token);
                    if (deleteResult)
                    {
                        return ServiceResult<bool>.Success(false);
                    }
                    else
                    {
                        return ServiceResult<bool>.Failure("DELETE_FAILED", "Không thể xóa khỏi wishlist", "500");
                    }
                }
                else
                {
                    var newWishlist = new Wishlist
                    {
                        UserId = customerId,
                        GameId = gameId,
                    };

                    var createResult = await Create(newWishlist, token);
                    if (createResult.IsSuccess)
                    {
                        return ServiceResult<bool>.Success(true);
                    }
                    else
                    {
                        return ServiceResult<bool>.Failure(createResult.ErrorCode, createResult.ErrorMessage, createResult.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure("EXCEPTION", $"Lỗi: {ex.Message}", "500");
            }
        }
    }
}
