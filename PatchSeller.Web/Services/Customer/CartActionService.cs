using PatchSeller.Web.DTOs;

namespace PatchSeller.Web.Services.Customer
{
    public class CartDetailResponse
    {
        public List<CartItemDetailResponse> CartItems { get; set; } = new List<CartItemDetailResponse>();
        public int TotalItemCount => CartItems.Count > 0 ? CartItems.Count : 0;
        public double TemporaryTotal => CartItems.Sum(i => i.PatchPrice);
    }

    public class CartActionService
    {
        private readonly HttpClient _httpClient;
        public CartDetailResponse CartDetails { get; private set; } = new CartDetailResponse();

        public event Action OnCountChange;

        public CartActionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task LoadCartAsync(int? userId = 0)
        {

            if (userId == null || userId <= 0)
            {
                CartDetails = new CartDetailResponse();
            } else
            {
                var request = new HttpRequestMessage(HttpMethod.Get, Constant.EndPointApi.Customer.getCartByUserId + $"?userId={userId}");

                var response = await _httpClient.SendAsync(request);

                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<CartItemDetailResponse>>();
                    CartDetails.CartItems = result ?? new List<CartItemDetailResponse>();
                } else
                {
                    CartDetails = new CartDetailResponse();
                }
            }
            OnCountChange?.Invoke();
        }
    }
}
