using DoAn1_DDG_Pro.Identity;
using DoAn1_DDG_Pro.Models;
using DoAn1_DDG_Pro.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

internal class Program
{
    private static async Task Main(string[] args)
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

        // Tạo một phạm vi dịch vụ
        using (var scope = app.Services.CreateScope())
        {
            // Gọi phương thức khởi tạo để tạo các role
            await InitializeRoles(scope.ServiceProvider);
        }

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

   

    private static async Task InitializeRoles(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<AppUserModel>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Admin", "Guest" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                // Tạo các vai trò và lưu chúng vào cơ sở dữ liệu
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        
        // Tạo một người dùng Admin
        var adminUser = new AppUserModel { UserName = "ad123", Email = "ad@gamil.com" };
        string adminPassword = "ad123";

        var _user = await userManager.FindByNameAsync(adminUser.UserName);

        if (_user == null)
        {
            var createAdminUser = await userManager.CreateAsync(adminUser, adminPassword);
            if (createAdminUser.Succeeded)
            {
                // Gán vai trò Admin cho người dùng
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

    }
}
