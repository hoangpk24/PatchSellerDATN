namespace PatchSeller.Web.DTOs
{
    public class ScanResponse
    {
        public string Status { get; set; } = string.Empty; // "clean", "malicious", "suspicious"
        public string DetailedUrl { get; set; } = string.Empty;
    }
}
