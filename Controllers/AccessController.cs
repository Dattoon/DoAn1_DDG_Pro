using Microsoft.AspNetCore.Mvc;
using DoAn1_DDG_Pro.Models;

namespace DoAn1_DDG_Pro.Controllers
{
    public class AccessController : Controller
    {
        ShopDdgContext db = new ShopDdgContext();



        [HttpGet]
        public ActionResult Login()
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
        public ActionResult Login(UserAccount user) 
        {
            if (HttpContext.Session.GetString("Username")== null)
            {
                var u = db.UserAccounts.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("Username", u.Username.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        
    }
}
