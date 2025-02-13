using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class PictureService : IPictureService
    {
        private readonly ApplicationDbContext _context;

        public PictureService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Picture>> List(int page, int pageSize, PicturesSearch search = null)
        {
            var query = _context.Pictures.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search?.Keyword))
            {
                query = query.Where(picture => picture.Name.Contains(search.Keyword) || picture.Context.Contains(search.Keyword));
            }

            return await query.GetPagedAsync(page, pageSize);
        }

        public async Task<Picture> Get(int id)
        {
            return await _context.Pictures.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Picture picture)
        {
            if (picture.Id == 0)
            {
                _context.Add(picture);
            }
            else
            {
                _context.Update(picture);
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
