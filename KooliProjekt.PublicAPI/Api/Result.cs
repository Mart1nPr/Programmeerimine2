namespace KooliProjekt.PublicAPI.Api
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public bool HasError => !IsSuccess;
        public static Result Success() => new Result { IsSuccess = true };
        public static Result Failure(string error) => new Result { IsSuccess = false, Error = error };
    }
}
