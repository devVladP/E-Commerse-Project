using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplicationLabWork9.Data;
using WebApplicationLabWork9.Interfaces;
using WebApplicationLabWork9.Models;
using WebApplicationLabWork9.Repository;

namespace WebApplicationLabWork9
{
    public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
			builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
			builder.Services.AddControllersWithViews();
			builder.Services.AddScoped<IProductRepository, ProductRepository>();
			builder.Services.AddScoped<IOrderRepository, OrderRepository>();
			builder.Services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>();
			builder.Services.AddMemoryCache();
			builder.Services.AddSession();
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

			var app = builder.Build();

			if (args.Length == 1 && args[0].ToLower() == "seeddata")
			{
				await Seed.SeedUsersAndRolesAsync(app);
			}

			app.UseAuthentication();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller}/{action}/{id?}",
				defaults: new {controller="Home", action="Index"});

			app.UseStaticFiles();
			app.Run();
		}
	}
}