namespace PatchSeller.API.DTOs
{
    public class CheckoutDTO
    {
        public string PaymentLink { get; set; } = string.Empty;

        public DateTime PaymentExpiration { get; set; }
        public int OrderId { get; set; }
        public string? OrderCode { get; set; }


    }
}
