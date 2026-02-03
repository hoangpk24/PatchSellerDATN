using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatchSeller.DAL.Models;

public class Patch
{
    [Key]
    public int PatchId { get; set; }
    

    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string UpdateBy { get; set; }
    public int Status { get; set; }
    public bool Delete { get; set; }
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("GameId")]
    public int GameId { get; set; }
    public Game Game { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
    public ICollection<UserPurchase> UserPurchases { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<PatchImage> PatchImages { get; set; }
    public ICollection<PatchVersion> PatchVersions { get; set; }
}
