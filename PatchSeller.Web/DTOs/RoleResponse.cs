namespace PatchSeller.Web.DTOs
{
    public class RoleResponse
    {
        public int Id { get; set; }
        public string? RoleName { get; set; }
        public string? PagesPermission { get; set; }
        public bool Status { get; set; } = true;
    }

    public class PagePermissionItemResponse
    {
        public string PageCode { get; set; } = "";
        public string PagePermissions { get; set; } = "";
    }
}
