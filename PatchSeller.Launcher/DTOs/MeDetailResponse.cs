namespace PatchSeller.Launcher.DTOs
{
    public class MeDetailResponse
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public double RewardPoint { get; set; }
        public int? RankId { get; set; }
        public string RankName { get; set; } = string.Empty;
        public double TotalSpent { get; set; }
        public DateTime? CreateAt { get; set; }
        public List<OrderWithDetailResponse> Orders { get; set; } = new List<OrderWithDetailResponse>();
        public List<UserPurchasePatchResponse> PurchasedPatches { get; set; } = new List<UserPurchasePatchResponse>();
    }
    public class UserPurchasePatchResponse
    {
        public int PatchId { get; set; }
        public int GameId { get; set; }
        public string GameName { get; set; } = string.Empty;
        public string PatchName { get; set; } = string.Empty;
        public string PatchThumbnail { get; set; } = string.Empty;
        public string GameThumbnail { get; set; } = string.Empty;
        public DateTime PurchasedAt { get; set; }
    }
}
