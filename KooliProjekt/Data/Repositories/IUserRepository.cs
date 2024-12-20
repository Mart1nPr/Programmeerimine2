using KooliProjekt.Data;

public interface IUserRepository
{
    Task<Users> Get(int id);
    Task<PagedResult<Users>> List(int page, int pageSize);
    Task Add(Users user);  
    Task Delete(int id);   
}
