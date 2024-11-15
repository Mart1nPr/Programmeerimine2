// Services/FolderService.cs
using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace YourApp.Services
{
    public class FolderService : IFolderService
    {
        private readonly ApplicationDbContext _context;

        public FolderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Folder>> GetAllFoldersAsync()
        {
            return await _context.Folders.ToListAsync();
        }

        public async Task<Folder> GetFolderByIdAsync(int id)
        {
            return await _context.Folders.FindAsync(id);
        }

        public async Task AddFolderAsync(Folder folder)
        {
            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFolderAsync(Folder folder)
        {
            _context.Folders.Update(folder);
            await _context.SaveChangesAsync();
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
    }
}

