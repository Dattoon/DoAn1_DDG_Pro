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
			return View(await _dbContext.OrderDetails.OrderByDescending(p=>p.Id).ToListAsync());
		}
       
    }
}
