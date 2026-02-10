namespace PatchSeller.Web.DTOs
{
    public class MineResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; }  = string.Empty;
        public int cartId { get; set; }

        public DateTime? ExpirationTime { get; set; }

        public bool IsExpired { get; set; }
    }
}
