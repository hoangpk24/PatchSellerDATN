using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatchSeller.DAL.Models;

public class Review
{
    [Key]
    public int ReviewId { get; set; }
   
    public string UserName { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public double Overall { get; set; }
    public int Status { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User User { get; set; }
    
    [ForeignKey("PatchId")]
    public int PatchId { get; set; }
    public Patch Patch { get; set; }
}
