namespace PatchSeller.Web.Models
{
    public class GameModel
    {
        public int GameId { get; set; } = -1;
        public string Title { get; set; } = string.Empty;
        public string Developer { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public DateTime? ReleaseDate { get; set; }
        public int Status { get; set; }
        public bool Delete { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public int PublisherId { get; set; } = -1;
        public List<int> CategoryIds { get; set; } = new List<int>();
        public List<GamePlatformInputModel> Platforms { get; set; } = new List<GamePlatformInputModel>();
    }
    public class GamePlatformInputModel
    {
        public int PlatformId { get; set; }
        public string? Description { get; set; }
    }
}
