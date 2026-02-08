namespace PatchSeller.Web.Constant
{
    public class EndPointApi
    {
        // Access
        public const string AccessLoginStaff = "/Access/LoginStaff";
        public const string AccessLoginCustomer = "/Access/LoginCustomer";
        public const string AccessRegisterCustomer = "/Access/customer-register";
        public const string AccessCheck = "/Access/Check";
        public const string AccessResetPassword = "/Access/reset-password";
        public const string AccessChangePassword = "/Access/change-password";
        public const string StaffChangePassword = "/Access/staff-change-password";

        // Admin
        public static class Admin
        {
            // Category
            public const string CategoryGetAll = "/admin/category/get-all-categories";
            public const string CategoryGetById = "/admin/category/get-category-by-id/:id";
            public const string CategoryCreate = "/admin/category/create";
            public const string CategoryUpdate = "/admin/category/update";
            public const string CategoryDelete = "/admin/category/delete/:id";

            // Platform
            public const string PlatformGetAll = "/admin/platform/get-all-platforms";
            public const string PlatformGetById = "/admin/platform/get-platform-by-id/:id";
            public const string PlatformCreate = "/admin/platform/create";
            public const string PlatformUpdate = "/admin/platform/update";
            public const string PlatformDelete = "/admin/platform/delete/:id";

            // Publisher
            public const string PublisherGetAll = "/admin/publisher/get-all-publishers";
            public const string PublisherGetById = "/admin/publisher/get-publisher-by-id/:id";
            public const string PublisherCreate = "/admin/publisher/create";
            public const string PublisherUpdate = "/admin/publisher/update";
            public const string PublisherDelete = "/admin/publisher/delete/:id";

            // Staff
            public const string StaffGetAll = "/admin/staff/get-all-staffs";
            public const string StaffGetById = "/admin/staff/get-staff-by-id/:id";
            public const string StaffCreate = "/admin/staff/create";
            public const string StaffUpdate = "/admin/staff/update";
            public const string StaffDelete = "/admin/staff/delete/:id";

            // User
            public const string UserGetAll = "/admin/user/get-all-users";
            public const string UserGetById = "/admin/user/get-user-by-id/:id";
            public const string UserCreate = "/admin/user/create";
            public const string UserUpdate = "/admin/user/update";
            public const string UserDelete = "/admin/user/delete/:id";

            // Voucher
            public const string VoucherGetAll = "/admin/discount/get-all-discounts";
            public const string VoucherGetById = "/admin/discount/get-discount-by-id/:id";
            public const string VoucherCreate = "/admin/discount/create";
            public const string VoucherUpdate = "/admin/discount/update";
            public const string VoucherDelete = "/admin/discount/delete/:id";

            // Rank
            public const string RankGetAll = "/admin/rank/get-all-ranks";
            public const string RankGetById = "/admin/rank/get-rank-by-id/:id";
            public const string RankCreate = "/admin/rank/create";
            public const string RankUpdate = "/admin/rank/update";
            public const string RankDelete = "/admin/rank/delete/:id";

            // Game
            public const string GameGetAll = "/admin/game/get-all-games";
            public const string GameDetailGetAll = "/admin/game/get-all-games-detail";
            public const string GameGetById = "/admin/game/get-game-by-id/:id";
            public const string GameDetailGetById = "/admin/game/get-by-id-detail/:id";
            public const string GameCreate = "/admin/game/create";
            public const string GameUpdate = "/admin/game/update";
            public const string GameDelete = "/admin/game/delete/:id";

            // Patch
            public const string PatchGetAll = "/admin/patch/get-all-patches";
            public const string PatchDetailGetAll = "/admin/patch/get-all-patches-detail";
            public const string PatchGetById = "/admin/patch/get-patch-by-id/:id";
            public const string PatchCreate = "/admin/patch/create";
            public const string PatchUpdate = "/admin/patch/update";
            public const string PatchDelete = "/admin/patch/delete/:id";
        }
    }
}
