namespace PatchSeller.Web.Services.Customer
{
    public class CartItemService
    {
        private readonly HttpClient _httpClient;

        public CartItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Delete(int id, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, Constant.EndPointApi.Customer.DeleteCartItem.Replace(":id", id.ToString()));

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
    }
}
