namespace PatchSeller.API.DTOs
{
    public class PatchVersionCreateDTO
    {
        public string WorkWithGameVersion { get; set; } = string.Empty;
        public string VersionName { get; set; } = string.Empty;
        public string Links { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string? ExtractionPassword { get; set; }
        public string? InstallationGuide { get; set; }
        public string? Changelog { get; set; }
        public int Status { get; set; }
        public string? Note { get; set; }
        public int PatchId { get; set; }
        public int StaffId { get; set; }
    }
}
