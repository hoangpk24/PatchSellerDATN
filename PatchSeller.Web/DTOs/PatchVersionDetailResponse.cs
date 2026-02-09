namespace PatchSeller.Web.DTOs
{
    public class PatchVersionDetailResponse
    {
        public int PatchVersionId { get; set; }
        public string WorkWithGameVersion { get; set; } = string.Empty;
        public string VersionName { get; set; } = string.Empty;
        public string Links { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string? ExtractionPassword { get; set; }
        public string? InstallationGuide { get; set; }
        public string? Changelog { get; set; }
        public int Status { get; set; }
        public string? Note { get; set; }
        public DateTime CreateAt { get; set; }
        public bool Delete { get; set; }
        public int PatchId { get; set; }
        public int StaffId { get; set; }

        // Thông tin bổ sung khi lấy dữ liệu
        public int GameId { get; set; }
        public string GameName { get; set; } = string.Empty;
        public string StaffName { get; set; } = string.Empty;
        public string PatchName { get; set; } = string.Empty;
        public string PatchDescription { get; set; } = string.Empty;
        public string UploaderFullName { get; set; } = string.Empty;
        public List<PatchImageBasicResponse> PatchImages { get; set; } = new List<PatchImageBasicResponse>();
    }

    public class PatchImageBasicResponse
    {
        public int PatchImageId { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsThumbnail { get; set; }
    }
}
