using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatchSeller.DAL.Models;

public class Discount
{
    [Key]
    public int DiscountId { get; set; }    
    public string Code { get; set; }
    public string DiscountType { get; set; }
    public double Value { get; set; }
    public double MaxDiscount { get; set; }
    public double MinOrderValue { get; set; }
    public int UsageLimit { get; set; }
    public int LimitPerUser { get; set; }
    public int UsedCount { get; set; }
    public int Status { get; set; }
    public bool Delete { get; set; }

    [ForeignKey("RankId")]
    public int? RankId { get; set; }
    public Rank Rank { get; set; }
    public ICollection<Order> Orders { get; set; }
}
