using Core.Entities.Identity;
using Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seed
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            // 1️⃣ Seed Roles
            var roles = new[]
            {
                "Admin",
                "BuyerCompanyAdmin",
                "BuyerUser",
                "VendorCompanyAdmin",
                "VendorUser"
            };

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new Role
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper(), 
                    };
                    await roleManager.CreateAsync(role);
                }
            }

            // 2️⃣ Seed Admin User
            var adminEmail = "admin@sgp.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = adminEmail,
                    NormalizedUserName = adminEmail.ToUpper(),
                    Email = adminEmail,
                    NormalizedEmail = adminEmail.ToUpper(),
                    EmailConfirmed = true,
                    FirstName = "System",
                    LastName = "Admin",
                    DisplayName = "System Admin",
                    UserType = UserTypeEnum.Admin, 
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                // Set password
                await userManager.CreateAsync(adminUser, "Admin@123");

                // Assign Admin role
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
