using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PatchSeller.DAL.Models;

public class DownloadLog
{
    [Key]
    public int DownloadLogId { get; set; }
    
    public DateTime DownloadedAt { get; set; }

    [ForeignKey("PatchVersionId")]
    public int PatchVersionId { get; set; }
    [JsonIgnore]
    public PatchVersion? PatchVersion { get; set; }
    
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
}
