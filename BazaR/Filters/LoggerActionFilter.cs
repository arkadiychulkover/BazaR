using BazaR.Interfaces;
using BazaR.Models;
using BazaR.Models.BazaR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BazaR.Filters
{
    public class LoggerActionFilter : IAsyncActionFilter
    {
        private readonly ILogDb _logger;
        private readonly UserManager<User> _userManager;

        public LoggerActionFilter(ILogDb logger, UserManager<User> user)
        {
            _logger = logger;
            _userManager = user;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            string controllerName = resultContext.RouteData.Values["controller"].ToString();
            string actionName = resultContext.RouteData.Values["action"].ToString();
            var user = await _userManager.GetUserAsync(resultContext.HttpContext.User);
            var request = resultContext.HttpContext.Request;

            if (user == null)
                return;

            int userId = user.Id;
            int? itemId = null;
            int? orderId = null;
            SearchFilters? filters = null;
            UserAction userAction = UserAction.VisitingPage;

            if (actionName == "Browse")
            {
                userAction = UserAction.Browse;
                filters = new SearchFilters()
                {
                    Query = request.Query["query"],
                    CategoryIds = request.Query["categoryIds"]
                        .Select(x => int.TryParse(x, out var val) ? val : (int?)null)
                        .Where(x => x.HasValue)
                        .Select(x => x!.Value)
                        .ToList(),
                    Page = int.TryParse(request.Query["page"], out var page) ? page : 1,
                    Sort = string.IsNullOrEmpty(request.Query["sort"]) ? "default" : request.Query["sort"].ToString(),
                    MinPrice = decimal.TryParse(request.Query["minPrice"], out var min) ? min : null,
                    MaxPrice = decimal.TryParse(request.Query["maxPrice"], out var max) ? max : null,
                    BrandIds = request.Query["brandIds"]
                        .Select(x => int.TryParse(x, out var val) ? val : (int?)null)
                        .Where(x => x.HasValue)
                        .Select(x => x!.Value)
                        .ToList()
                };
            }
            else if (actionName == "AddToCart")
            {
                userAction = UserAction.AddToCart;

                if (context.ActionArguments.ContainsKey("itemId"))
                {
                    itemId = Convert.ToInt32(context.ActionArguments["itemId"]);
                }
                else if (request.Query.ContainsKey("itemId"))
                {
                    itemId = Convert.ToInt32(request.Query["itemId"]);
                }
                else if (request.HasFormContentType && request.Form.ContainsKey("itemId"))
                {
                    itemId = Convert.ToInt32(request.Form["itemId"]);
                }
            }
            else if (actionName == "AddToWishList" || actionName == "AddToWishlist")
            {
                userAction = UserAction.AddToWishList;

                if (context.ActionArguments.ContainsKey("id"))
                {
                    itemId = Convert.ToInt32(context.ActionArguments["id"]);
                }
                else if (request.Query.ContainsKey("id"))
                {
                    itemId = Convert.ToInt32(request.Query["id"]);
                }
                else if (request.HasFormContentType && request.Form.ContainsKey("id"))
                {
                    itemId = Convert.ToInt32(request.Form["id"]);
                }
            }
            else if (actionName == "CreateOrder")
            {
                userAction = UserAction.MakeOrder;
            }
            else
            {
                userAction = UserAction.VisitingPage;
            }

            await _logger.LogPageVisitAsync(userId, userAction, controllerName, actionName, itemId, orderId, filters);
        }
    }
}