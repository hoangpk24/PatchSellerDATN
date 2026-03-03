using PatchSeller.Web.DTOs;

namespace PatchSeller.Web.Services.Customer
{
    public class UploadService
    {
        private readonly HttpClient _httpClient;

        public UploadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<ScanResponse>> ScanFile(string fileUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Constant.EndPointApi.Customer.ScanFile + $"?url={fileUrl}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ScanResponse>();
                return ServiceResult<ScanResponse>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;
                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                ? Constant.Constant.Errors[errorCode ?? ""]
                                : result;
                return ServiceResult<ScanResponse>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }
    }
}
