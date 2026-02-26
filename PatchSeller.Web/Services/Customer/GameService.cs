using PatchSeller.Web.DTOs;

namespace PatchSeller.Web.Services.Customer
{
    public class GameService
    {
        private readonly HttpClient _httpClient;

        public GameService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<GameForHomeResponse>> GetAllForHome()
        {
            var url = Constant.EndPointApi.Customer.GameForHome;

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GameForHomeResponse>();
                return ServiceResult<GameForHomeResponse>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<GameForHomeResponse>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<GameDetailResponse>> GetDetailById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Constant.EndPointApi.Customer.GameDetail.Replace(":id", id.ToString()));

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GameDetailResponse>();
                return ServiceResult<GameDetailResponse>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;
                return ServiceResult<GameDetailResponse>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<GameSearchResponse>> SearchGames(
        string? keyword,
        List<int>? categoryIds,
        List<int>? platformIds,
        double? startMoney,
        double? endMoney,
        string? sort,
        int page = 1,
        int perPage = 12)
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(keyword)) queryParams.Add($"keyword={Uri.EscapeDataString(keyword)}");

            if (categoryIds != null && categoryIds.Any())
                queryParams.Add($"categoryIds={string.Join(",", categoryIds)}");

            if (platformIds != null && platformIds.Any())
                queryParams.Add($"platformIds={string.Join(",", platformIds)}");

            if (startMoney.HasValue) queryParams.Add($"startMoney={startMoney}");
            if (endMoney.HasValue) queryParams.Add($"endMoney={endMoney}");
            if (!string.IsNullOrEmpty(sort)) queryParams.Add($"sort={sort}");

            queryParams.Add($"page={page}");
            queryParams.Add($"perPage={perPage}");

            var url = $"{Constant.EndPointApi.Customer.GameForSearch}?{string.Join("&", queryParams)}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GameSearchResponse>();
                return ServiceResult<GameSearchResponse>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;
                var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                ? Constant.Constant.Errors[errorCode ?? ""]
                                : result;
                return ServiceResult<GameSearchResponse>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }
    }
}
