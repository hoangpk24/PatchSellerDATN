namespace PatchSeller.Web.DTOs
{
    public class CartItemDetailResponse
    {
        public int CartItemId { get; set; }

        public int CartId { get; set; }

        public int PatchId { get; set; }
        public string PatchName { get; set; }
        public string GameThumbnail { get; set; }
        public string GameTitle { get; set; }
        public int GameId { get; set; }
        public double PatchPrice { get; set; }
        public string PatchDescription { get; set; }
        public string GameDescription { get; set; }
    }
}
