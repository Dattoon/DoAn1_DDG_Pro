using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using DoAn1_DDG_Pro.Models;
using DoAn1_DDG_Pro.Repository;
using DoAn1_DDG_Pro.Models.ViewModels;
using DoAn1_DDG_Pro.Identity;

namespace DoAn1_DDG_Pro.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _AppDbContext;
        public CartController(AppDbContext AppDbContext)
        {
            _AppDbContext = AppDbContext;
        }
        
        public IActionResult OrderForm()
        {
          
            return View("OrderForm");
        }
        public IActionResult ViewOrder()
        {

            return View("ViewOrder");
        }


        public IActionResult Cart()
		{
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemViewModels cartVM = new()
			{
				CartItems = cartItems,
				Total = cartItems.Sum(x => x.Quantity * x.Price),
			};
			return View(cartVM);
		}
		public async Task<IActionResult> Add(int ProductId)
		{
			Product product = await _AppDbContext.products.FindAsync(ProductId);
			List<CartItemModel> carts = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemModel cartItem = carts.Where(x => x.ProductId == ProductId).FirstOrDefault();

			if (cartItem == null)
			{
				carts.Add(new CartItemModel(product));
			}
			else
			{
				cartItem.Quantity += 1;
			}
			HttpContext.Session.SetJson("Cart", carts);
			TempData["success"] = "Bạn Đã Thêm Sản Phẩm Vào Giỏ Hàng";
			return Redirect(Request.Headers["Referer"].ToString());
		}

		public async Task<IActionResult> Giam(int ProductId)
		{
			List<CartItemModel> carts = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

			CartItemModel cartItem = carts.Where(x => x.ProductId == ProductId).FirstOrDefault();
			if (cartItem.Quantity > 1)
			{
				--cartItem.Quantity;
			}
			else
			{
				carts.RemoveAll(x => x.ProductId == ProductId);
			}


			if (carts.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", carts);
			}

			return RedirectToAction("Cart");


		}
		public async Task<IActionResult> Tang(int ProductId)
		{
			List<CartItemModel> carts = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

			CartItemModel cartItem = carts.Where(x => x.ProductId == ProductId).FirstOrDefault();
			if (cartItem.Quantity >= 1)
			{
				++cartItem.Quantity;
			}
			else
			{
				carts.RemoveAll(x => x.ProductId == ProductId);
			}


			if (carts.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", carts);
			}
            
            return RedirectToAction("Cart");


		}

		public async Task<IActionResult> Remove(int ProductId)
		{
			List<CartItemModel> carts = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			carts.RemoveAll(x=>x.ProductId == ProductId);
			if (carts.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", carts);
			}
			TempData["success"] = "Bạn Đã Xóa Sản Phẩm Khỏi Giỏ Hàng";
            return RedirectToAction("Cart");
		}
		public async Task<IActionResult> Clear(int ProductId)
		{
			HttpContext.Session.Remove("Cart");
			return RedirectToAction("Cart");
		}
      




    }
}
