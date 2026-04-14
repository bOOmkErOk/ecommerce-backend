namespace E_Commerce.Domain.Results;

public class Result
{
    public bool IsSuccess { get; set; }
    public Errors? Error { get; set; }
    public bool IsFailure => !IsSuccess;

    protected Result(bool succes, Errors? error)
    {
        IsSuccess = succes;
        Error = error;
    }
    public static Result Success() => new Result(true, null);
    public static Result Failure(Errors error) => new Result(false, error);
}

public sealed class Result<T> : Result
{
    public T? Value { get; set; }
    protected Result(bool succes, T? value, Errors? error) : base(succes, error)
    {
        Value = value;
    }
    public static Result<T> Success(T value) => new Result<T>(true, value, null);
    public static new Result<T> Failure(Errors error) => new Result<T>(false, default, error);
}
