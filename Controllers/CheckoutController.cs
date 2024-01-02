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
                await _datacontext.SaveChangesAsync();

                List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
                foreach (var cartItem in cartItems)
                {
                    var Orders = new OrderDetails();
                    Orders.UserName = userEmail;
                    Orders.OrderCode = OrderCode;
                    Orders.ProductId = cartItem.ProductId;
                    Orders.Price = cartItem.Price;
                    Orders.Quantity = cartItem.Quantity;

                    // Lưu các trường mới
                    Orders.CustomerName = orderDetails.CustomerName;
                    Orders.PhoneNumber = orderDetails.PhoneNumber;
                    Orders.Address = orderDetails.Address;
                    Orders.PaymentMethod = orderDetails.PaymentMethod;
                    Orders.Description = orderDetails.Description; 

                    _datacontext.Add(Orders);
                    await _datacontext.SaveChangesAsync();
                }

                HttpContext.Session.Remove("Cart");
                TempData["success"] = "Đặc hàng thành công, Vui lòng chờ duyệt";
                return RedirectToAction("Cart", "Cart");
            }
            return View();
        }


       





    }
}
