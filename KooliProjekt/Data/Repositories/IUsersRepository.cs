namespace KooliProjekt.Data.Repositories
{
    public interface IUsersRepository
    {
        Task<Users> Get(int id);
        Task<IEnumerable<Users>> GetAll();
        Task Save(Users user);
        Task Delete(int id);
    }
}
