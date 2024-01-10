using DoAn1_DDG_Pro.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DoAn1_DDG_Pro.Areas.Admin.Controllers
{
    [Authentication]
    [Area("admin")]
    [Route("admin")]
    [Route("admin/manager")]
    public class ManagerController : Controller
    {
        [Route("DoanhThu")]
        public IActionResult DoanhThu()
        {
            return View();
        }
        [Route("NhanVien")]
        public IActionResult NhanVien()
        {
            return View();
        }
        [Route("KhachHang")]
        public IActionResult KhachHang()
        {
            return View();
        }
    }
}
