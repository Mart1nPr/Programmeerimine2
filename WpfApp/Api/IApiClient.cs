namespace WpfApp.Api
{
    public interface IApiClient
    {
        Task<Result<List<User>>> List();
        Task<Result> Save(User list);
        Task<Result> Delete(int id);
    }
}
