using PatchSeller.DAL.Models;
using PatchSeller.Web.DTOs;

namespace PatchSeller.Web.Services.Admin
{
    public class AdminUploadService
    {
        private readonly HttpClient _httpClient;

        public AdminUploadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<UploadFileResponse>> UploadFileAsync(Stream fileStream, string fileName)
        {
            using var content = new MultipartFormDataContent();

            var streamContent = new StreamContent(fileStream);
            content.Add(streamContent, "file", fileName);
            
            var nameContent = new StringContent(fileName);
            content.Add(nameContent, "fileName");

            var response = await _httpClient.PostAsync("/WeatherForecast/upload", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UploadFileResponse>();
                return ServiceResult<UploadFileResponse>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;
                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                ? Constant.Constant.Errors[errorCode ?? ""]
                                : result;
                return ServiceResult<UploadFileResponse>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<bool>> DeleteFileAsync(string url)
        {
            var response = await _httpClient.GetAsync($"/WeatherForecast/delete-file-google?url={Uri.EscapeDataString(url)}");
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

        public async Task<ServiceResult<CheckFileResponse>> CheckFile(string url)
        {
            var response = await _httpClient.GetAsync($"/WeatherForecast/check-file?url={Uri.EscapeDataString(url)}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<CheckFileResponse>();
                return ServiceResult<CheckFileResponse>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;
                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                ? Constant.Constant.Errors[errorCode ?? ""]
                                : result;
                return ServiceResult<CheckFileResponse>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }
    }
}
