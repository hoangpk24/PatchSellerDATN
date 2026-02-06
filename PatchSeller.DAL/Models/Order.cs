using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatchSeller.DAL.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }
    public string PaymentStatus { get; set; }
    public string OrderCode { get; set; }
    public DateTime OrderDate { get; set; }
    public double UsedRewardPoint { get; set; }
    public double TotalAmount { get; set; }
    public double DiscountAmount { get; set; }
    public double FinalAmount { get; set; }
    public string? PaymentLink { get; set; }
    public DateTime? PaymentExpiration { get; set; }
    public string? Note { get; set; }
    public int Status { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User? User { get; set; }
    
    [ForeignKey("DiscountId")]
    public int? DiscountId { get; set; }
    public Discount? Discount { get; set; }
    public ICollection<OrderDetail>? OrderDetails { get; set; }
}
