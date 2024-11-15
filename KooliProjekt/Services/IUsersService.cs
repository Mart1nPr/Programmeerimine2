using KooliProjekt.Data;

namespace YourApp.Services
{
    public interface IUserService
    {
        // Get all users
        Task<List<User>> GetAllUsersAsync();

        // Get user by ID
        Task<User> GetUserByIdAsync(int id);

        // Add new user
        Task AddUserAsync(User user);

        // Update an existing user
        Task UpdateUserAsync(User user);

        // Delete user by ID
        Task DeleteUserAsync(int id);
    }
}
