namespace PatchSeller.Web.DTOs
{
    public class OrderWithDetailResponse
    {
        public int OrderId { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
        public string OrderCode { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public double UsedRewardPoint { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double FinalAmount { get; set; }
        public string? PaymentLink { get; set; }
        public DateTime? PaymentExpiration { get; set; }
        public string? Note { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public int? DiscountId { get; set; }
        public UserDetailResponse User { get; set; } = new UserDetailResponse();
        public List<OrderDetailItemResponse> OrderDetails { get; set; } = new List<OrderDetailItemResponse>();

        public class OrderDetailItemResponse
        {
            public int OrderDetailId { get; set; }
            public int OrderId { get; set; }
            public int PatchId { get; set; }
            public int GameId { get; set; }
            public string GameThumbnail { get; set; } = string.Empty;
            public string GameName { get; set; } = string.Empty;
            public string PatchName { get; set; } = string.Empty;
            public double Price { get; set; }
            public List<GameImageBasicDTO> GameImages { get; set; } = new List<GameImageBasicDTO>();
            public List<PatchImageBasicDTO> PatchImages { get; set; } = new List<PatchImageBasicDTO>();
        }
    }
}
