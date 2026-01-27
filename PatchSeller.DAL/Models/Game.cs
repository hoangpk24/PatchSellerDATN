using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatchSeller.DAL.Models;

public class Game
{
    [Key]
    public int GameId { get; set; }
    
    public string Title { get; set; }
    public string Developer { get; set; }
    public string Description { get; set; }
    public string Thumbnail { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int Status { get; set; }
    public bool Delete { get; set; }

    [ForeignKey("PublisherId")]
    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; }
    public ICollection<GameCategory> GameCategories { get; set; }
    public ICollection<GameImage> GameImages { get; set; }
    public ICollection<GamePlatform> GamePlatforms { get; set; }
    public ICollection<Patch> Patches { get; set; }
}
