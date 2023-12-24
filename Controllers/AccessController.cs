using Microsoft.AspNetCore.Mvc;
using DoAn1_DDG_Pro.Models;
using Microsoft.AspNetCore.Identity;

namespace DoAn1_DDG_Pro.Controllers
{
    public class AccessController : Controller
    {
        ShopDdgContext db = new ShopDdgContext();



        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
            

        }
        
        [HttpPost]
        public IActionResult Login(UserAccount user)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                var u = db.UserAccounts.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("Username", u.Username.ToString());
                    // Kiểm tra trường Role
                    if (u.Role.Equals("1"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (u.Role.Equals("0"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
            }
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login", "Access");
            
        }
    }
}
