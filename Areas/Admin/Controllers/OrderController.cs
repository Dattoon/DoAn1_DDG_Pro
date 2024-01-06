using DoAn1_DDG_Pro.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoAn1_DDG_Pro.Models;
using DoAn1_DDG_Pro.Areas.Admin.Controllers;
using DoAn1_DDG_Pro.Identity;

namespace DoAn1_DDG_Pro.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    
    public class OrderController : Controller
	{
		private readonly AppDbContext _dbContext;
		public OrderController(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}
        [Route("admin/Order")]
        public async Task< IActionResult> Order()
		{
			return View(await _dbContext.OrderModel.OrderByDescending(p=>p.Id).ToListAsync());
		}
        [Route("admin/OrderView/{orderCode}")]
        public async Task<IActionResult> OrderView(string orderCode)
        {
            var order = await _dbContext.OrderModel.FirstOrDefaultAsync(o => o.OrderCode == orderCode);
            if (order == null)
            {
                return NotFound();
            }

            var orderDetails = await _dbContext.OrderDetail
                .Include(o => o.Product)
                .Where(od => od.OrderCode == orderCode)
                .ToListAsync();

            if (!orderDetails.Any())
            {
                return NotFound();
            }

            return View(orderDetails); // Trả về một View với mô hình là orderDetails
        }






        [Route("admin/Access/{id}")]
        public async Task<IActionResult> Access(int id)
        {
            var order = await _dbContext.OrderModel.FirstOrDefaultAsync(o => o.Id == id);
            if (order != null)
            {
                order.Status = 2; // Change the status
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Order"); // This will refresh the page
        }





    }
}
