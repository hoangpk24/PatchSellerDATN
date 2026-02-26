namespace PatchSeller.API.DTOs
{
    public class ReviewDetailDTO
    {
        public int ReviewId { get; set; }

        public string UserName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int Overall { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int UserId { get; set; }
        public int PatchId { get; set; }
        public string PatchName { get; set; } = string.Empty;
    }
}
