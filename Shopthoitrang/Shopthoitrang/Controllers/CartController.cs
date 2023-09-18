using Microsoft.AspNetCore.Mvc;
using Shopthoitrang.Data;
using Shopthoitrang.DataContext;
using Shopthoitrang.Models;
using Shopthoitrang.Models.ViewModels;

namespace Shopthoitrang.Controllers
{
    public class CartController : Controller
    {
        private readonly Datacontext _datacontext;
        public CartController(Datacontext datacontext)
        {
            _datacontext = datacontext;
        }
        [Route("gio-hang.html",Name ="cart")]
        public IActionResult Index()
        {
            List<CartModel> cartItem = HttpContext.Session.Getjson<List<CartModel>>("Cart")??new List<CartModel> ();
            CartviewModel cartview = new()
            {
                cartitem = cartItem,
                TotalGrand = cartItem.Sum(x => x.Quanity * x.price)

            };
            return View(cartview);
        }
        public async Task<IActionResult> Addcart(int ID)
        {
            ProductModel product = await _datacontext.Product.FindAsync(ID);
			List<CartModel> cartItems = HttpContext.Session.Getjson<List<CartModel>>("Cart") ?? new List<CartModel>();
            CartModel cart = cartItems.Where(x => x.productid == ID).FirstOrDefault();
            if (cart == null)
            {
                cartItems.Add(new CartModel(product));
            }
            else
            {
                cart.Quanity += 1;
            }
            HttpContext.Session.Setjson("Cart", cartItems);
            return Redirect(Request.Headers["Referer"].ToString());
		}
        public async Task<IActionResult> giam(int id)
        {
            List<CartModel> cartItems = HttpContext.Session.Getjson<List<CartModel>>("Cart");
            CartModel cart = cartItems.Where(x=>x.productid== id).FirstOrDefault();
            if(cart.Quanity>1)
            {
                --cart.Quanity;
            }
            else
            {
				return RedirectToAction("Index");
			}
            if (cartItems.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.Setjson("Cart", cartItems);
            }
            return RedirectToAction("Index");

		}
		public async Task<IActionResult> tang(int id)
		{
			List<CartModel> cartitem = HttpContext.Session.Getjson<List<CartModel>>("Cart");
			CartModel cart = cartitem.Where(x => x.productid == id).FirstOrDefault();

			if (cart != null)
			{
				if (cart.Quanity >= 1)
				{
					cart.Quanity++; // Tăng số lượng sản phẩm
				}
				

				// Cập nhật danh sách giỏ hàng trong phiên làm việc của người dùng
				HttpContext.Session.Setjson("Cart", cartitem);
			}

			return RedirectToAction("Index");
		}
		public async Task<IActionResult> xoa(int id)
		{
			List<CartModel> cartitem = HttpContext.Session.Getjson<List<CartModel>>("Cart");

			// Loại bỏ chỉ mục có productid tương ứng với id
			var itemToRemove = cartitem.SingleOrDefault(x => x.productid == id);
			if (itemToRemove != null)
			{
				cartitem.Remove(itemToRemove);
				HttpContext.Session.Setjson("Cart", cartitem); // Lưu lại giỏ hàng sau khi loại bỏ mục
			}

			return RedirectToAction("Index");
		}


	}
}
