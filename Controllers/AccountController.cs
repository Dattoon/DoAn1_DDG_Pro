using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DoAn1_DDG_Pro.Identity;
using DoAn1_DDG_Pro.Models;
using DoAn1_DDG_Pro.Models.ViewModels;
using Octokit;


namespace DoAn1_DDG_Pro.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUserModel> _userManager;
        private SignInManager<AppUserModel> _signInManager;



        public AccountController(SignInManager<AppUserModel> signInManager ,UserManager<AppUserModel> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel {returnUrl=returnUrl});
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(loginVM.UserName);
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                    }
                    else
                    {
                        return Redirect(loginVM.returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError("", "IsValid UserName and Password");
            }
            return View(loginVM);
        }





        public async Task<IActionResult> Register(UserModel user)
        {
            if (ModelState.IsValid)
            {
                AppUserModel model = new AppUserModel { UserName = user.UserName, Email = user.Email };
                IdentityResult result = await _userManager.CreateAsync(model, user.Password);
                if (result.Succeeded)
                {
                    // Gán role "Guest" cho người dùng mới
                    await _userManager.AddToRoleAsync(model, "Guest");

                    TempData["success"] = "Tạo tài khoản thành công ";
                    return Redirect("/account/login");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(user);
        }

        public async Task<IActionResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

    }
}
