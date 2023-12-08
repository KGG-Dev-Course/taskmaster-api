using taskmaster_api.Data.DTOs.Interface;

namespace taskmaster_api.Data.DTOs
{
    public class CoreActionResult : ICoreActionResult
    {
        protected CoreActionResult(bool isSucess, bool isException, string message, string errorCode)
        {
            this.IsSuccess = isSucess;
            this.IsException = isException;
            this.ErrorMessage = message;
            this.ErrorCode = errorCode;
        }

        protected CoreActionResult(bool isSucess, string message, string errorCode)
        {
            this.IsException = false;
            this.IsSuccess = isSucess;
            this.ErrorMessage = message;
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Error message that can be sent to the Client
        /// </summary>
        public string ErrorMessage { get; }
        public string ErrorCode { get; }
        public bool IsFailure => IsSuccess == false;
        public bool IsSuccess { get; }
        public bool IsException { get; set; }
        public static ICoreActionResult Failure(
            string errorMesage,
            string errorCode)
        {
            return new CoreActionResult(false, errorMesage, errorCode);
        }

        public static ICoreActionResult Exception(Exception ex)
        {
            return new CoreActionResult(
               false,
               true,
               ex.Message,
               "COMMON.UNHANDLED_EXCEPTION");
        }

        public static ICoreActionResult Success()
        {
            return new CoreActionResult(true, String.Empty, String.Empty);
        }

        public static ICoreActionResult Ignore(string errorMesage, string errorCode)
        {
            return new CoreActionResult(false, errorMesage, errorCode);
        }

    }

    public class CoreActionResult<T> : CoreActionResult, ICoreActionResult<T>
    {
        public CoreActionResult(bool isSuccess, string errorMesage, T data, string errorCode)
            : base(isSuccess, errorMesage, errorCode)
        {
            Data = data;
        }

        public T Data { get; }

        public static ICoreActionResult<T> Failure(string errorMesage)
        {
            return new CoreActionResult<T>(false, errorMesage, default(T), "");
        }

        public static ICoreActionResult<T> Exception(Exception ex)
        {
            return new CoreActionResult<T>(
                false,
                ex.Message,
                default(T),
                "COMMON.UNHANDLED_EXCEPTION");
        }


        public static ICoreActionResult<T> Failure(string errorMessage, string errorCode)
        {
            return new CoreActionResult<T>(false, errorMessage, default(T), errorCode);
        }

        public static ICoreActionResult<T> Success(T data)
        {
            return new CoreActionResult<T>(true, string.Empty, data, string.Empty);
        }
    }
}
