namespace PatchSeller.API.DTOs
{

    public class GameCreateEditDTO
    {
        public int GameId { get; set; } // Cái này thêm cũng được, không thêm cũng được, DB tự sinh key nhé, thêm vào cho nó nhìn chuẩn cấu trúc =)
        public string Title { get; set; } = string.Empty;
        public string Developer { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public int Status { get; set; }
        public bool Delete { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;      
        public int PublisherId { get; set; } = -1; // -1 là Unknow, update lại db sẽ có
        public List<int> CategoryIds { get; set; } = new List<int>();
        public List<GamePlatformInputDTO> Platforms { get; set; } = new List<GamePlatformInputDTO>();
    }

    public class GamePlatformInputDTO
    {
        public int PlatformId { get; set; }
        public string? Description { get; set; }
    }
}
