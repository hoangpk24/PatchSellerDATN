using PatchSeller.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatchSeller.Web.Models
{
    public class DiscountModel
    {
        public int DiscountId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string DiscountType { get; set; } = string.Empty;
        public double Value { get; set; }
        public double? MaxDiscount { get; set; }
        public double? MinOrderValue { get; set; }
        public int UsageLimit { get; set; }
        public int LimitPerUser { get; set; }
        public int UsedCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Status { get; set; }
        public bool Delete { get; set; }
        public int RankId { get; set; }
    }
}
