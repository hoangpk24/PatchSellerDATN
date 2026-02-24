using PatchSeller.DAL.Models;

namespace PatchSeller.API.DTOs
{
    public class UserPurchasePatchDTO
    {
        public int PatchId { get; set; }
        public int GameId { get; set; }
        public string GameName { get; set; } = string.Empty;
        public string PatchName { get; set; } = string.Empty;
        public DateTime PurchasedAt { get; set; }
    }

    public class UserMeDetailDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public double RewardPoint { get; set; }
        public int? RankId { get; set; }
        public string RankName { get; set; } = string.Empty;
        public double TotalSpent { get; set; }
        public List<OrderDetailResponseDTO> Orders { get; set; } = new List<OrderDetailResponseDTO>();
        public List<UserPurchasePatchDTO> PurchasedPatches { get; set; } = new List<UserPurchasePatchDTO>();
    }
}

