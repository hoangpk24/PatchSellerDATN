namespace Pro219.API.DTOs
{
    public class PagePermissionDTO
    {
        public string PageCode { get; set; } = string.Empty;
        public string PagePermissions { get; set; } = string.Empty;
    }

    public class StaffMeDetailDTO
    {
        public int StaffId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public List<PagePermissionDTO> PagePermissions { get; set; } = new List<PagePermissionDTO>();
    }
}
