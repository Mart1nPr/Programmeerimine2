using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Folders.ToListAsync());
        }

        // GET: Folders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var folders = await _context.Folders
                .FirstOrDefaultAsync(m => m.ID == id);
            if (folders == null)
            {
                return NotFound();
            }

            return View(folders);
        }

        // GET: Folders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Folders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,Creation_date")] Folders folders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(folders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(folders);
        }

        // GET: Folders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var folders = await _context.Folders.FindAsync(id);
            if (folders == null)
            {
                return NotFound();
            }
            return View(folders);
        }

        // POST: Folders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Creation_date")] Folders folders)
        {
            if (id != folders.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(folders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoldersExists(folders.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(folders);
        }

        // GET: Folders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var folders = await _context.Folders
                .FirstOrDefaultAsync(m => m.ID == id);
            if (folders == null)
            {
                return NotFound();
            }

            return View(folders);
        }

        // POST: Folders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var folders = await _context.Folders.FindAsync(id);
            if (folders != null)
            {
                _context.Folders.Remove(folders);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoldersExists(int id)
        {
            return _context.Folders.Any(e => e.ID == id);
        }
    }
}
