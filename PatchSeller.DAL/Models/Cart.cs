using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatchSeller.DAL.Models;

public class Cart
{
    [Key]
    public int CartId { get; set; }
    
    public bool Delete { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User? User { get; set; }
    public ICollection<CartItem>? CartItems { get; set; }
}
