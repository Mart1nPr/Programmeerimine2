using KooliProjekt.Data.Repositories;

namespace KooliProjekt.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IUserRepository Users { get; }
        public IPictureRepository Pictures { get; }
        public IFolderRepository Folders { get; }

        public UnitOfWork(ApplicationDbContext context,
                          IUserRepository usersRepository,
                          IPictureRepository picturesRepository,
                          IFolderRepository foldersRepository)
        {
            _context = context;
            Users = usersRepository;
            Pictures = picturesRepository;
            Folders = foldersRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task Rollback()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}
