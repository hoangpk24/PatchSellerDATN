using System;

namespace PatchSeller.API.DTOs
{

    public class PatchCreateUpdateDTO
    {
        public int PatchId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string UpdateBy { get; set; }
        public int Status { get; set; }
        public bool Delete { get; set; }
        public string? ThumbnailLink { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int GameId { get; set; }
    }
}
