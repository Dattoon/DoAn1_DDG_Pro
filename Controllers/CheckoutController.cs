using DoAn1_DDG_Pro.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DoAn1_DDG_Pro.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly Datacontext _datacontext;

		public CheckoutController(Datacontext context)
		{
			_datacontext = context;
		}
		public async Task<IActionResult> Checkout()
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
				_datacontext.SaveChanges();


			}
			return View();
		}
	}
}
