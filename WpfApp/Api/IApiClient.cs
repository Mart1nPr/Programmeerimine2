namespace WpfApp.Api
{
    public interface IApiClient
    {
        Task<List<User>> List();
        Task Save(User list);
        Task Delete(int id);
    }
}
