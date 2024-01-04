using DoAn1_DDG_Pro.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoAn1_DDG_Pro.Models;
using DoAn1_DDG_Pro.Areas.Admin.Controllers;

namespace DoAn1_DDG_Pro.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/Order")]
    public class OrderController : Controller
	{
		private readonly AppDbContext _dbContext;
		public OrderController(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		 
		public async Task< IActionResult> Order()
		{
			return View(await _dbContext.OrderModel.OrderByDescending(p=>p.Id).ToListAsync());
		}
        public async Task<IActionResult> OrderView(string OrderCode)
        {
			var DetailOrder = await _dbContext.OrderDetail.Include(o=>o.ProductId).Where(od=>od.OrderCode == OrderCode).ToArrayAsync();
            return View(DetailOrder);
        }

    }
}
