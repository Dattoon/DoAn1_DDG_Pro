
using Azure;
using DoAn1_DDG_Pro.Models;
using DoAn1_DDG_Pro.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.InteropServices;
using X.PagedList;

namespace DDG_shop.Controllers
{
    public class HomeController : Controller
    {
        ShopDdgContext db = new ShopDdgContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = page==null || page<0 ? 1 : page.Value;
            var lstsanpham = db.Products.AsNoTracking().OrderBy(x=>x.ProductName);
            PagedList<Product> lst = new PagedList<Product>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }


        public IActionResult SanPhamTheoLoai(string TypeId, int? page)
        {
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.Products.AsNoTracking().Where(x=>x.TypeId==TypeId).OrderBy(x => x.ProductName);
            PagedList<Product> lst = new PagedList<Product>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }
        

        public IActionResult ChiTietSanPham (int ProductId ) 
        {
            var sanPham=db.Products.SingleOrDefault(x=>x.ProductId==ProductId);
            var anhSanPham=db.ProductImages.Where(x=>x.ProductId==ProductId).ToList();
            ViewBag.anhSanPham = anhSanPham;
            return View(sanPham);
        }
        public IActionResult ProductDetail (int ProductId)
        {
            var sanPham = db.Products.SingleOrDefault(x => x.ProductId == ProductId);
            var anhSanPham = db.ProductImages.Where(x => x.ProductId == ProductId).ToList();
            var homeProductDetailViewModel = new HomeProductDetailViewModel
            {
                SanPham = sanPham,
                anhSps = anhSanPham,
            };

            return View(homeProductDetailViewModel);
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
