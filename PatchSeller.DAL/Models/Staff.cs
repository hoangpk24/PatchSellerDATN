using System.ComponentModel.DataAnnotations;

namespace PatchSeller.DAL.Models;

public class Staff
{
    [Key]
    public int StaffId { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    [MaxLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public ICollection<StaffPagePermission>? StaffPagePermissions { get; set; }
    public ICollection<PatchVersion>? PatchVersions { get; set; }
    public ICollection<ActionLog>? ActionLogs { get; set; }
}
