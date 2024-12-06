using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class UsersRepository
    {
        public UsersRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Users> Get(int id)
        {
            return await DbContext.Users
                .Include(list => list.Folders)
                .Where(list => list.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
