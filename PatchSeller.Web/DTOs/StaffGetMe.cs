namespace PatchSeller.Web.DTOs
{
    public class StaffGetMe
    {
        public int StaffId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public List<PagePermissionItemResponse> PagePermissions { get; set; } = new List<PagePermissionItemResponse>();

        public bool IsStaff { get; set; } = false;
    }
}
