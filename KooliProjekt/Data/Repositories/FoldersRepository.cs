using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class FoldersRepository : BaseRepository<Folders>, IFoldersRepository
    {
        public FoldersRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Folders> Get(int id)
        {
            return await DbContext.TodoLists
                .Include(list => list.Items)
                .Where(list => list.Id == id)
                .FirstOrDefaultAsync();
        }

        public override async Task<PagedResult<Folders>> List(int page, int pageSize)
        {
            return await DbContext.TodoLists
                .OrderBy(list => list.Title)
                .GetPagedAsync(page, pageSize);
        }
    }
}
