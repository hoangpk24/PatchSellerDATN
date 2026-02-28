namespace GoogleServiceLib
{

    public class DriveNode
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string MimeType { get; set; }
        public string Link { get; set; }
        public string? LinkDownload { get; set; }
        public double SizeMb { get; set; }
        public DateTime? CreatedTime { get; set; }
        public List<DriveNode> Children { get; set; } = new List<DriveNode>();
    }
}