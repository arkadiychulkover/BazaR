// OnlineResourceFilter.cs
using BazaR.Interfaces;
using BazaR.Models;
using BazaR.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BazaR.Filters
{
    public class OnlineResourceFilter : Attribute, IAsyncResourceFilter
    {
        private readonly ActiveUsersService _activeUsers;
        private readonly IUserStatistick _statsRepo;
        private readonly UserManager<User> _userManager;

        public OnlineResourceFilter(ActiveUsersService activeUsers, UserManager<User> userManager, IUserStatistick st)
        {
            _activeUsers = activeUsers;
            _userManager = userManager;
            _statsRepo = st;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var user = await _userManager.GetUserAsync(context.HttpContext.User);

            if (user != null)
            {
                await _statsRepo.AddUser(user);
                _activeUsers.PingUser(user.Id);
            }

            await next();
        }
    }
}