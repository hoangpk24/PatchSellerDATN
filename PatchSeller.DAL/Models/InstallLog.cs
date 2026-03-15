using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Models
{
    public class InstallLog
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PatchId { get; set; }
        public int PatchVersionId { get; set; }
        public string? BIOSSerialNumber { get; set; }
        public DateTime? InstallDate { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

    }
}
