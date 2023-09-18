using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopthoitrang.DataContext;

namespace Shopthoitrang.Data.Components
{
    public class CategoriesViewComponent:ViewComponent
    {
        private readonly Datacontext _datacontext;
        public CategoriesViewComponent(Datacontext context)
        {
            _datacontext = context;
        }
        public async Task<IViewComponentResult> InvokeAsync() => View(await _datacontext .Categories.ToListAsync());
    }
}
