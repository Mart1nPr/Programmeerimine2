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

        public async Task DeleteFolderAsync(int id)
        {
            var folder = await _context.Folders.FindAsync(id);
            if (folder != null)
            {
                _context.Folders.Remove(folder);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Folders> GetFolderByIdAsync(int id)
        {
            return await _context.Folders.FindAsync(id);
        }

        public async Task<IEnumerable<Folders>> GetAllFoldersAsync()
        {
            return await _context.Folders.ToListAsync();
        }

        public async Task SaveFolderAsync(Folders folder)
        {
            if (folder.Id == 0)
            {
                _context.Folders.Add(folder);
            }
            else
            {
                _context.Folders.Update(folder);
            }

            await _context.SaveChangesAsync();
        }
    }
}
