namespace PatchSeller.Web.Models
{
    public class ReviewModel
    {
        public int ReviewId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Overall { get; set; }
        public int UserId { get; set; }
        public int PatchId { get; set; }
        public int Status { get; set; }
    }
}
