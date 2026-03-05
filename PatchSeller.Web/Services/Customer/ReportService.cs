using PatchSeller.Web.Constant;
using PatchSeller.Web.DTOs;

namespace PatchSeller.Web.Services.Customer
{
    public class ReportService
    {
        private readonly HttpClient _httpClient;

        public ReportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<StatisticResponse>> GetStatistics(DateTime? startDate, DateTime? endDate, string splitData = "month")
        {
            var queryString = $"?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}&splitData={splitData}";
            var url = Constant.EndPointApi.Admin.Statistic + queryString;

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            try
            {
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<StatisticResponse>();
                    return ServiceResult<StatisticResponse>.Success(result);
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var errorCode = result;

                    var errorMess = Constant.Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Constant.Errors[errorCode ?? ""]
                                    : result;

                    return ServiceResult<StatisticResponse>.Failure(result, errorMess, response.StatusCode.ToString());
                }
            }
            catch (Exception ex)
            {
                return ServiceResult<StatisticResponse>.Failure("Exception", ex.Message, "500");
            }
        }
    }
}
