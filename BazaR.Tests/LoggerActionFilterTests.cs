using BazaR.Filters;
using BazaR.Interfaces;
using BazaR.Models;
using BazaR.Models.BazaR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using System.Security.Claims;
using Xunit;

namespace BazaR.Tests
{
    public class LoggerActionFilterTests
    {
        private Mock<UserManager<User>> GetUserManagerMock()
        {
            var store = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        }

        private ActionExecutingContext GetContext(
            string controller = "Home",
            string action = "Index",
            Dictionary<string, object>? actionArgs = null,
            QueryString? query = null)
        {
            var httpContext = new DefaultHttpContext();

            if (query.HasValue)
                httpContext.Request.QueryString = query.Value;

            httpContext.User = new ClaimsPrincipal(
                new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "test") }, "mock"));

            var routeData = new RouteData();
            routeData.Values["controller"] = controller;
            routeData.Values["action"] = action;

            var actionContext = new Microsoft.AspNetCore.Mvc.ActionContext(
                httpContext,
                routeData,
                new ActionDescriptor());

            return new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                actionArgs ?? new Dictionary<string, object>(),
                controller: null
            );
        }

        private ActionExecutionDelegate GetNextDelegate(ActionExecutingContext executingContext)
        {
            return () =>
            {
                var executedContext = new ActionExecutedContext(
                    executingContext,
                    new List<IFilterMetadata>(),
                    executingContext.Controller);

                return Task.FromResult(executedContext);
            };
        }

        [Fact]
        public async Task Should_Log_VisitingPage_By_Default()
        {
            var loggerMock = new Mock<ILogDb>();
            var userManagerMock = GetUserManagerMock();

            var user = new User { Id = 1 };
            userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);

            var filter = new LoggerActionFilter(loggerMock.Object, userManagerMock.Object);
            var context = GetContext(action: "Index");

            await filter.OnActionExecutionAsync(context, GetNextDelegate(context));

            loggerMock.Verify(x => x.LogPageVisitAsync(
                user.Id,
                UserAction.VisitingPage,
                "Home",
                "Index",
                null,
                null,
                null), Times.Once);
        }

        [Fact]
        public async Task Should_Log_Browse_With_Filters()
        {
            var loggerMock = new Mock<ILogDb>();
            var userManagerMock = GetUserManagerMock();

            var user = new User { Id = 1 };
            userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);

            var query = new QueryString("?query=test&page=2&sort=price");

            var filter = new LoggerActionFilter(loggerMock.Object, userManagerMock.Object);
            var context = GetContext(action: "Browse", query: query);

            await filter.OnActionExecutionAsync(context, GetNextDelegate(context));

            loggerMock.Verify(x => x.LogPageVisitAsync(
                user.Id,
                UserAction.Browse,
                "Home",
                "Browse",
                null,
                null,
                It.Is<SearchFilters>(f =>
                    f.Query == "test" &&
                    f.Page == 2 &&
                    f.Sort == "price"
                )), Times.Once);
        }

        [Fact]
        public async Task Should_Log_AddToCart_With_ItemId_From_ActionArgs()
        {
            var loggerMock = new Mock<ILogDb>();
            var userManagerMock = GetUserManagerMock();

            var user = new User { Id = 1 };
            userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);

            var args = new Dictionary<string, object>
            {
                { "itemId", 10 }
            };

            var filter = new LoggerActionFilter(loggerMock.Object, userManagerMock.Object);
            var context = GetContext(action: "AddToCart", actionArgs: args);

            await filter.OnActionExecutionAsync(context, GetNextDelegate(context));

            loggerMock.Verify(x => x.LogPageVisitAsync(
                user.Id,
                UserAction.AddToCart,
                "Home",
                "AddToCart",
                10,
                null,
                null), Times.Once);
        }

        [Fact]
        public async Task Should_Log_AddToWishlist_With_Id()
        {
            var loggerMock = new Mock<ILogDb>();
            var userManagerMock = GetUserManagerMock();

            var user = new User { Id = 1 };
            userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);

            var args = new Dictionary<string, object>
            {
                { "id", 5 }
            };

            var filter = new LoggerActionFilter(loggerMock.Object, userManagerMock.Object);
            var context = GetContext(action: "AddToWishlist", actionArgs: args);

            await filter.OnActionExecutionAsync(context, GetNextDelegate(context));

            loggerMock.Verify(x => x.LogPageVisitAsync(
                user.Id,
                UserAction.AddToWishList,
                "Home",
                "AddToWishlist",
                5,
                null,
                null), Times.Once);
        }

        [Fact]
        public async Task Should_Log_CreateOrder()
        {
            var loggerMock = new Mock<ILogDb>();
            var userManagerMock = GetUserManagerMock();

            var user = new User { Id = 1 };
            userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);

            var filter = new LoggerActionFilter(loggerMock.Object, userManagerMock.Object);
            var context = GetContext(action: "CreateOrder");

            await filter.OnActionExecutionAsync(context, GetNextDelegate(context));

            loggerMock.Verify(x => x.LogPageVisitAsync(
                user.Id,
                UserAction.MakeOrder,
                "Home",
                "CreateOrder",
                null,
                null,
                null), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Log_When_User_Is_Null()
        {
            var loggerMock = new Mock<ILogDb>();
            var userManagerMock = GetUserManagerMock();

            userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync((User)null);

            var filter = new LoggerActionFilter(loggerMock.Object, userManagerMock.Object);
            var context = GetContext();

            await filter.OnActionExecutionAsync(context, GetNextDelegate(context));

            loggerMock.Verify(x => x.LogPageVisitAsync(
                It.IsAny<int>(),
                It.IsAny<UserAction>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<int?>(),
                It.IsAny<SearchFilters>()), Times.Never);
        }
    }
}