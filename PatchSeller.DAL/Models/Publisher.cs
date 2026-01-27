using System.ComponentModel.DataAnnotations;

namespace PatchSeller.DAL.Models;

public class Publisher
{
    [Key]
    public int PublisherId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public bool Delete { get; set; }

    public ICollection<Game> Games { get; set; }
}
