using System.ComponentModel.DataAnnotations;

namespace PatchSeller.DAL.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public bool Delete { get; set; }

    public ICollection<GameCategory> GameCategories { get; set; }
}
