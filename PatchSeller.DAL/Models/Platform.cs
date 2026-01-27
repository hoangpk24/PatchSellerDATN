using System.ComponentModel.DataAnnotations;

namespace PatchSeller.DAL.Models;

public class Platform
{
    [Key]
    public int PlatformId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public bool Delete { get; set; }

    public ICollection<GamePlatform> GamePlatforms { get; set; }
}
