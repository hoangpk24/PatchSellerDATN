namespace PatchSeller.Web.DTOs
{
    public class UploadFileResponse
    {
        public string LinkDownload { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public long? Size { get; set; }
        public double SizeMb { get; set; }
    }

    public class CheckFileResponse
    {
        public bool Exists { get; set; }
        public string Name { get; set; } = string.Empty;
        public double SizeMb { get; set; }
    }
}
