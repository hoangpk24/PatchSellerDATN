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
    }
}
