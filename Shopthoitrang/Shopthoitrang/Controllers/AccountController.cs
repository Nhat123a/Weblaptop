using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopthoitrang.Models;
using Shopthoitrang.Models.ViewModels;

namespace Shopthoitrang.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUserModel> _account;
        private readonly SignInManager<AppUserModel> _signInManager;
        public AccountController(UserManager<AppUserModel> account,SignInManager<AppUserModel> signIn)
        {
            _account = account;
            _signInManager = signIn;
        }
        public IActionResult Login(string Url)
        {
            return View(new LoginviewModel { url=Url});
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginviewModel loginview)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(loginview.Username, loginview.Password,false,false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác");
            }
            return View(loginview);
        }
		public IActionResult Singup()
		{
			return View();
		}
		[HttpPost]
        public async Task<IActionResult> Singup(UserModel user)  
        {
            if(ModelState.IsValid)
            {
                AppUserModel newuser = new AppUserModel { UserName=user.Username,Email=user.Email};
                IdentityResult result = await _account.CreateAsync(newuser,user.Password);
                if(result.Succeeded)
                {
                    RedirectToAction("Login");
                }
            }
            else
            {
                ModelState.AddModelError("", "Tạo không thành công kiểm tra lại ");
            }
            return View(user);
        }
        public async Task<IActionResult> logout(string Url)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

	}
}
