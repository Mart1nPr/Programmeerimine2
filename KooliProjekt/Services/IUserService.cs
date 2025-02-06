using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IUserService
    {
        Task<PagedResult<User>> List(int page, int pageSize);
        Task<User> Get(int id);
        Task Save(User list);
        Task Delete(int id);
    }
}