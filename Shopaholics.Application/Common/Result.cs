namespace Shopaholics.Application.Common
{
    public class Result 
    {
        public bool IsSuccess { get; protected set; }
        public string[] Errors { get; protected set; } = Array.Empty<string>();

        public static Result Success() => new() { IsSuccess = true };
        public static Result Failure(params string[] errors) => new() { IsSuccess = false, Errors = errors };
    }

    public class Result<T> : Result
    {
        public T? Value { get; private set; }

        
        public static Result<T> Success(T value) => new() { IsSuccess = true, Value = value };
        public static new Result<T> Failure(params string[] errors) => new() { IsSuccess = false, Errors = errors };
    }
}
