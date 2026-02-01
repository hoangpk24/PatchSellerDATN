namespace PatchSeller.Web.Models
{
    public class ChangePasswordModel
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewHashPassword { get; set; } = string.Empty;
    }
}
