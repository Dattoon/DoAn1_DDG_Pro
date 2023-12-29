using DoAn1_DDG_Pro.Identity;
using DoAn1_DDG_Pro.Models;
using DoAn1_DDG_Pro.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DoAn1_DDG_Pro.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly AppDbContext _datacontext;

		public CheckoutController(AppDbContext context)
		{
			_datacontext = context;
		}
		public async Task<IActionResult> Checkout(OrderDetails orderDetails)
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);
			if (userEmail == null)
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{
				var OrderCode = Guid.NewGuid().ToString();
				var OrderItem = new OrderModel();
				OrderItem.OrderCode = OrderCode;
				OrderItem.UserName = userEmail;
				OrderItem.Status = 1;
				OrderItem.CreatedDate = DateTime.Now;

                _datacontext.Add(OrderItem);
                _datacontext.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT OrderModel ON;");
                _datacontext.SaveChanges();
                _datacontext.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT OrderModel OFF");

               

				List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
				foreach (var cartItem in cartItems)
				{
					var Orders = new OrderDetails();
					orderDetails.UserName = userEmail;
					orderDetails.OrderCode = OrderCode;
					orderDetails.ProductId = cartItem.ProductId;
					orderDetails.Price = cartItem.Price;
					orderDetails.Quantity = cartItem.Quantity;
					_datacontext.Add(orderDetails);

                    _datacontext.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT OrderDetails ON;");
                    _datacontext.SaveChanges();
                    _datacontext.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT OrderDetails OFF");
                    
				}
				HttpContext.Session.Remove("Cart");
				TempData["success"] = "Đặc hàng thành công, Vui lòng chờ duyệt";
				return RedirectToAction("Cart","Cart");
			}
			return View();
		}
	}
}
