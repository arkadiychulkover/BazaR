using BazaR.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BazaR.Data
{
    public class DbMaker
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<User> _userManager;

        public DbMaker(AppDbContext context, RoleManager<IdentityRole<int>> roleManager, UserManager<User> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task MakeAsync()
        {
            await EnsureRoleAsync("Admin");
            await EnsureRoleAsync("User");

            var adminEmail = "admin@bazar.ua";
            var admin = await _userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    Name = "Admin User",
                    FirstName = "Admin",
                    LastName = "BazaR",
                    IsAdmin = true
                };

                var result = await _userManager.CreateAsync(admin, "admin123");
                if (!result.Succeeded)
                {
                    throw new Exception($"Не удалось создать администратора: {string.Join(", ", result.Errors)}");
                }
            }

            if (!await _userManager.IsInRoleAsync(admin, "Admin"))
            {
                await _userManager.AddToRoleAsync(admin, "Admin");
            }

            if (!await _userManager.IsInRoleAsync(admin, "User"))
            {
                await _userManager.AddToRoleAsync(admin, "User");
            }

            var isInAdminRole = await _userManager.IsInRoleAsync(admin, "Admin");
            bool needUpdate = false;
            if (admin.IsAdmin != isInAdminRole) { admin.IsAdmin = isInAdminRole; needUpdate = true; }
            if (string.IsNullOrWhiteSpace(admin.FirstName)) { admin.FirstName = "Admin"; needUpdate = true; }
            if (string.IsNullOrWhiteSpace(admin.LastName)) { admin.LastName = "BazaR"; needUpdate = true; }
            if (needUpdate) await _userManager.UpdateAsync(admin);
        }

        private async Task EnsureRoleAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole<int>(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception($"Не удалось создать роль {roleName}: {string.Join(", ", result.Errors)}");
                }
            }
        }
    }
}
