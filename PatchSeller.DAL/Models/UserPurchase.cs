using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatchSeller.DAL.Models;

public class UserPurchase
{
    [Key]
    public int UserPurchaseId { get; set; } 
    public DateTime PurchasedAt { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User? User { get; set; }
    
    [ForeignKey("PatchId")]
    public int PatchId { get; set; }
    public Patch? Patch { get; set; }
}
