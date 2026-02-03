using System.ComponentModel.DataAnnotations;

namespace PatchSeller.DAL.Models;

public class Rank
{
    [Key]
    public int RankId { get; set; }
    public string RankName { get; set; }
    public double MiniumSpend { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public bool Delete { get; set; }
    public DateTime? CreatedAt { get; set; }

    public ICollection<User> Users { get; set; }
    public ICollection<Discount> Discounts { get; set; }
}
