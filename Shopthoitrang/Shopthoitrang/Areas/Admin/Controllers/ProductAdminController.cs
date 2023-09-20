using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopthoitrang.Data.Removemarks;
using Shopthoitrang.DataContext;
using Shopthoitrang.Models;

namespace Shopthoitrang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductAdminController : Controller
    {
        private readonly Datacontext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductAdminController(Datacontext context, IWebHostEnvironment environment)
        {
            _context = context;
            _webHostEnvironment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var datacontext = _context.Product.Include(p => p.Brand).Include(p => p.Category);
            return View(await datacontext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var productModel = await _context.Product
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        public IActionResult Create()
        {
            ViewData["BrandID"] = new SelectList(_context.Brands, "ID", "BrandName");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "ID", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Slug,Description,Price,Status,CategoryId,BrandID,Image")] ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                string productNameWithoutDiacritics = RemoveDiacriticsa.RemoveDiacritics(productModel.ProductName);
                productModel.Slug = Regex.Replace(productNameWithoutDiacritics, @"\s+", "-").ToLower();

                // Kiểm tra xem Slug đã tồn tại trong cơ sở dữ liệu hay chưa
                var productSlug = await _context.Product.FirstOrDefaultAsync(p => p.Slug == productModel.Slug);
                if (productSlug != null)
                {
                    ModelState.AddModelError("", "Sản phẩm đã tồn tại");
                }
                if(productModel.Imgload != null)
                {
                    string path = "images/product-details";

					string Imgdir = Path.Combine(_webHostEnvironment.WebRootPath,path);
                    string Nameimg=Guid.NewGuid().ToString()+"-"+productModel.Imgload.FileName;
                    string Pathimg = Path.Combine(Imgdir, Nameimg);
					using (var fileStream = new FileStream(Pathimg, FileMode.Create))
					{
						await productModel.Imgload.CopyToAsync(fileStream);
					}
                    productModel.Image = Nameimg;
                }
                _context.Add(productModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandID"] = new SelectList(_context.Brands, "ID", "BrandName", productModel.BrandID);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "ID", "CategoryName", productModel.CategoryId);
            return View(productModel);
        }
        //Hàm bỏ dấu
        


        // GET: Admin/ProductAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var productModel = await _context.Product.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }
            ViewData["BrandID"] = new SelectList(_context.Brands, "ID", "BrandName", productModel.BrandID);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "ID", "CategoryName", productModel.CategoryId);
            return View(productModel);
        }

        // POST: Admin/ProductAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Slug,Description,Price,Status,CategoryId,BrandID,Image")] ProductModel productModel)
        {
            if (id != productModel.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductModelExists(productModel.ProductId))
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
            ViewData["BrandID"] = new SelectList(_context.Brands, "ID", "BrandName", productModel.BrandID);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "ID", "CategoryName", productModel.CategoryId);
            return View(productModel);
        }

        // GET: Admin/ProductAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var productModel = await _context.Product
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // POST: Admin/ProductAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'Datacontext.Product'  is null.");
            }
            var productModel = await _context.Product.FindAsync(id);
            if (productModel != null)
            {
                _context.Product.Remove(productModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductModelExists(int id)
        {
          return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
