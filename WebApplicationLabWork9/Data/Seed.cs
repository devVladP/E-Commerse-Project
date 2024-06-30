using Microsoft.AspNetCore.Identity;
using System.Net;
using Microsoft.Data;
using WebApplicationLabWork9.Models;

namespace WebApplicationLabWork9.Data
{
	public class Seed
	{
		public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				//Roles
				var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
					await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
				if (!await roleManager.RoleExistsAsync(UserRoles.User))
					await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
				if (!await roleManager.RoleExistsAsync(UserRoles.Manager))
					await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));

				//Users
				var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
				string adminUserEmail = "vladislavlicey1@gmail.com";

				var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
				if (adminUser == null)
				{
					var newAdminUser = new AppUser()
					{
						UserName = "vladyslavdev",
						Email = adminUserEmail,
						EmailConfirmed = true,
					};
					await userManager.CreateAsync(newAdminUser, "Vlad@1234");
					await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
				}

				string appUserEmail = "randomuser@gmail.com";

				var appUser = await userManager.FindByEmailAsync(appUserEmail);
				if (appUser == null)
				{
					var newAppUser = new AppUser()
					{
						UserName = "random-app-user",
						Email = appUserEmail,
						EmailConfirmed = true,
					};
					await userManager.CreateAsync(newAppUser, "User@1234");
					await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
				}

				string managerUserEmail = "manager123@gmail.com";

				var managerUser = await userManager.FindByEmailAsync(managerUserEmail);
				if (managerUser == null)
				{
					var newManagerUser = new AppUser()
					{
						UserName = "managerUser",
						Email = managerUserEmail,
						EmailConfirmed = true,
					};
					await userManager.CreateAsync(newManagerUser, "Manager@1234");
					await userManager.AddToRoleAsync(newManagerUser, UserRoles.Manager);
				}
			}
		}
	}
}
