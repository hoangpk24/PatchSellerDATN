namespace PatchSeller.Web.DTOs
{
    public class GetDriveResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string MimeType { get; set; }
        public string Link { get; set; }
        public string? LinkDownload { get; set; }
        public double SizeMb { get; set; }
        public DateTime? CreatedTime { get; set; }
        public List<GetDriveResponse> Children { get; set; } = new List<GetDriveResponse>();
    }

    public class FlatFileItem
    {
        public string Name { get; set; }
        public string ParentName { get; set; }
        public string MimeType { get; set; }
        public double SizeMb { get; set; }
        public string LinkDownload { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
