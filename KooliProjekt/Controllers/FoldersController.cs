using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;

namespace KooliProjekt.Controllers
{
    public class FoldersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoldersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Folders
        public async Task<IActionResult> Index(int page = 1, FoldersSearch searchParams = null)
        {
            // Kui otsingut ei ole määratud, alustage tühi otsing
            if (searchParams == null)
                searchParams = new FoldersSearch();

            // Filtreeri andmed otsingupäringute järgi
            var query = _context.Folders.AsQueryable();

            if (searchParams.Done.HasValue)
            {
                query = query.Where(f => f.Done == searchParams.Done.Value);
            }

            if (!string.IsNullOrEmpty(searchParams.Keyword))
            {
                query = query.Where(f => f.Name.Contains(searchParams.Keyword));
            }

            // Lehekülje täpsustamiseks
            var pagedData = await query.GetPagedAsync(page, 5); // Oletame, et kasutame 5 elementi lehe kohta

            // Täida mudel
            var model = new FoldersIndexModel
            {
                SearchParams = searchParams,
                Data = pagedData
            };

            return View(model);
        }

        // Muud meetodid (Create, Edit, Delete jne) jäävad samaks
    }
}
