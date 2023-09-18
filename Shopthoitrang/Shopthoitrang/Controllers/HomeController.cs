using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopthoitrang.DataContext;
using Shopthoitrang.Models;
using System.Diagnostics;

namespace Shopthoitrang.Controllers
{
    public class HomeController : Controller
    {
        private readonly Datacontext _datacontext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,Datacontext context)
        {
            _logger = logger;
            _datacontext = context;
        }

        public IActionResult Index()
        {
			var product = _datacontext.Product.Include("Brand").ToList();
			return View(product);
        }
        [Route("page-404.html",Name ="404")]
         public async Task<IActionResult> page() {
			return View();
		}
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}