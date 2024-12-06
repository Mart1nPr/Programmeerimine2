namespace KooliProjekt.Data.Repositories
{
    public interface IUsersRepository
    {
        Task<Users> Get(int id);
<<<<<<< HEAD
        Task<PagedResult<Users>> List(int page, int pageSize);
        Task Save(Users item);
=======
        Task<IEnumerable<Users>> GetAll();
        Task Save(Users user);
>>>>>>> d741a0326c07780bfd56b54fabb4e0b7705beee3
        Task Delete(int id);
    }
}
