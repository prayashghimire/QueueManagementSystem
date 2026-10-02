
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using QueueMS.Domain.Models.UserModels;
using QueueMS.Domain.Role;
using System.ComponentModel.DataAnnotations;

namespace QueueMS.Infrastructure.IdentitySeeder;

public static class IdentitySeeder
{
    public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        string[] roles =
        {
            Roles.Admin,
            Roles.Staff,
            Roles.User
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<int>(role));
            }
        }

        var adminEmail = "admin@system.com";
        var adminUsername = "admin";

        var admin = await userManager.FindByEmailAsync(adminEmail);

        if(admin == null)
        {
            admin = new User
            {
                UserName = adminUsername,
                Email = adminEmail,
                
            };

            var result = await userManager.CreateAsync(admin, "Admin@123");

            if (!result.Succeeded)
                throw new Exception("Failed To Create Admin");


        }

        if(!await userManager.IsInRoleAsync(admin, Roles.Admin))
        {
            await userManager.AddToRoleAsync(admin, Roles.Admin);
        }
    }
}
