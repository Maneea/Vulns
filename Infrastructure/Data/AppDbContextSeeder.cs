using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Vulns.Core;

namespace Vulns.Infrastructure;
public class AppDbContextInitializer
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        RoleManager<IdentityRole>? roleManager;
        UserManager<User>? userManager;
        using (var scope = serviceProvider.CreateScope())
        {
            roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            userManager = scope.ServiceProvider.GetService<UserManager<User>>();

            if (roleManager == null || userManager == null) return;

            // Seeding roles
            string[] roles = new string[] { Role.User.ToString(), Role.Admin.ToString() };
            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new(role));
            }

            // Seeding admin
            var user = new User
            {
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                SecurityStamp = Guid.NewGuid().ToString()
            };


            var userFromDb = await userManager.FindByNameAsync(user.UserName);
            if (userFromDb == null){
                await userManager.CreateAsync(user, "P@ssw0rd");
                userFromDb = user;
            }

            // assigning the admin user with the roles "user" and "admin" 
            foreach (var role in roles)
                if(!await userManager.IsInRoleAsync(userFromDb, role))
                    await userManager.AddToRoleAsync(userFromDb, role);
        }
    }
}