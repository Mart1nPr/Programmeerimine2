using KooliProjekt.Data;

public interface IUserService
{
    Task<IEnumerable<Users>> GetAllUsersAsync();
    Task<Users> GetUserByIdAsync(int id);
    Task SaveUserAsync(Users user);
    Task DeleteUserAsync(int id);
    Task<PagedResult<Users>> List(int page, int pageSize);  // New method
}
