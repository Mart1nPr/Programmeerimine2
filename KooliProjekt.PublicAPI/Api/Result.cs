using System.Collections.Generic;

namespace KooliProjekt.PublicAPI.Api
{
    public class Result
    {
        public Dictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();
        public bool IsSuccess => Errors.Count == 0;
        public bool HasError => !IsSuccess;

        public static Result Success() => new Result();
        public static Result Failure(string error)
        {
            var result = new Result();
            result.AddError("_", error);
            return result;
        }

        public void AddError(string propertyName, string errorMessage)
        {
            if (!Errors.ContainsKey(propertyName))
                Errors[propertyName] = new List<string>();

            Errors[propertyName].Add(errorMessage);
        }
    }
}
