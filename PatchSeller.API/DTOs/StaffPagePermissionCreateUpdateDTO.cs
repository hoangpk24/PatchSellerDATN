namespace PatchSeller.API.DTOs
{
    public class StaffPagePermissionCreateDTO
    {
        public int StaffId { get; set; }
        public string PageCode { get; set; } = string.Empty;
        public string PermissionCode { get; set; } = string.Empty;
    }

    public class StaffPagePermissionUpdateDTO
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public string PageCode { get; set; } = string.Empty;
        public string PermissionCode { get; set; } = string.Empty;
    }
}
