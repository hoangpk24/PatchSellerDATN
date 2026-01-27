using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatchSeller.DAL.Models;

public class GamePlatform
{
    [Key]
    public int GamePlatformId { get; set; }
    

    
 
    public string Description { get; set; }
    public int Status { get; set; }
    public bool Delete { get; set; }

    [ForeignKey("PlatformId")]
    public int PlatformId { get; set; }
    public Platform Platform { get; set; }
    
    [ForeignKey("GameId")]
    public int GameId { get; set; }
    public Game Game { get; set; }
}
