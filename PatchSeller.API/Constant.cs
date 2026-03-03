namespace PatchSeller.API
{
    public class Constant
    {
        public static class ErrorCode
        {
            // Required Fields
            public const string EmailOrPhoneRequired = "email_phone_required";
            public const string CurrentPasswordFailed = "current_password_failed";
            public const string EmailOrUsernameRequired = "email_username_required";
            public const string DataRequired = "data_required";
            public const string NotEnoughRewardPoint = "not_enought_point";

            // Already Exists
            public const string EmailOrUsernameAlreadyExit = "email_username_already_exit";
            public const string UserNameOrEmailAlreadyExit = "username_email_already_exit";
            public const string NameAlreadyExit = "name_already_exit";
            public const string PointAlreadyExit = "point_already_exit";
            public const string CodeAlreadyExit = "code_already_exit";
            public const string DataAlreadyExit = "data_already_exit";

            // Authentication & Authorization
            public const string Unauthorized = "unauthorized";
            public const string TokenExpired = "token_expired";
            public const string InvalidToken = "invalid_token";

            // Validation Errors
            public const string PasswordIsTheSame = "password_same";

            // Not Found Errors
            public const string InvalidPatchId = "invalid_patch_id";
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

        public static class OrderStatus
        {
            public const int OrderWaitingForPayment = 2; // Chờ thanh toán
            public const int OrderCanceled = 3; // Huỷ
            public const int OrderDone = 1; // 
            public const int OrderPaymentExpired = 4; // Hết thời gian chờ thanh toán

        }

        public static class DiscountType
        {
            public const string Fixed = "Fixed";
            public const string Percent = "Percent";

        }

        public static class PaymentStatus
        {
            public const string WaitingForPayment = "Đang chờ thanh toán";
            public const string PaymentCompleted = "Đã thanh toán";
            public const string PaymentCanceled = "Đã huỷ thanh toán";

        }
    }
}
