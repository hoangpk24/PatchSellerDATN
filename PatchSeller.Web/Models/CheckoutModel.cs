namespace PatchSeller.Web.Models
{
    public class CheckoutModel
    {
        public List<CheckoutItemModel>? ListItemCheckout { get; set; }
        public int? DiscountApplydId { get; set; } = null;
        public int UsedRewardPoint { get; set; } = 0;
        public double DiscountAmount { get; set; } = 0;
        public string? Note { get; set; } = string.Empty;
    }

    public class CheckoutItemModel
    {
        public int PatchId { get; set; }
        public double Price { get; set; }
        public string? PatchName { get; set; }
        public string? GameName { get; set; }
    }
}
