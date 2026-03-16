// OnlineResourceFilter.cs
using BazaR.Models;
using BazaR.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BazaR.Filters
{
    public class OnlineResourceFilter : Attribute, IAsyncResourceFilter
    {
        private readonly ActiveUsersService _activeUsers;
        private readonly UserManager<User> _userManager;

        public OnlineResourceFilter(ActiveUsersService activeUsers, UserManager<User> userManager)
        {
            _activeUsers = activeUsers;
            _userManager = userManager;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var user = await _userManager.GetUserAsync(context.HttpContext.User);
            if (user != null)
            {
                _activeUsers.PingUser(user.Id);
            }

            await next();
        }
    }
}