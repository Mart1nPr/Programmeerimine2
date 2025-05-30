namespace KooliProjekt.WinFormsApp.Api
{
    public class Result<T> : Result
    {
        public T Value { get; set; }

        public static Result<T> Ok(T value) => new Result<T> { Success = true, Value = value };
        public static new Result<T> Fail(string error) => new Result<T> { Success = false, Error = error };
    }
}
