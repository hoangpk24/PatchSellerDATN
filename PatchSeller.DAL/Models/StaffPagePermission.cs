using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Models
{
    public class StaffPagePermission
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("StaffId")]
        public int StaffId { get; set; }
        public Staff? Staff { get; set; }
        [ForeignKey("PagePermissionId")]
        public int PagePermissionId { get; set; }
        public PagePermission? PagePermission { get; set; }
        [Required]
        [StringLength(1)]
        [Column(TypeName = "char(1)")]
        public string PermissionCode { get; set; }

    }
}
