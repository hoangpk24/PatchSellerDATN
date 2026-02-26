using Azure;
using PatchSeller.DAL.Models;
using PatchSeller.Web.DTOs;
using PatchSeller.Web.Models;

namespace PatchSeller.Web.Services.Customer
{
    public class ReviewService
    {
        private readonly HttpClient _httpClient;

        public ReviewService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<Review>>> GetAll()
        {
            var url = Constant.EndPointApi.Customer.GetAllReview;

            var request = await _httpClient.GetAsync(url);

            if(request.IsSuccessStatusCode)
            {
                var content = await request.Content.ReadFromJsonAsync<List<Review>>();
                return ServiceResult<List<Review>>.Success(content);
            }
            else
            {
                var result = await request.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<List<Review>>.Failure(result, errorMess, request.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<List<Review>>> GetAllByPatchId(int patchId)
        {
            var url = Constant.EndPointApi.Customer.GetAllReviewByPatch.Replace(":id", patchId.ToString());

            var request = await _httpClient.GetAsync(url);

            if (request.IsSuccessStatusCode)
            {
                var content = await request.Content.ReadFromJsonAsync<List<Review>>();
                return ServiceResult<List<Review>>.Success(content);
            }
            else
            {
                var result = await request.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<List<Review>>.Failure(result, errorMess, request.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<Review>> CreateReview(ReviewModel review)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Constant.EndPointApi.Customer.CreateReview);
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


        public async Task<ServiceResult<Review>> UpdateReview(ReviewModel review)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, Constant.EndPointApi.Customer.UpdateReview);
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

        public async Task<bool> Delete(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, Constant.EndPointApi.Customer.DeleteReview.Replace(":id", id.ToString()));

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
