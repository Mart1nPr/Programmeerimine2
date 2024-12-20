using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class FoldersRepository : BaseRepository<Folders>, IFoldersRepository
    {
        public FoldersRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Folders> GetById(int id)
        {
            return await DbContext.Folders.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<PagedResult<Folders>> List(int page, int pageSize)
        {
            return await DbContext.Folders.GetPagedAsync(page, pageSize);
        }
    }
}
