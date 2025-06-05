namespace KooliProjekt.PublicAPI.Api
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public T Value { get; set; }

        public bool HasError => !IsSuccess;

        public static Result<T> Success(T value) => new Result<T> { IsSuccess = true, Value = value };
        public static Result<T> Failure(string error) => new Result<T> { IsSuccess = false, Error = error };
        public static Result<T> Ok(T value) => Success(value);
        public static Result<T> Fail(string error) => Failure(error);
    }
}