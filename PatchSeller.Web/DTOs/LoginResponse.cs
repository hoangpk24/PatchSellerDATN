namespace PatchSeller.Web.DTOs
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public bool LoginSuccess { get; set; }
        public bool FirstLogin { get; set; } = false;
    }
}
