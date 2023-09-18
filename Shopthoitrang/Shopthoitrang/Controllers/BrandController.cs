using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopthoitrang.DataContext;
using Shopthoitrang.Models;

namespace Shopthoitrang.Controllers
{
	public class BrandController : Controller
	{
		private readonly Datacontext _datacontext;
		public BrandController(Datacontext datacontext)
		{
			_datacontext = datacontext;
		}
		
		public async Task<IActionResult> Index(string Slug="")
		{
			BrandModel brand = _datacontext.Brands.Where(x=>x.Slug==Slug).FirstOrDefault();
			if (brand == null)
			{
				return RedirectToAction("Index");
			}
			var productBrand = _datacontext.Product.Where(x => x.BrandID == brand.ID);
			return View( await productBrand.OrderByDescending(x=>x.ProductId).ToListAsync());
		}
	}
}
