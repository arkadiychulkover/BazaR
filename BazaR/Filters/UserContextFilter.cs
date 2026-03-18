using BazaR.Interfaces;
using BazaR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BazaR.Filters
{
    public class UserContextFilter : IActionFilter
    {
        private readonly IUserDb _usMan;
        private readonly UserManager<User> _userManager;

        public UserContextFilter(IUserDb usMan, UserManager<User> userManager)
        {
            _usMan = usMan;
            _userManager = userManager;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is not Controller controller)
                return;

            var principal = context.HttpContext.User;
            if (principal.Identity?.IsAuthenticated == true)
            {
                var user = _userManager.GetUserAsync(principal).Result;
                controller.ViewBag.User = user;
                if (user != null)
                {
                    controller.ViewBag.CartCount = _usMan.GetCartItems(user.Id)?.Count() ?? 0;
                    controller.ViewBag.WishlistCount = _usMan.GetWishList(user.Id)?.Count() ?? 0;
                }
                else
                {
                    controller.ViewBag.CartCount = 0;
                    controller.ViewBag.WishlistCount = 0;
                }
            }
            else
            {
                controller.ViewBag.User = null;
                controller.ViewBag.CartCount = 0;
                controller.ViewBag.WishlistCount = 0;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
