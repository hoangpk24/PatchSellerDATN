using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatchSeller.DAL.Models;

public class GameImage
{
    [Key]
    public int GameImageId { get; set; }
    
 
    public string URL { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public bool Delete { get; set; }

    [ForeignKey("GameId")]
    public int GameId { get; set; }
    public Game Game { get; set; }
}
