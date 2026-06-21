using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TempleERP.Data;
using TempleERP.Models;

namespace TempleERP.Controllers
{
    public class PoojaMastersController : Controller
    {
        private readonly TempleDbContext _context;

        public PoojaMastersController(TempleDbContext context)
        {
            _context = context;
        }
        // GET: PoojaMasters
        public async Task<IActionResult> Index()
        {
            var poojaMasters = await _context.PoojaMaster
                .Include(p => p.Category)
                .Where(x => x.IsActive)
                .ToListAsync();

            return View(poojaMasters);
        }

        // GET: PoojaMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poojaMaster = await _context.PoojaMaster
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PoojaId == id);

            if (poojaMaster == null)
            {
                return NotFound();
            }

            return View(poojaMaster);
        }

        // GET: PoojaMasters/Create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(
                _context.Categories.Where(x => x.IsActive),
                "CategoryId",
                "CategoryName");

            return View();
        }

        // POST: PoojaMasters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PoojaMaster poojaMaster)
        {
            if (ModelState.IsValid)
            {
                poojaMaster.CreatedDate = DateTime.Now;
                poojaMaster.IsActive = true;

                _context.PoojaMaster.Add(poojaMaster);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(
                _context.Categories.Where(x => x.IsActive),
                "CategoryId",
                "CategoryName",
                poojaMaster.CategoryId);

            return View(poojaMaster);
        }

        // GET: PoojaMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poojaMaster = await _context.PoojaMaster.FindAsync(id);

            if (poojaMaster == null)
            {
                return NotFound();
            }

            ViewBag.CategoryId = new SelectList(
                _context.Categories.Where(x => x.IsActive),
                "CategoryId",
                "CategoryName",
                poojaMaster.CategoryId);

            return View(poojaMaster);
        }

        // POST: PoojaMasters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PoojaMaster poojaMaster)
        {
            if (id != poojaMaster.PoojaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.PoojaMaster.Update(poojaMaster);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoojaMasterExists(poojaMaster.PoojaId))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(
                _context.Categories.Where(x => x.IsActive),
                "CategoryId",
                "CategoryName",
                poojaMaster.CategoryId);

            return View(poojaMaster);
        }

        // GET: PoojaMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poojaMaster = await _context.PoojaMaster
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PoojaId == id);

            if (poojaMaster == null)
            {
                return NotFound();
            }

            return View(poojaMaster);
        }

        // POST: PoojaMasters/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var poojaMaster = await _context.PoojaMaster.FindAsync(id);

            if (poojaMaster != null)
            {
                poojaMaster.IsActive = false;

                _context.PoojaMaster.Update(poojaMaster);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PoojaMasterExists(int id)
        {
            return _context.PoojaMaster.Any(e => e.PoojaId == id);
        }
    }
}
