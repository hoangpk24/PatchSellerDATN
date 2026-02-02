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

        // Admin
        public static class Admin
        {
            // Category
            public const string CategoryGetAll = "/admin/category/get-all-categories";
            public const string CategoryGetById = "/admin/category/get-category-by-id/:id";
            public const string CategoryCreate = "/admin/category/create";
            public const string CategoryUpdate = "/admin/category/update";
            public const string CategoryDelete = "/admin/category/delete/:id";
        }
    }
}
