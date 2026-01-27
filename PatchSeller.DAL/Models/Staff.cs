using System.ComponentModel.DataAnnotations;

namespace PatchSeller.DAL.Models;

public class Staff
{
    [Key]
    public int StaffId { get; set; }

    public string FullName { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }

    [MaxLength(15)]
    public string PhoneNumber { get; set; }
    public string Role { get; set; }

    public ICollection<PatchVersion> PatchVersions { get; set; }
    public ICollection<ActionLog> ActionLogs { get; set; }
}
