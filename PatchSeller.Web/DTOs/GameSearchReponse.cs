namespace PatchSeller.Web.DTOs
{
    public class GameSearchResponse
    {
        public int TotalItems { get; set; } = 0;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 0;
        public int PerPage { get; set; } = 12;
        public List<GameDetailResponse> Games { get; set; } = new List<GameDetailResponse>();
    }
}
