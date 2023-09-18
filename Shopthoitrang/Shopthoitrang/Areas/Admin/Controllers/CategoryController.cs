using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopthoitrang.Data.Removemarks;
using Shopthoitrang.DataContext;
using Shopthoitrang.Models;

namespace Shopthoitrang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly Datacontext _context;

        public CategoryController(Datacontext context)
        {
            _context = context;
        }

        // GET: Admin/Category
        [Route("quan-li-danh-muc-san-pham.html",Name ="Category")]
        public async Task<IActionResult> Index()
        {
              return _context.Categories != null ? 
                          View(await _context.Categories.ToListAsync()) :
                          Problem("Entity set 'Datacontext.Categories'  is null.");
        }

        // GET: Admin/Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categoryModelcs = await _context.Categories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (categoryModelcs == null)
            {
                return NotFound();
            }

            return View(categoryModelcs);
        }

        // GET: Admin/Category/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CategoryModelcs categoryModelcs)
        {
            if (ModelState.IsValid)
            {
                string brandRemove = RemoveDiacriticsa.RemoveDiacritics(categoryModelcs.CategoryName);
                categoryModelcs.Slug = Regex.Replace(brandRemove, @"\s+", "-").ToLower();

                var Categoryn = await _context.Categories.FirstOrDefaultAsync(p => p.Slug == categoryModelcs.Slug);
                if (Categoryn != null)
                {
                    ModelState.AddModelError("", "Danh mục đã tồn tại");
                }
                _context.Add(categoryModelcs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryModelcs);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categoryModelcs = await _context.Categories.FindAsync(id);
            if (categoryModelcs == null)
            {
                return NotFound();
            }
            return View(categoryModelcs);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CategoryName,Slug,Status,Description")] CategoryModelcs categoryModelcs)
        {
            if (id != categoryModelcs.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryModelcs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryModelcsExists(categoryModelcs.ID))
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
            return View(categoryModelcs);
        }

        // GET: Admin/Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categoryModelcs = await _context.Categories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (categoryModelcs == null)
            {
                return NotFound();
            }

            return View(categoryModelcs);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'Datacontext.Categories'  is null.");
            }
            var categoryModelcs = await _context.Categories.FindAsync(id);
            if (categoryModelcs != null)
            {
                _context.Categories.Remove(categoryModelcs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryModelcsExists(int id)
        {
          return (_context.Categories?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
