using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PatchSeller.DAL.Models;

public class Game
{
    [Key]
    public int GameId { get; set; }
    
    public string Title { get; set; }
    public string Developer { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Thumbnail { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public int Status { get; set; }
    public bool Delete { get; set; }
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("PublisherId")]
    public int PublisherId { get; set; }
    [JsonIgnore]
    public Publisher? Publisher { get; set; }
    [JsonIgnore]
    public ICollection<GameCategory>? GameCategories { get; set; }
    [JsonIgnore]
    public ICollection<GameImage>? GameImages { get; set; }
    [JsonIgnore]
    public ICollection<GamePlatform>? GamePlatforms { get; set; }
    [JsonIgnore]
    public ICollection<Patch>? Patches { get; set; }
}
