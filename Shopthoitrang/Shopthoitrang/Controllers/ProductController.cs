using Microsoft.AspNetCore.Mvc;
using Shopthoitrang.DataContext;

namespace Shopthoitrang.Controllers
{
    
    public class ProductController : Controller
    {
        private readonly Datacontext _datacontext;
        public ProductController(Datacontext datacontext) {
            _datacontext = datacontext;
        }
        [Route("/san-pham-lap-top.html",Name ="product")]
        public IActionResult Index()
        {
            var product = _datacontext.Product.ToList();
            return View(product);
        }
        public async Task<IActionResult> Details(int id)
        {
            var Detail = _datacontext.Product.FirstOrDefault(p => p.ProductId == id);

            if (Detail == null)
            {
                return NotFound(); // Trả về trang 404 nếu không tìm thấy sản phẩm
            }

            var relatedPosts = _datacontext.Product
                .Where(p => p.CategoryId == Detail.CategoryId && p.ProductId != id)
                .ToList();

            ViewBag.lienquan = relatedPosts;
            return View(Detail);
        }

    }
}
