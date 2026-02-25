namespace PatchSeller.Web.Models
{
    public class PatchModel
    {
        public int PatchId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; } 
        public string Description { get; set; } = string.Empty;
        public string UpdateBy { get; set; } = string.Empty;
        public int Status { get; set; }
        public bool Delete { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? ThumbnailLink { get; set; }
        public int GameId { get; set; }
    }
}
