using DoAn1_DDG_Pro.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DoAn1_DDG_Pro.Identity
{
	public class AppDbContext : IdentityDbContext<AppUserModel>
	{
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Product> Product { get; set; }

		public DbSet<ProductType> ProductType { get; set; }

        public DbSet<OrderModel> OrderModel { get; set; }

        public DbSet<OrderDetails> OrderDetail { get; set; }

		

		public DbSet<Product> products { get; set; }



	}
}
