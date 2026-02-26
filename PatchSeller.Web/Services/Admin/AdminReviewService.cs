using PatchSeller.DAL.Models;
using PatchSeller.Web.DTOs;
using PatchSeller.Web.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace PatchSeller.Web.Services.Admin
{
    public class AdminReviewService
    {
        private readonly HttpClient _httpClient;

        public AdminReviewService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<ReviewDetailResponse>>> GetAll(string? keyword = null)
        {
            var url = string.IsNullOrEmpty(keyword)
                      ? Constant.EndPointApi.Admin.GetAllReview
                      : $"{Constant.EndPointApi.Admin.GetAllReview}?keyword={keyword}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<List<ReviewDetailResponse>>();
                return ServiceResult<List<ReviewDetailResponse>>.Success(content);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<List<ReviewDetailResponse>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<Review>> UpdateReview(ReviewModel review)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, Constant.EndPointApi.Admin.UpdateReview);
            request.Content = JsonContent.Create(review);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Review>();
                return ServiceResult<Review>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;
                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<Review>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }
    }
}
