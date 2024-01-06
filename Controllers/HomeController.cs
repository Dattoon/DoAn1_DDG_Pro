
using Azure;
using DoAn1_DDG_Pro.Models;
using DoAn1_DDG_Pro.Models.Authentication;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.InteropServices;
using X.PagedList;
using DoAn1_DDG_Pro.Identity;

namespace DDG_shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AppDbContext db, ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }


        public IActionResult Index(string q, int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = _db.products.AsNoTracking();

            if (!String.IsNullOrEmpty(q))
            {
                lstsanpham = lstsanpham.Where(s => s.ProductName.ToUpper().Contains(q.ToUpper()));
            }

            lstsanpham = lstsanpham.OrderBy(x => x.ProductName);
            PagedList<Product> lst = new PagedList<Product>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }




        public IActionResult SanPhamTheoLoai(string TypeId) 
        {
            var lstsanpham = _db.products.AsNoTracking().Where(x => x.TypeId == TypeId).OrderBy(x => x.ProductName);
            return View(lstsanpham);
        } 
        
        

        public IActionResult ChiTietSanPham (int ProductId ) 
        {
            var sanPham=_db.products.SingleOrDefault(x=>x.ProductId==ProductId);
            var anhSanPham=_db.products.Where(x=>x.ProductId==ProductId).ToList();
            ViewBag.anhSanPham = anhSanPham;
            return View(sanPham);
        }




        public IActionResult TimSanPham(int? page, string name)
        {
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            if (!string.IsNullOrEmpty(name))
            {
                var lstsanpham = _db.products.AsNoTracking().Where(x => x.ProductName.ToUpper().Contains(name.ToUpper()));
                PagedList<Product> lst = new PagedList<Product>(lstsanpham, pageNumber, pageSize);
                return View(lst);
            }
            else
            {
                var lstsanpham = _db.products.AsNoTracking().OrderBy(x => x.ProductName);
                PagedList<Product> lst = new PagedList<Product>(lstsanpham, pageNumber, pageSize);
                return View(lst);
            }
        }

       


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
