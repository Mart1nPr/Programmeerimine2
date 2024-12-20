using KooliProjekt.Data.Repositories;
using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

public class UserRepository : BaseRepository<Users>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Users> Get(int id)
    {
        return await DbContext.Users.FindAsync(id);
    }

    public async Task<Users> GetByEmail(string email)
    {
        return await DbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<PagedResult<Users>> List(int page, int pageSize)
    {
        return await DbContext.Users.GetPagedAsync(page, pageSize);
    }

    public async Task Add(Users user)
    {
        await DbContext.Users.AddAsync(user);
    }

    public async Task Delete(int id)
    {
        var user = await Get(id);
        if (user != null)
        {
            DbContext.Users.Remove(user);
        }
    }

    public async Task Update(Users user)
    {
        DbContext.Users.Update(user);
        await DbContext.SaveChangesAsync();
    }
}
