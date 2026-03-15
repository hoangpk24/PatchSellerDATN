namespace PatchSeller.Launcher.DTOs
{
    public class GameDetailResponse
    {
        public int GameId { get; set; }
        public string Title { get; set; }
        public string Developer { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int PublisherId { get; set; }

        public PublisherBasicDTO Publisher { get; set; }
        public List<PlatformBasicDTO> Platforms { get; set; } = new List<PlatformBasicDTO>();
        public List<CategoryBasicDTO> Categories { get; set; } = new List<CategoryBasicDTO>();
        public List<PatchBasicDTO> Patches { get; set; } = new List<PatchBasicDTO>();
        public List<GameImageBasicDTO> GameImages { get; set; } = new List<GameImageBasicDTO>();
        public List<GameDetailResponse> GameSameCategories { get; set; } = new List<GameDetailResponse>();
    }

    public class PublisherBasicDTO
    {
        public int PublisherId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }

    public class PlatformBasicDTO
    {
        public int PlatformId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }

    public class CategoryBasicDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }

    public class PatchBasicDTO
    {
        public int PatchId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string UpdateBy { get; set; } = string.Empty;
        public int Status { get; set; }
        public string ThumbnailLink { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public List<PatchVersionBasicDTO> PatchVersions { get; set; } = new List<PatchVersionBasicDTO>();
    }

    public class GameImageBasicDTO
    {
        public int GameImageId { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }

    public class GameForHomeResponse
    {
        public List<GameDetailResponse> LstCommingSoon { get; set; } = new List<GameDetailResponse>();
        public List<GameDetailResponse> LstHot { get; set; } = new List<GameDetailResponse>();
        public List<GameDetailResponse> LstNew { get; set; } = new List<GameDetailResponse>();
    }
}
