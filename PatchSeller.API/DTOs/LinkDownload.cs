namespace PatchSeller.API.DTOs
{
    public class LinkDownload
    {
        public int Index { get; set; }
        public string Title { get; set; } = "Link Tải";
        public string Url { get; set; }
        public string Server { get; set; } = "Google Drive";
        public string FileName { get; set; }
        public double Size { get; set; }
    }
}
