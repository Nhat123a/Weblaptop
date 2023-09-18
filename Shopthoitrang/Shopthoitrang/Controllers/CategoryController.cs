using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopthoitrang.DataContext;
using Shopthoitrang.Models;

namespace Shopthoitrang.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Datacontext _datacontext;
        public CategoryController(Datacontext contex)
        {
            _datacontext = contex;
        }
        public async Task<IActionResult> Index(string Slug ="")
        {
            CategoryModelcs category = _datacontext.Categories.Where(x=>x.Slug == Slug).FirstOrDefault();
           if (category == null)
            {
                return RedirectToAction("Index");
            }
            var productbyCategory = _datacontext.Product.Where(y=>y.CategoryId==category.ID);
            return View( await productbyCategory.OrderByDescending(y=>y.ProductId).ToListAsync());
        }
    }
}
