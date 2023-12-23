using DoAn1_DDG_Pro.Models;
using DoAn1_DDG_Pro.Repository;
using Microsoft.AspNetCore.Mvc;
namespace DoAn1_DDG_Pro.ViewComponents
{
    public class LoaiSpMenuViewComponent : ViewComponent
    {
        private readonly ILoaiSpRepository _loaiSp;

        public LoaiSpMenuViewComponent(ILoaiSpRepository loaiSpRepository)
        {
            _loaiSp = loaiSpRepository;
        }
        public IViewComponentResult Invoke()
        {
            var loaisp = _loaiSp.GetAll().OrderBy(X => X.TypeName);
            return View(loaisp);
        }
    }
}
