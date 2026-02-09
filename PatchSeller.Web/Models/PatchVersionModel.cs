namespace PatchSeller.Web.Models
{
    public class PatchVersionModel
    {
        public int PatchVersionId { get; set; } = 0;
        public string WorkWithGameVersion { get; set; } = string.Empty;
        public string VersionName { get; set; } = string.Empty;
        public string Links { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string? ExtractionPassword { get; set; } = string.Empty;
        public string? InstallationGuide { get; set; } = string.Empty;
        public string? Changelog { get; set; }
        public int Status { get; set; }
        public string? Note { get; set; }
        public int PatchId { get; set; } = 0;
        public int StaffId { get; set; } = 0;
    }
}
