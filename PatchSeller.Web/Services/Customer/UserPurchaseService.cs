using PatchSeller.Web.DTOs;

namespace PatchSeller.Web.Services.Customer
{
    public class UserPurchaseService
    {
        private readonly HttpClient _httpClient;

        public UserPurchaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<DAL.Models.UserPurchase>>> GetAllByUserId(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Constant.EndPointApi.Customer.GetAllUserPurchase.Replace(":id", id.ToString()));

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<DAL.Models.UserPurchase>>();
                return ServiceResult<List<DAL.Models.UserPurchase>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<List<DAL.Models.UserPurchase>>.Failure(result, errorMess, response.StatusCode.ToString());

            }
        }

        public async Task<ServiceResult<bool>> CheckPatchPurchased(int patchId, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Constant.EndPointApi.Customer.CheckPatchPurchased.Replace(":id", patchId.ToString()));

            if (!string.IsNullOrEmpty(token))
            {
                var formatToken = token.Trim('"');
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", formatToken);
            }

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return ServiceResult<bool>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<bool>.Failure(result, errorMess, response.StatusCode.ToString());

            }
        }
    }
}
