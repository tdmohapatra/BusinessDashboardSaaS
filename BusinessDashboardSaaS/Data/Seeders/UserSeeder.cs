using Microsoft.AspNetCore.Identity;

namespace BusinessDashboardSaaS.Data.Seeders
{
    public static class UserSeeder
    {
        public static async Task SeedUsersAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Admin", "Manager", "User" };

            // ✅ Create roles
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // 🔐 Admin
            await CreateUserIfNotExists(userManager, "admin@demo.com", "Admin@123", "Admin");

            // 👔 Manager
            await CreateUserIfNotExists(userManager, "manager@demo.com", "Manager@123", "Manager");

            // 👤 Basic User
            await CreateUserIfNotExists(userManager, "user@demo.com", "User@123", "User");
        }

        private static async Task CreateUserIfNotExists(UserManager<IdentityUser> userManager, string email, string password, string role)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
            else
            {
                if (!await userManager.IsInRoleAsync(user, role))
                    await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
