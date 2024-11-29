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
    public class PicturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PicturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pictures
        public async Task<IActionResult> Index(int page = 1)
        {
            return View(await _context.Pictures.GetPagedAsync(page, 5));
        }

        // GET: Pictures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pictures = await _context.Pictures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pictures == null)
            {
                return NotFound();
            }

            return View(pictures);
        }

        // GET: Pictures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pictures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Folder_id,ImageLink,Name,Context,Creation_date,Latitude,Longitude")] Pictures pictures)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pictures);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pictures);
        }

        // GET: Pictures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pictures = await _context.Pictures.FindAsync(id);
            if (pictures == null)
            {
                return NotFound();
            }
            return View(pictures);
        }

        // POST: Pictures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Folder_id,ImageLink,Name,Context,Creation_date,Latitude,Longitude")] Pictures pictures)
        {
            if (id != pictures.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pictures);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PicturesExists(pictures.Id))
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
            return View(pictures);
        }

        // GET: Pictures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pictures = await _context.Pictures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pictures == null)
            {
                return NotFound();
            }

            return View(pictures);
        }

        // POST: Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pictures = await _context.Pictures.FindAsync(id);
            if (pictures != null)
            {
                _context.Pictures.Remove(pictures);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PicturesExists(int id)
        {
            return _context.Pictures.Any(e => e.Id == id);
        }
    }
}
