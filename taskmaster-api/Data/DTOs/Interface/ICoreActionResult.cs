namespace taskmaster_api.Data.DTOs.Interface
{
    public interface ICoreActionResult
    {
        bool IsSuccess { get; }
        string ErrorMessage { get; }
        string ErrorCode { get; }
    }

    public interface ICoreActionResult<T> : ICoreActionResult
    {
        T Data { get; }
    }
}
