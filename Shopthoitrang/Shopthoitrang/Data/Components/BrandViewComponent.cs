using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopthoitrang.DataContext;

namespace Shopthoitrang.Data.Components
{
	public class BrandViewComponent:ViewComponent
	{
		private readonly Datacontext _datacontext;
		public BrandViewComponent(Datacontext datacontext)
		{
			_datacontext = datacontext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View(await _datacontext.Brands.ToListAsync());
		}
	}
}
