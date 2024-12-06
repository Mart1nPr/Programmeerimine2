using KooliProjekt.Data.Repositories;

namespace KooliProjekt.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IUsersRepository Users { get; }

        public UnitOfWork(ApplicationDbContext context, IUsersRepository usersRepository)
        {
            _context = context;
            Users = usersRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
