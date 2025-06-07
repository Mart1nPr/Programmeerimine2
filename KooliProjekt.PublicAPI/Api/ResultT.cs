namespace KooliProjekt.PublicAPI.Api
{
    public class Result<T> : Result
    {
        public T Value { get; set; }

        public static Result<T> Success(T value) => new Result<T> { Value = value };
        public static Result<T> Failure(string error)
        {
            var result = new Result<T>();
            result.AddError("_", error);
            return result;
        }
    }
}