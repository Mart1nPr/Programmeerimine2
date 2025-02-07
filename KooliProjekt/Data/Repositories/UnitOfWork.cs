
namespace KooliProjekt.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context,
            IUserRepository userRepository,
            IFolderRepository folderRepository,
            IPictureRepository pictureRepository)
        {
            _context = context;

            UserRepository = userRepository;
            FolderRepository = folderRepository;
            PictureRepository = pictureRepository;
        }

        public IUserRepository UserRepository { get; }
        public IFolderRepository FolderRepository { get; }
        public IPictureRepository PictureRepository { get; }

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
