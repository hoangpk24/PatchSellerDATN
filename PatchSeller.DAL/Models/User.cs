using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatchSeller.DAL.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    
    public string FullName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public double RewardPoint { get; set; }
    public int Status { get; set; }
    public DateTime? LastLogin { get; set; }
    public bool Delete { get; set; }
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("RankId")]
    public int? RankId { get; set; }
    public Rank? Rank { get; set; }
    public ICollection<Cart>? Carts { get; set; }
    public ICollection<DownloadLog>? DownloadLogs { get; set; }
    public ICollection<UserPurchase>? UserPurchases { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public ICollection<Review>? Reviews { get; set; }
}
