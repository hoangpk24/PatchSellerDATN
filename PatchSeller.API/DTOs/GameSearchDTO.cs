namespace PatchSeller.API.DTOs
{
    public class GameSearchDTO
    {
        public int TotalItems { get; set; } = 0;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 0;
        public int PerPage { get; set; } = 12;
        public List<GameDetailDTO> Games { get; set; } = new List<GameDetailDTO>();
    }
}
