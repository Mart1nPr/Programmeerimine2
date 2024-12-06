namespace KooliProjekt.Data.Repositories
{
    public interface IUsersRepository
    {
        Task<Users> Get(int id);
        Task<PagedResult<Users>> List(int page, int pageSize);
        Task Save(Users item);
        Task Delete(int id);
    }
}
