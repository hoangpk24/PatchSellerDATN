using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatchSeller.DAL.Models;

public class CartItem
{
    [Key]
    public int CartItemId { get; set; }   
    public bool Delete { get; set; }
    [ForeignKey("CartId")]
    public int CartId { get; set; }
    public Cart Cart { get; set; }    
    [ForeignKey("PatchId")]
    public int PatchId { get; set; }
    public Patch Patch { get; set; }
}
