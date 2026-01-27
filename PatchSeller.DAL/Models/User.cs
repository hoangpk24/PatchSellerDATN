using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatchSeller.DAL.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string PasswordHash { get; set; }
    public double RewardPoint { get; set; }
    public int Status { get; set; }
    public bool Delete { get; set; }

    [ForeignKey("RankId")]
    public int? RankId { get; set; }
    public Rank? Rank { get; set; }
    public ICollection<Cart> Carts { get; set; }
    public ICollection<DownloadLog> DownloadLogs { get; set; }
    public ICollection<UserPurchase> UserPurchases { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Review> Reviews { get; set; }
}
