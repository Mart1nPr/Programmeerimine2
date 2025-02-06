using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class FolderService : IFolderService
    {
        private readonly ApplicationDbContext _context;

        public FolderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Folder>> List(int page, int pageSize)
        {
            return await _context.Folders.GetPagedAsync(page, 5);
        }

        public async Task<Folder> Get(int id)
        {
            return await _context.Folders.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Folder list)
        {
            if (list.Id == 0)
            {
                _context.Add(list);
            }
            else
            {
                _context.Update(list);
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
