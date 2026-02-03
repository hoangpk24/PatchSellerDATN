namespace PatchSeller.API
{
    public class Constant
    {
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
            public const string NameAlreadyExit = "name_already_exit";
            public const string UserNameOrEmailAlreadyExit = "username_email_already_exit";

            public const string NotFound = "not_found";
            public const string DataNotFound = "data_not_found";

            public const string InvalidData = "invalid_data";
            public const string DataRequired = "data_required";
           

            public const string OtherError = "other_error";
            public const string DatabaseError = "database_error";

            public const string OutOfStock = "out_of_stock";
            public const string ProductInActiveOrder = "product_in_active_order";

        }
    }
}
