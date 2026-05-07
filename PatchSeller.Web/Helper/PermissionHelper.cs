using PatchSeller.Web.DTOs;
using System.Text.RegularExpressions;

namespace PatchSeller.Web.Helper
{
    public static class PermissionHelper
    {
        private static readonly Dictionary<string, string> _pageCodeMapping = new()
        {
            { "/admin/category", "CATEGORY" },
            { "/admin/customer", "CUSTOMER" },
            { "/admin/file", "FILE" },
            { "/admin/game", "GAME" },
            { "/admin/order", "ORDER" },
            { "/admin/patch", "PATCH" },
            { "/admin/permissions", "PERMISSIONS" },
            { "/admin/platform", "PLATFORM" },
            { "/admin/publisher", "PUBLISHER" },
            { "/admin/rank", "RANK" },
            { "/admin/review", "REVIEW" },
            { "/admin/staff", "STAFF" },
            { "/admin/statistical", "STATISTICAL" },
            { "/admin/version", "VERSION" },
            { "/admin/voucher", "VOUCHER" }
        };

        public static Dictionary<string, string> GetModules() => _pageCodeMapping;

        public static string GetPageCode(string currentPath)
        {
            if (string.IsNullOrEmpty(currentPath)) return "";

            var path = currentPath.Split('?')[0].ToLower();
            if (!path.StartsWith("/")) path = "/" + path;

            if (path.Length > 1) path = path.TrimEnd('/');

            var sortedMapping = _pageCodeMapping.OrderByDescending(x => x.Key.Length);

            foreach (var entry in sortedMapping)
            {
                var routeTarget = entry.Key.ToLower();

                // Kiểm tra nếu path hiện tại bắt đầu bằng route cấu hình
                // Ví dụ: "/admin/game/create" bắt đầu bằng "/admin/game" -> Trả về M4
                // Thêm check hoặc để tránh "/admin/games" khớp nhầm với "/admin/game"
                if (path == routeTarget || path.StartsWith(routeTarget + "/"))
                {
                    return entry.Value;
                }
            }

            return "";
        }

        public static bool HasAccess(StaffGetMe? mine, string pageCode, string action)
        {
            if (mine?.PagePermissions == null || string.IsNullOrEmpty(pageCode)) return false;

            var permission = mine.PagePermissions.FirstOrDefault(x => x.PageCode == pageCode);

            if (permission == null) return false;

            return permission.PagePermissions.Contains(action);
        }

        public static string GetActionFromPath(string path)
        {
            var p = path.ToLower();
            if (p.Contains("/create") || p.Contains("/add")) return Constant.Constant.ActionButton.Create;
            if (p.Contains("/edit") || p.Contains("/update")) return Constant.Constant.ActionButton.Edit;
            if (p.Contains("/delete")) return Constant.Constant.ActionButton.Delete;
            return Constant.Constant.ActionButton.Read;
        }
    }
}