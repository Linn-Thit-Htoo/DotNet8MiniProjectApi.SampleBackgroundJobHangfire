namespace DotNet8MiniProjectApi.SampleBackgroundJobHangfire.Models;

public class Result<T>
{
    public T Data { get; set; }
    public string Message { get; set; }
    public EnumStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public bool IsError => !IsSuccess;

    public static Result<T> SuccessResult(
        string message = "Success.",
        EnumStatusCode statusCode = EnumStatusCode.Success
    ) =>
        new()
        {
            Message = message,
            StatusCode = statusCode,
            IsSuccess = true
        };

    public static Result<T> SuccessResult(
        T data,
        string message = "Success.",
        EnumStatusCode statusCode = EnumStatusCode.Success
    ) =>
        new()
        {
            Data = data,
            Message = message,
            StatusCode = statusCode,
            IsSuccess = true
        };

    public static Result<T> FailureResult(
        string message = "Fail.",
        EnumStatusCode statusCode = EnumStatusCode.BadRequest
    ) =>
        new()
        {
            Message = message,
            StatusCode = statusCode,
            IsSuccess = false
        };

    public static Result<T> FailureResult(Exception ex) =>
        new()
        {
            Message = ex.ToString(),
            StatusCode = EnumStatusCode.InternalServerError,
            IsSuccess = false
        };

    public static Result<T> NotFoundResult(string message = "No Data Found.") =>
        new()
        {
            Message = message,
            IsSuccess = false,
            StatusCode = EnumStatusCode.NotFound
        };
}
