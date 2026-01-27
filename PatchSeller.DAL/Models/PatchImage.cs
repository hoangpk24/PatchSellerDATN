using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatchSeller.DAL.Models;

public class PatchImage
{
    [Key]
    public int PatchImageId { get; set; }

    

    public string URL { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsThumbnail { get; set; }
    public bool Delete { get; set; }

    [ForeignKey("PatchId")]
    public int? PatchId { get; set; }
    public Patch? Patch { get; set; }
    
    [ForeignKey("PatchVersionId")]
    public int? PatchVersionId { get; set; }
    public PatchVersion? PatchVersion { get; set; }
}
