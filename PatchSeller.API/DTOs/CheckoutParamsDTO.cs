namespace PatchSeller.API.DTOs
{
    public class CheckoutParamsDTO
    {
        public List<CheckoutItemDTO>? ListItemCheckout { get; set; }
        public int? DiscountApplydId { get; set; } = null;
        public int UsedRewardPoint { get; set; } = 0;

        public double DiscountAmount { get; set; } = 0;
        
        public string? Note { get; set; } = string.Empty; 
    }
}
