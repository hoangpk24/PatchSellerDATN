using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatchSeller.DAL.Models;

public class GameCategory
{
    [Key]
    public int GameCategoryId { get; set; }
    

    
  
    public bool Delete { get; set; }

    [ForeignKey("GameId")]
    public int GameId { get; set; }
    public Game Game { get; set; }
    
    [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
