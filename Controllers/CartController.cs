using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using DoAn1_DDG_Pro.Models;
using DoAn1_DDG_Pro.Repository;
using DoAn1_DDG_Pro.Models.ViewModels;

namespace DoAn1_DDG_Pro.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopDdgContext _shopDdgContext;
        public CartController(ShopDdgContext shopDdgContext)
        {
            _shopDdgContext = shopDdgContext;
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
			Product product = await _shopDdgContext.Products.FindAsync(ProductId);
			List<CartItemModel> carts = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemModel cartSP = carts.Where(x => x.ProductId == ProductId).FirstOrDefault();


			if (cartSP == null)
			{
				carts.Add(new CartItemModel(product));
			}
			else
			{
				cartSP.Quantity += 1;
			}
			HttpContext.Session.SetJson("Cart", carts);

			return Redirect(Request.Headers["Referer"].ToString());
		}

	}
}
