namespace PatchSeller.Web.Constant
{
    public static class RouterConfig
    {

        public const string AccessDenied = "/access-denined";
        public const string NotFound = "/not-found";

        public static class Customer
        {
            // Home
            public const string Home = "/";

            // Search
            public const string Search = "/search";

            // Auth
            public const string SignUp = "/register";
            public const string Login = "/login";
            public const string ForgotPassword = "/forgot-password";
            public const string ChangePassword = "/change-password";

            // Voucher
            public const string voucher = "/voucher";

            // Profile
            public const string Profile = "/profile";

            // Game
            public const string Game = "/game/:id";
            // Cart
            public const string CartDetail = "/cart/detail";

            // Checkout
            public const string Checkout = "/checkout";

            // Order
            public const string Orders = "/orders";
            public const string OrderDetail = "/orders/:id/detail";
            public const string PaymentSuccess = "/order/payment-success";
            public const string PaymentCancelled = "/order/payment-cancelled";
            public const string SearchOrder = "/orders/search";

            // Wishlist
            public const string wishlists = "/wishlist";
        }

        public static class Admin
        {
            // Root
            public const string Root = "/admin/";

            // Home
            public const string Home = "/admin/home";

            // Auth
            public const string Login = "/admin/login";

            // Game
            public const string Game = "/admin/game";
            public const string CreateGame = "/admin/game/create";
            public const string EditGame = "/admin/game/:id/edit";

            // Publisher
            public const string Publisher = "/admin/publisher";
            public const string CreatePublisher = "/admin/publisher/create";
            public const string EditPublisher = "/admin/publisher/:id/edit";

            // Category
            public const string Category = "/admin/category";
            public const string CreateCategory = "/admin/category/create";
            public const string EditCategory = "/admin/category/:id/edit";

            // Platform
            public const string Platform = "/admin/platform";
            public const string CreatePlatform = "/admin/platform/create";
            public const string EditPlatform = "/admin/platform/:id/edit";

            // Patch
            public const string Patch = "/admin/patch";
            public const string CreatePatch = "/admin/patch/create";
            public const string EditPatch = "/admin/patch/:id/edit";

            // Patch Version
            public const string Version = "/admin/version";
            public const string CreateVersion = "/admin/version/create";
            public const string EditVersion = "/admin/version/:id/edit";

            // Voucher
            public const string Voucher = "/admin/voucher";
            public const string CreateVoucher = "/admin/voucher/create";
            public const string EditVoucher = "/admin/voucher/:id/edit";

            // Staff
            public const string Staff = "/admin/staff";
            public const string CreateStaff = "/admin/staff/create";
            public const string EditStaff = "/admin/staff/:id/edit";

            // Customer
            public const string Customer = "/admin/customer";
            public const string CreateCustomer = "/admin/customer/create";
            public const string EditCustomer = "/admin/customer/:id/edit";

            // Order
            public const string Order = "/admin/order";
            public const string OrderDetail = "/admin/order/:id/detail";

            // Statistical
            public const string Statistical = "/admin/statistical";

            // Payment Done
            public const string PaymentSuccess = "/admin/order/payment-success";
            public const string PaymentCancelled = "/admin/order/payment-cancelled";

            // Rank
            public const string Rank = "/admin/rank";
            public const string CreateRank = "/admin/rank/create";
            public const string EditRank = "/admin/rank/:id/edit";

            // Review
            public const string Review = "/admin/review";
        }
    }
}
