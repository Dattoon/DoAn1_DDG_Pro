using DoAn1_DDG_Pro.Identity;
using DoAn1_DDG_Pro.Models;
using DoAn1_DDG_Pro.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var connectionString2 = builder.Configuration.GetConnectionString("AppDbContext");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString2));





        builder.Services.AddScoped<ILoaiSpRepository, LoaiSpRepository>();
        builder.Services.AddSession();
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(Option =>
        {
            Option.IdleTimeout = TimeSpan.FromMinutes(30);
            Option.Cookie.IsEssential = true;
        });


        builder.Services.AddIdentity<AppUserModel, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

        builder.Services.AddRazorPages();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 3;
          
           
            options.User.RequireUniqueEmail = false;
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.UseSession();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
        name: "Admin",
        pattern: "{area:exists}/{controller=HomeAdmin}/{action=Index}/{id?}");

        app.Run();
    }
}