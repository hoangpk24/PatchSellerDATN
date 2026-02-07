namespace PatchSeller.API.DTOs
{
    public class PatchDetailDTO
    {
        public int PatchId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string UpdateBy { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int GameId { get; set; }

        public List<PatchImageBasicDTO> PatchImages { get; set; } = new List<PatchImageBasicDTO>();
        public List<PatchVersionBasicDTO> PatchVersions { get; set; } = new List<PatchVersionBasicDTO>();
    }

    public class PatchImageBasicDTO
    {
        public int PatchImageId { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsThumbnail { get; set; }
    }

    public class PatchVersionBasicDTO
    {
        public int PatchVersionId { get; set; }
        public string WorkWithGameVersion { get; set; }
        public string VersionName { get; set; }
        public string Links { get; set; }
        public long FileSize { get; set; }
        public string ExtractionPassword { get; set; }
        public string InstallationGuide { get; set; }
        public string Changelog { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
