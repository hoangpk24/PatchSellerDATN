namespace PatchSeller.Web.Models
{
    public class CustomerChangePasswordModel
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewHashPassword { get; set; } = string.Empty;
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
