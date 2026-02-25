using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PatchSeller.DAL.Models;

public class PatchVersion
{
    [Key]
    public int PatchVersionId { get; set; }
    public string WorkWithGameVersion { get; set; }
    public string VersionName { get; set; }
    public string Links { get; set; }
    public long FileSize { get; set; }
    public string? ExtractionPassword { get; set; }
    public string? InstallationGuide { get; set; }
    public string? Changelog { get; set; }
    public int Status { get; set; }
    public string? Note { get; set; }
    public DateTime CreateAt { get; set; }
    public bool Delete { get; set; }

    [ForeignKey("PatchId")]
    public int PatchId { get; set; }
    public Patch? Patch { get; set; }
    
    [ForeignKey("StaffId")]
    public int StaffId { get; set; }
    [JsonIgnore]
    public Staff? Staff { get; set; }
    [JsonIgnore]
    public ICollection<DownloadLog>? DownloadLogs { get; set; }
    [JsonIgnore]
    public ICollection<PatchImage>? PatchImages { get; set; }
}
