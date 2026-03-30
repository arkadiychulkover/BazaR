using BazaR.Interfaces;
using BazaR.Models;
using BazaR.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BazaR.Filters
{
    public class BlockResourseFilter : Attribute, IAsyncResourceFilter
    {
        private readonly UserManager<User> _userManager;
        public BlockResourseFilter(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            User us = await _userManager.GetUserAsync(context.HttpContext.User);
            if (us != null && us.LockoutEnabled && us.LockoutEnd.HasValue && us.LockoutEnd.Value > DateTimeOffset.UtcNow)
            {
                Console.WriteLine($"============================================{us.Id}, {us.LockoutEnabled}, {us.Name}");
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
                return;
            }
            await next();
        }
    }
}
