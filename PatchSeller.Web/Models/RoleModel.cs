namespace PatchSeller.Web.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string? RoleName { get; set; }
        public string? PagesPermission { get; set; }
        public bool Status { get; set; } = true;
    }
}
