﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;
using KooliProjekt.Services;

namespace KooliProjekt.Controllers
{
    public class FoldersController : Controller
    {
        private readonly IFolderService _folderService;

        public FoldersController(IFolderService folderService)
        {
            _folderService = folderService;
        }

        // GET: Folders
        public async Task<IActionResult> Index(int page = 1)
        {
            var data = await _folderService.List(page, 5);

            return View(data);
        }

        // GET: Folders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var folder = await _folderService.Get(id.Value);
            if (folder == null)
            {
                return NotFound();
            }

            return View(folder);
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
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Creation_date")] Folder folder)
        {
            if (ModelState.IsValid)
            {
                await _folderService.Save(folder);
                return RedirectToAction(nameof(Index));
            }
            return View(folder);
        }

        // GET: Folders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var folder = await _folderService.Get(id.Value);
            if (folder == null)
            {
                return NotFound();
            }
            return View(folder);
        }

        // POST: Folders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Creation_date")] Folder folder)
        {
            if (id != folder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _folderService.Save(folder);
                return RedirectToAction(nameof(Index));
            }
            return View(folder);
        }

        // GET: Folders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var folder = await _folderService.Get(id.Value);
            if (folder == null)
            {
                return NotFound();
            }

            return View(folder);
        }

        // POST: Folders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _folderService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
