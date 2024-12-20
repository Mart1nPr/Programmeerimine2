using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;

namespace KooliProjekt.Controllers
{
    public class PicturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PicturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pictures
        public async Task<IActionResult> Index(int page = 1, string keyword = "", bool? isDone = null)
        {
            var picturesSearch = new PicturesSearch
            {
                Keyword = keyword,
                IsDone = isDone
            };

            var query = _context.Pictures.AsQueryable();

            // Rakendame otsingukriteeriumid
            if (!string.IsNullOrEmpty(picturesSearch.Keyword))
            {
                query = query.Where(p => p.Name.Contains(picturesSearch.Keyword) || p.Context.Contains(picturesSearch.Keyword));
            }

            if (picturesSearch.IsDone.HasValue)
            {
                query = query.Where(p => p.IsDone == picturesSearch.IsDone.Value); // Oletame, et on IsDone väli, mis määrab, kas pilt on tehtud või mitte
            }

            var result = await query.GetPagedAsync(page, 5);  // Lehekülje andmed

            var model = new PicturesIndexModel
            {
                SearchParams = picturesSearch,
                Data = result
            };

            return View(model);
        }

        // Muud meetodid (Create, Edit, Delete, Details) jäävad samaks
    }
}
