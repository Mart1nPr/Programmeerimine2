namespace KooliProjekt.WinFormsApp.Api
{
    public interface IApiClient
    {
        Task<Result<List<User>>> List();
        Task<Result> Save(User user);
        Task<Result> Delete(int id);
    }
}