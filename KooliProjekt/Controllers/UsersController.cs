using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;

namespace KooliProjekt.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index(int page = 1, string keyword = "", bool? isDone = null)
        {
            var usersSearch = new UsersSearch
            {
                Keyword = keyword,
                IsDone = isDone
            };

            var query = _context.Users.AsQueryable();

            // Rakenda otsingukriteeriumid
            if (!string.IsNullOrEmpty(usersSearch.Keyword))
            {
                query = query.Where(u => u.Email.Contains(usersSearch.Keyword) || u.Name.Contains(usersSearch.Keyword));
            }

            if (usersSearch.IsDone.HasValue)
            {
                query = query.Where(u => u.IsDone == usersSearch.IsDone.Value);  // Oletame, et IsDone on Users mudelis
            }

            var result = await query.GetPagedAsync(page, 5);  // GetPagedAsync võtab arvesse pagineerimist

            var model = new UsersIndexModel
            {
                SearchParams = usersSearch,
                Data = result
            };

            return View(model);
        }

        // Muud meetodid (Create, Edit, Delete, Details) jäävad samaks
    }
}
