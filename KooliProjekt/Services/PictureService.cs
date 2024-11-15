// Services/PictureService.cs
using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace YourApp.Services
{
    public class PictureService : IPictureService
    {
        private readonly ApplicationDbContext _context;

        public PictureService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pictures>> GetAllPicturesAsync()
        {
            return await _context.Pictures.ToListAsync();
        }

        public async Task<Pictures> GetPictureByIdAsync(int id)
        {
            return await _context.Pictures.FindAsync(id);
        }

        public async Task AddPictureAsync(Pictures picture)
        {
            _context.Pictures.Add(picture);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePictureAsync(Pictures picture)
        {
            _context.Pictures.Update(picture);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePictureAsync(int id)
        {
            var picture = await _context.Pictures.FindAsync(id);
            if (picture != null)
            {
                _context.Pictures.Remove(picture);
                await _context.SaveChangesAsync();
            }
        }
    }
}
