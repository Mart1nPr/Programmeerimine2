namespace KooliProjekt.WinFormsApp.Api
{
    public class Result
    {
        public bool Success { get; set; }
        public string Error { get; set; }

        public bool IsSuccess => Success;

        public static Result Ok() => new Result { Success = true };
        public static Result Fail(string error) => new Result { Success = false, Error = error };
    }
}
