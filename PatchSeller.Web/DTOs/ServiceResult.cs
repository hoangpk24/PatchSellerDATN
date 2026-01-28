namespace PatchSeller.Web.DTOs
{
    public class ServiceResult<T>
    {
        public T Data { get; init; }

        public bool IsSuccess { get; init; }

        public string ErrorCode { get; init; } = string.Empty;

        public string ErrorMessage { get; init; } = string.Empty;

        public string StatusCode { get; init; } = string.Empty;

        public static ServiceResult<T> Success(T data) => new ServiceResult<T>
        {
            Data = data,
            IsSuccess = true,
            ErrorCode = string.Empty,
            ErrorMessage = string.Empty,
            StatusCode = string.Empty
        };

        public static ServiceResult<T> Failure(string errorCode, string errorMess, string statusCode) => new ServiceResult<T>
        {
            Data = default,
            IsSuccess = false,
            ErrorCode = errorCode,
            ErrorMessage = errorMess,
            StatusCode = statusCode
        };
    }
}
