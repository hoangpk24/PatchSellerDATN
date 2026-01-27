using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatchSeller.DAL.Models;

public class OrderDetail
{
    [Key]
    public int OrderDetailID { get; set; }
    public double Price { get; set; }

    [ForeignKey("OrderId")]
    public int OrderId { get; set; }
    public Order Order { get; set; }
    
    [ForeignKey("PatchId")]
    public int PatchId { get; set; }
    public Patch Patch { get; set; }
}
