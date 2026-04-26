using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Models
{
    public class PagePermission
    {
        [Key]
        public int Id { get; set; }
        public string? PageCode { get; set; }
        public string? PageRoute { get; set; }
        public string AvailablePermissions { get; set; } = "C,R,U,D";
        public string DefaultPermissions { get; set; } = "C,R,U,D";
        public ICollection<StaffPagePermission>? StaffPagePermissions { get; set; }
    }
}
