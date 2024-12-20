using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class PicturesRepository : BaseRepository<Pictures>, IPicturesRepository
    {
        public PicturesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Pictures> GetById(int id)
        {
            return await DbContext.Pictures.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PagedResult<Pictures>> List(int page, int pageSize)
        {
            return await DbContext.Pictures.GetPagedAsync(page, pageSize);
        }
    }
}

