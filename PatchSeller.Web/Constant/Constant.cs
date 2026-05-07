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
        public const string MainImages = "/uploads/images/games/";
        public const string MainPatchImages = "/uploads/images/patches/";
        public const string DefaultImages = "/assets/images/default-image.png";

        // Discount type
        public const string DiscountTypePercent = "Percent";
        public const string DiscountTypeFixed = "Amount";

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
            // Required Fields
            public const string EmailOrPhoneRequired = "email_phone_required";
            public const string CurrentPasswordFailed = "current_password_failed";
            public const string EmailOrUsernameRequired = "email_username_required";
            public const string DataRequired = "data_required";

            // Already Exists
            public const string EmailOrUsernameAlreadyExit = "email_username_already_exit";
            public const string UserNameOrEmailAlreadyExit = "username_email_already_exit";
            public const string NameAlreadyExit = "name_already_exit";
            public const string CodeAlreadyExit = "code_already_exit";

            // Authentication & Authorization
            public const string Unauthorized = "unauthorized";
            public const string TokenExpired = "token_expired";
            public const string InvalidToken = "invalid_token";

            // Validation Errors
            public const string PasswordIsTheSame = "password_same";

            // Not Found Errors
            public const string CustomerNotFound = "customer_not_found";
            public const string StaffNotFound = "staff_not_found";
            public const string CustomerNotFoundWidthEmailOrPhone = "customer_not_found_with_email_or_phone";
            public const string EmailOrUsernameNotFound = "email_username_not_found";
            public const string EmailOrPhoneNotFound = "email_phone_not_found";
            public const string NotFound = "not_found";
            public const string DataNotFound = "data_not_found";

            // Invalid Data Errors
            public const string InvalidData = "invalid_data";

            // Other Errors
            public const string OtherError = "other_error";
            public const string DatabaseError = "database_error";
        }

        public static readonly Dictionary<string, string> Errors = new Dictionary<string, string>
        {
            { ErrorCode.EmailOrUsernameAlreadyExit, "Email hoặc tên tài khoản này đã được sử dụng." },
            { ErrorCode.UserNameOrEmailAlreadyExit, "Tên tài khoản hoặc email này đã tồn tại." },
            { ErrorCode.NameAlreadyExit, "Tên này đã được sử dụng, vui lòng chọn tên khác." },
            { ErrorCode.CodeAlreadyExit, "Mã này đã được sử dụng, vui lòng chọn mã khác." },
            
            { ErrorCode.EmailOrPhoneRequired, "Vui lòng nhập email hoặc số điện thoại của bạn." },
            { ErrorCode.EmailOrUsernameRequired, "Vui lòng nhập email hoặc tên đăng nhập." },
            { ErrorCode.DataRequired, "Thông tin bắt buộc còn thiếu, vui lòng kiểm tra lại." },

            { ErrorCode.CurrentPasswordFailed, "Mật khẩu hiện tại không chính xác." },
            { ErrorCode.PasswordIsTheSame, "Mật khẩu mới không được trùng với mật khẩu hiện tại." },

            { ErrorCode.Unauthorized, "Bạn không có quyền truy cập vào chức năng này." },
            { ErrorCode.InvalidToken, "Phiên làm việc không hợp lệ." },
            { ErrorCode.TokenExpired, "Phiên làm việc đã hết hạn, vui lòng đăng nhập lại." },

            { ErrorCode.EmailOrPhoneNotFound, "Email hoặc số điện thoại không tồn tại trên hệ thống." },
            { ErrorCode.EmailOrUsernameNotFound, "Email hoặc tên đăng nhập không tồn tại." },
            { ErrorCode.CustomerNotFound, "Không tìm thấy thông tin khách hàng." },
            { ErrorCode.StaffNotFound, "Không tìm thấy thông tin nhân viên." },
            { ErrorCode.CustomerNotFoundWidthEmailOrPhone, "Không tìm thấy khách hàng với thông tin đã cung cấp." },
            { ErrorCode.NotFound, "Yêu cầu không tìm thấy." },
            { ErrorCode.DataNotFound, "Dữ liệu không tồn tại trên hệ thống." },

            { ErrorCode.InvalidData, "Dữ liệu cung cấp không hợp lệ." },

            { ErrorCode.DatabaseError, "Lỗi kết nối cơ sở dữ liệu. Vui lòng thử lại sau." },
            { ErrorCode.OtherError, "Đã có lỗi không xác định xảy ra." },
            { "", "Đã có lỗi xảy ra. Vui lòng liên hệ quản trị viên." },
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

        public static class StatusDefault
        {
            public const int Active = 1;
            public const int InActive = 0;
            public const int CommingSoon = 2;
        }

        public static class OrderStatus
        {
            public const int OrderWaitingForPayment = 2; // Chờ thanh toán
            public const int OrderCanceled = 3; // Huỷ
            public const int OrderDone = 1; // Hoàn thành
        }

        public static class ActionButton
        {
            public const string Create = "C";
            public const string Edit = "U";
            public const string Delete = "D";
            public const string Read = "R";
        }
    }
}
