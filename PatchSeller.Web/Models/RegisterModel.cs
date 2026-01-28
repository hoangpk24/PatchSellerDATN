using System.ComponentModel.DataAnnotations.Schema;

namespace PatchSeller.Web.Models
{
    public class RegisterModel
    {
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public double RewardPoint { get; set; } = 0;
        public int Status { get; set; } = 1;
        public bool Delete { get; set; } = false;
        public int? RankId { get; set; } = 1;
    }
}
