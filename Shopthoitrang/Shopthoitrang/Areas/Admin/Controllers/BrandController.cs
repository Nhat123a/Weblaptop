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
    public class BrandController : Controller
    {
        private readonly Datacontext _context;

        public BrandController(Datacontext context)
        {
            _context = context;
        }

        // GET: Admin/Brand
        [Route("quan-li-thuong-hieu-san-pham.html",Name ="thuonghieu")]
        public async Task<IActionResult> Index()
        {
              return _context.Brands != null ? 
                          View(await _context.Brands.ToListAsync()) :
                          Problem("Entity set 'Datacontext.Brands'  is null.");
        }

        // GET: Admin/Brand/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brandModel = await _context.Brands
                .FirstOrDefaultAsync(m => m.ID == id);
            if (brandModel == null)
            {
                return NotFound();
            }

            return View(brandModel);
        }

        // GET: Admin/Brand/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( BrandModel brandModel)
        {
            if (ModelState.IsValid)
            {
                string Brandrm = RemoveDiacriticsa.RemoveDiacritics(brandModel.BrandName);
                string slug = Regex.Replace(Brandrm, @"\s+", "-").ToLower();
                brandModel.Slug= "thuong-hieu-"+slug;
                var brandn = await _context.Brands.FirstOrDefaultAsync(p => p.Slug == brandModel.Slug);
                if (brandn != null)
                {
                    ModelState.AddModelError("", "Thương hiệu  đã tồn tại");
                }
                _context.Add(brandModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brandModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brandModel = await _context.Brands.FindAsync(id);
            if (brandModel == null)
            {
                return NotFound();
            }
            return View(brandModel);
        }

        // POST: Admin/Brand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BrandName,Description,Slug,Status")] BrandModel brandModel)
        {
            if (id != brandModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brandModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandModelExists(brandModel.ID))
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
            return View(brandModel);
        }

        // GET: Admin/Brand/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brandModel = await _context.Brands
                .FirstOrDefaultAsync(m => m.ID == id);
            if (brandModel == null)
            {
                return NotFound();
            }

            return View(brandModel);
        }

        // POST: Admin/Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Brands == null)
            {
                return Problem("Entity set 'Datacontext.Brands'  is null.");
            }
            var brandModel = await _context.Brands.FindAsync(id);
            if (brandModel != null)
            {
                _context.Brands.Remove(brandModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandModelExists(int id)
        {
          return (_context.Brands?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
