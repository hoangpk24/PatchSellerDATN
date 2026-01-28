namespace PatchSeller.Web.Models
{
    public class LoginModel
    {
        public string UserName { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
    }
}
