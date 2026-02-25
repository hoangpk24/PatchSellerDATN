using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PatchSeller.DAL.Models;

public class Patch
{
    [Key]
    public int PatchId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? ThumbnailLink { get; set; } = string.Empty;
    public string? UpdateBy { get; set; }
    public int Status { get; set; }
    public bool Delete { get; set; }
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("GameId")]
    public int GameId { get; set; }
    [JsonIgnore]
    public Game? Game { get; set; }
    [JsonIgnore]
    public ICollection<CartItem>? CartItems { get; set; }
    [JsonIgnore]
    public ICollection<UserPurchase>? UserPurchases { get; set; }
    [JsonIgnore]
    public ICollection<OrderDetail>? OrderDetails { get; set; }
    [JsonIgnore]
    public ICollection<Review>? Reviews { get; set; }
    [JsonIgnore]
    public ICollection<PatchImage>? PatchImages { get; set; }
    [JsonIgnore]
    public ICollection<PatchVersion>? PatchVersions { get; set; }
}
