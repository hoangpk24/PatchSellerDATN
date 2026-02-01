namespace PatchSeller.Web.Constant
{
    public class Constant
    {
        // Page title
        public const string PageTitleUser = "Iteam -";
        public const string PageTitleAdmin = "Iteam Management -";

        // Name local
        public const string TokenNameLocalStorage = "token";
        public const string TokenExpiredLocalStorage = "expired";
        public const string UserInfoLocalStorage = "userInfo";
        public const string UserFirstLoginLocalStorage = "firstLogin";

        // Image default
        public const string DefaultImages = "/Assets/Images/default-image.png";

        // Regex
        public static class Regex
        {
            public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[^A-Za-z0-9]).{8,16}$";
            public const string PhoneNumber = @"(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b";
            public const string HexColor = @"^#([A-Fa-f0-9]{8}|[A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";
            public const string Pattern = @"^\d+$";
            public const string SKU = @"^[A-Z0-9-_]+$";
        }

        // Data store
        public static class CascadingNameParams
        {
            public const string Mine = "mine";
            public const string Token = "token";
        }

        public static class ErrorCode
        {
            public const string EmailOrPhoneRequired = "email_phone_required";
            public const string CurrentPasswordFailed = "current_password_failed";
            public const string EmailOrUsernameRequired = "email_username_required";
            public const string EmailOrPhoneNotFound = "email_phone_not_found";
            public const string EmailOrUsernameNotFound = "email_username_not_found";
            public const string EmailOrUsernameAlreadyExit = "email_username_already_exit";
            public const string CustomerNotFound = "customer_not_found";
            public const string CustomerNotFoundWidthEmailOrPhone = "customer_not_found_with_email_or_phone";
            public const string Unauthorized = "unauthorized";
            public const string TokenExpired = "token_expired";
            public const string InvalidToken = "invalid_token";

            public const string NotFound = "not_found";
            public const string DataNotFound = "data_not_found";

            public const string InvalidData = "invalid_data";
            public const string DataRequired = "data_required";


            public const string OtherError = "other_error";
            public const string DatabaseError = "database_error";

            public const string OutOfStock = "out_of_stock";
            public const string ProductInActiveOrder = "product_in_active_order";

        }

        public static readonly Dictionary<string, string> Errors = new Dictionary<string, string>
        {
            { ErrorCode.EmailOrUsernameAlreadyExit, "Email hoặc tên tài khoản này đã được sử dụng." },
            { ErrorCode.EmailOrPhoneRequired, "Hãy nhập email của bạn." },
            { ErrorCode.CurrentPasswordFailed, "Mật khẩu hiện tại không đúng." },
            { ErrorCode.EmailOrUsernameRequired, "Hãy nhập email hoặc tên đăng nhập của bạn." },
            { ErrorCode.EmailOrPhoneNotFound, "Email không tồn tại trong hệ thống." },
            { ErrorCode.EmailOrUsernameNotFound, "Email hoặc tên đăng nhập không tồn tại trong hệ thống." },
            { ErrorCode.CustomerNotFound, "Khách hàng không tồn tại." },
            { ErrorCode.CustomerNotFoundWidthEmailOrPhone, "Khách hàng không tồn tại." },
            { ErrorCode.Unauthorized, "unauthorized" },
            { ErrorCode.InvalidToken, "Token invalid" },
            { ErrorCode.TokenExpired, "Token expired" },
            { ErrorCode.NotFound, "Không tìm thấy." },
            { ErrorCode.DataNotFound, "Không có dữ liệu." },
            { ErrorCode.InvalidData, "Dữ liệu không hợp lệ." },
            { ErrorCode.DataRequired, "Thiếu dữ liệu gửi đi." },
            { ErrorCode.DatabaseError, "Lỗi database." },
            { ErrorCode.OtherError, "Đã có lỗi xảy ra." },
            { ErrorCode.OutOfStock, "Số lượng đạt tối đa."},
            { "", "Đã có lỗi xảy ra." },
        };

        public static class ErrorSatusCode
        {
            public const int BadRequest = 400;
            public const int Fobidden = 403;
            public const int NotFound = 404;
            public const int Internal = 500;
        }

        public static class Pagination
        {
            public const int DefaultPage = 1;
            public const int DefaultPerPage = 20;
        }

        public static class Role
        {
            public const string Admin = "Admin";
            public const string Manager = "Manager";
            public const string Customer = "Customer";
        }
    }
}
