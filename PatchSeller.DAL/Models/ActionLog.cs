using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatchSeller.DAL.Models;

public class ActionLog
{
    [Key]
    public int ActionLogId { get; set; }
    
    public string Action { get; set; }
    public string TargetTable { get; set; }
    public string TargetID { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public DateTime CreatedAt { get; set; }

    [ForeignKey("StaffId")]
    public int StaffId { get; set; }
    public Staff Staff { get; set; }
}
