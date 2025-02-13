using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class FolderService : IFolderService
    {
        private readonly ApplicationDbContext _context;

        public FolderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Folder>> List(int page, int pageSize, FoldersSearch search = null)
        {
            var query = _context.Folders.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search?.Keyword))
            {
                query = query.Where(folder => folder.Name.Contains(search.Keyword) || folder.Description.Contains(search.Keyword));
            }
        }

            return await query.GetPagedAsync(page, pageSize);
        }

        public async Task<Folder> Get(int id)
        {
            return await _context.Folders.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Folder folder)
        {
            if (folder.Id == 0)
            {
                _context.Add(folder);
            }
            else
            {
                _context.Update(folder);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var folder = await _context.Folders.FindAsync(id);
            if (folder != null)
            {
                _context.Folders.Remove(folder);
                await _context.SaveChangesAsync();
            }
        }
    }
}
