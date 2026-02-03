using System.ComponentModel.DataAnnotations;

namespace PatchSeller.DAL.Models;

public class Platform
{
    [Key]
    public int PlatformId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Status { get; set; }
    public bool Delete { get; set; }
    public DateTime? CreatedAt { get; set; }

    public ICollection<GamePlatform>? GamePlatforms { get; set; }
}
