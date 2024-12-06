using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
<<<<<<< HEAD
    public class UsersRepository
=======
    public class UsersRepository : BaseRepository<Users>, IUsersRepository
>>>>>>> d741a0326c07780bfd56b54fabb4e0b7705beee3
    {
        public UsersRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Users> Get(int id)
        {
            return await DbContext.Users
<<<<<<< HEAD
                .Include(list => list.Folders)
                .Where(list => list.Id == id)
                .FirstOrDefaultAsync();
        }
=======
                .Where(user => user.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            return await DbContext.Users.ToListAsync();
        }

        public async Task Save(Users user)
        {
            if (user.Id == 0)
            {
                await DbContext.Users.AddAsync(user);
            }
            else
            {
                DbContext.Users.Update(user);
            }
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await DbContext.Users.FindAsync(id);
            if (user != null)
            {
                DbContext.Users.Remove(user);
                await DbContext.SaveChangesAsync();
            }
        }
>>>>>>> d741a0326c07780bfd56b54fabb4e0b7705beee3
    }
}
