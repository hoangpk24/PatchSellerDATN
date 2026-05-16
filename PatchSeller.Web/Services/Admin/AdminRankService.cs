using PatchSeller.DAL.Models;
using PatchSeller.Web.DTOs;

namespace PatchSeller.Web.Services.Admin
{
    public class AdminRankService
    {
        private readonly HttpClient _httpClient;

        public AdminRankService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<Rank>>> GetAll(string? keyword = null)
        {
            var url = string.IsNullOrEmpty(keyword)
                      ? Constant.EndPointApi.Admin.RankGetAll
                      : $"{Constant.EndPointApi.Admin.RankGetAll}?keyword={keyword}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<Rank>>();
                return ServiceResult<List<Rank>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<List<Rank>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<Rank>> GetById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Constant.EndPointApi.Admin.RankGetById.Replace(":id", id.ToString()));

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Rank>();
                return ServiceResult<Rank>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<Rank>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<Rank>> CreateRank(Rank rank)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Constant.EndPointApi.Admin.RankCreate);
            request.Content = JsonContent.Create(rank);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Rank>();
                return ServiceResult<Rank>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;
                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<Rank>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<Rank>> UpdateRank(Rank rank)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, Constant.EndPointApi.Admin.RankUpdate);
            request.Content = JsonContent.Create(rank);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Rank>();
                return ServiceResult<Rank>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;
                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<Rank>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<bool> Delete(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, Constant.EndPointApi.Admin.RankDelete.Replace(":id", id.ToString()));

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

        public async Task<ServiceResult<ChangePercentResponse>> GetPercentPoint()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Constant.EndPointApi.Admin.ChangePercentPoint);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ChangePercentResponse>();
                return ServiceResult<ChangePercentResponse>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;
                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<ChangePercentResponse>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<bool> ChangePercentPoint(double percent)
        {
            var url = $"{Constant.EndPointApi.Admin.ChangePercentPoint}?rewardPercent={percent.ToString(System.Globalization.CultureInfo.InvariantCulture)}";

            var response = await _httpClient.PutAsync(url, null);
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
