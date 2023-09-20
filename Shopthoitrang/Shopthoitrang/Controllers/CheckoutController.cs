using Microsoft.AspNetCore.Mvc;
using Shopthoitrang.Data;
using Shopthoitrang.DataContext;
using Shopthoitrang.Models;
using System.Security.Claims;

namespace Shopthoitrang.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly Datacontext datacontext;
		public CheckoutController(Datacontext context)
		{
			this.datacontext = context;
		}
		public async Task<IActionResult> checkout()
		{
			var userMail = User.FindFirstValue(ClaimTypes.Email);
			if (userMail == null)
			{
				RedirectToAction("Login", "Account");
			}
			else
			{
				var orderCode = Guid.NewGuid().ToString();
				var orderItem = new OrderModel();
				orderItem.Order_code = orderCode;
				orderItem.Username = userMail;
				orderItem.status = 1;
				orderItem.Creatiem = DateTime.Now;
				datacontext.Add(orderItem);
				datacontext.SaveChanges();
				RedirectToAction("Index", "Cart");
				List<CartModel> cartItems = HttpContext.Session.Getjson<List<CartModel>>("Cart") ?? new List<CartModel>();
				foreach (var cartmd in cartItems)
				{
					var ordeltail = new OrderdeltailModel();
					ordeltail.ProductId = cartmd.productid;
					ordeltail.Price = cartmd.price;
					ordeltail.Order_code=orderCode;
					ordeltail.Username = userMail;
					ordeltail.Quanity = cartmd.Quanity;
					ordeltail.Description= cartmd.description;
					datacontext.Add(ordeltail);
					datacontext.SaveChanges();
					HttpContext.Session.Remove("Cart");
					RedirectToAction("Index", "Cart");
				}
			}
			return View();
		}
	}
}
