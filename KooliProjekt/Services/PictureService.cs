using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class PictureService : IPictureService
    {
        private readonly ApplicationDbContext _context;

        public PictureService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Picture>> List(int page, int pageSize)
        {
            return await _context.Pictures.GetPagedAsync(page, 5);
        }

        public async Task<Picture> Get(int id)
        {
            return await _context.Pictures.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Picture list)
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
            var picture = await _context.Pictures.FindAsync(id);
            if (picture != null)
            {
                _context.Pictures.Remove(picture);
                await _context.SaveChangesAsync();
            }
        }
    }
}
