using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shopthoitrang.DataContext;

namespace Shopthoitrang.Data.Components
{
    public class BannerViewComponent:ViewComponent
    {
        private readonly Datacontext _context;
        public BannerViewComponent (Datacontext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.Banner.ToListAsync());
        }
    }
}
