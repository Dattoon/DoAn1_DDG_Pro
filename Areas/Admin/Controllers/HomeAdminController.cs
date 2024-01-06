using DoAn1_DDG_Pro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using DoAn1_DDG_Pro.Identity;
using DoAn1_DDG_Pro.Repository;


namespace DoAn1_DDG_Pro.Areas.Admin.Controllers
{
    
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]

    
    public class HomeAdminController : Controller
    {
        
        

        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeAdminController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham(string q, int? page)
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
        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
           
        {
            ViewBag.TypeId=new SelectList(_db.ProductType.ToList(),"TypeId","TypeName");
            return View();

        }
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemSanPhamMoi(Product sanPham)
        {
            if (ModelState.IsValid)
            {
                if (sanPham.ImgUpLoad != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "Media/main");
                    string imageName = Guid.NewGuid().ToString() + "_" +sanPham.ImgUpLoad.FileName ;
                    string filePath =Path.Combine(uploadsDir, imageName);

                    FileStream fs =new FileStream(filePath, FileMode.Create);
                    await sanPham.ImgUpLoad.CopyToAsync(fs);
                    fs.Close();
                    sanPham.Imgtop = imageName;
                }
                if (sanPham.ImgUpLoad2 != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "Media/bonus");
                    string imageName = Guid.NewGuid().ToString() + "_" + sanPham.ImgUpLoad2.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await sanPham.ImgUpLoad2.CopyToAsync(fs);
                    fs.Close();
                    sanPham.Imgbot = imageName;
                }
                _db.products.Add(sanPham);
                _db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sanPham);
        }

        

        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(int ProductID)

        {
            ViewBag.TypeId = new SelectList(_db.ProductType.ToList(), "TypeId", "TypeName");
            var sanPham = _db.products.Find(ProductID);

            return View(sanPham);

            

        }
        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaSanPham(int ProductID, Product sanPham)
        {
            if (ModelState.IsValid)
            {

                if (sanPham.ImgUpLoad != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "Media/main");
                    string imageName = Guid.NewGuid().ToString() + "_" + sanPham.ImgUpLoad.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await sanPham.ImgUpLoad.CopyToAsync(fs);
                    fs.Close();
                    sanPham.Imgtop = imageName;
                }
                if (sanPham.ImgUpLoad2 != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "Media/bonus");
                    string imageName = Guid.NewGuid().ToString() + "_" + sanPham.ImgUpLoad2.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await sanPham.ImgUpLoad2.CopyToAsync(fs);
                    fs.Close();
                    sanPham.Imgbot = imageName;
                }
                _db.Entry(sanPham).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            return View(sanPham);
        }



        [Route("XoaSanPham")]
        [HttpGet]
        public async Task< IActionResult> XoaSanPham(int ProductID)
        {
            TempData["Message"] = "";
            var anhSanPhams = _db.products.Where(x => x.ProductId==ProductID);
            if (anhSanPhams.Any()) _db.RemoveRange(anhSanPhams);
            _db.Remove(_db.products.Find(ProductID));
            _db.SaveChanges();
            TempData["Message"] = "Sản Phẩm Đã Được Xóa";
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
        }



        [Route("XoaLoai")]
        [HttpGet]
        public async Task<IActionResult> XoaLoai(string TypeID)
        {
            var loaiSanPham = await _db.ProductType.Include(p => p.Products).FirstOrDefaultAsync(p => p.TypeId == TypeID);
            if (loaiSanPham == null)
            {
                return NotFound();
            }

            if (loaiSanPham.Products.Any())
            {
                // Xử lý trường hợp có sản phẩm liên quan đến loại sản phẩm này.
                // Bạn có thể chọn xóa các sản phẩm liên quan hoặc cập nhật chúng để loại bỏ sự liên kết.
                foreach (var product in loaiSanPham.Products)
                {
                    _db.products.Remove(product);
                }
            }

            _db.ProductType.Remove(loaiSanPham);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(DanhMucLoai));
        }















        [Route("danhmucloai")]
        public  IActionResult DanhMucLoai()
        {
            var lstLoai = _db.ProductType.ToList();
            return View(lstLoai);
        }


        [Route("ThemLoai")]
        [HttpGet]

        public IActionResult ThemLoai()
        {
            return View();
        }
        [Route("ThemLoai")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemLoai(ProductType Loai)
        {
            if (ModelState.IsValid)
            {
                _db.ProductType.Add(Loai);
                _db.SaveChanges();
                return RedirectToAction("DanhMucLoai");
            }
            return View(Loai);
        }



        [Route("SuaLoai")]
        [HttpGet]
        public IActionResult SuaLoai(string TypeID)
        {
            var loaiSanPham = _db.ProductType.Find(TypeID);
            if (loaiSanPham == null)
            {
                return NotFound("Không tìm thấy loại sản phẩm với ID: " + TypeID);
            }
            return View(loaiSanPham);
        }


        [Route("SuaLoai")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaLoai(string TypeID, ProductType Loai)
        {
            if (TypeID != Loai.TypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(Loai);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTypeExists(Loai.TypeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("DanhMucLoai");
            }
            return View(Loai);
        }

        private bool ProductTypeExists(string id)
        {
            return _db.ProductType.Any(e => e.TypeId == id);
        }






    }
}

