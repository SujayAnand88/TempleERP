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
    public class ItemsController : Controller
    {
        private readonly TempleDbContext _context;

        public ItemsController(TempleDbContext context)
        {
            _context = context;
        }
        // GET: Items
        public async Task<IActionResult> Index()
        {
            try
            {
                var items = await _context.Items
                    .Include(i => i.Category)
                    .Where(x => x.IsActive)
                    .ToListAsync();

                return View(items);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var items = await _context.Items
                    .Include(i => i.Category)
                    .FirstOrDefaultAsync(m => m.ItemId == id);

                if (items == null)
                {
                    return NotFound();
                }

                return View(items);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            try
            {
                ViewBag.CategoryId = new SelectList(
                    _context.Categories.Where(x => x.IsActive),
                    "CategoryId",
                    "CategoryName");

                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Items items)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    items.CreatedDate = DateTime.Now;
                    items.IsActive = true;

                    _context.Items.Add(items);

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

                ViewBag.CategoryId = new SelectList(
                    _context.Categories.Where(x => x.IsActive),
                    "CategoryId",
                    "CategoryName",
                    items.CategoryId);

                return View(items);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var items = await _context.Items.FindAsync(id);

                if (items == null)
                {
                    return NotFound();
                }

                ViewBag.CategoryId = new SelectList(
                    _context.Categories.Where(x => x.IsActive),
                    "CategoryId",
                    "CategoryName",
                    items.CategoryId);

                return View(items);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Items items)
        {
            try
            {
                if (id != items.ItemId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    _context.Items.Update(items);

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }

                ViewBag.CategoryId = new SelectList(
                    _context.Categories.Where(x => x.IsActive),
                    "CategoryId",
                    "CategoryName",
                    items.CategoryId);

                return View(items);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var item = await _context.Items
                    .Include(i => i.Category)
                    .FirstOrDefaultAsync(m => m.ItemId == id);

                if (item == null)
                {
                    return NotFound();
                }

                return View(item);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // POST: Items/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var item = await _context.Items.FindAsync(id);

                if (item != null)
                {
                    item.IsActive = false;

                    _context.Items.Update(item);

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        private bool ItemsExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
