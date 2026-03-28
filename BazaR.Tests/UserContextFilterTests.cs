using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BazaR.Filters;
using BazaR.Models;
using BazaR.Interfaces;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace BazaR.Tests
{
    public class UserContextFilterTests
    {
        private Mock<UserManager<User>> GetUserManagerMock()
        {
            var store = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        }

        private ActionExecutingContext GetActionContext(ClaimsPrincipal user)
        {
            var httpContext = new DefaultHttpContext
            {
                User = user
            };

            var controller = new TestController();
            var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());

            return new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                controller
            );
        }

        [Fact]
        public void Should_Set_ViewBag_When_User_Authenticated_With_Data()
        {
            var userDbMock = new Mock<IUserDb>();
            var userManagerMock = GetUserManagerMock();

            var user = new User { Id = 1, Name = "TestUser" };

            userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            userDbMock.Setup(x => x.GetCartItems(user.Id)).Returns(new List<Item> { new Item(), new Item() }.AsQueryable());
            userDbMock.Setup(x => x.GetWishList(user.Id)).Returns(new List<Item> { new Item() }.AsQueryable());

            var filter = new UserContextFilter(userDbMock.Object, userManagerMock.Object);

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "test") }, "mock"));
            var context = GetActionContext(claimsPrincipal);

            var controller = context.Controller as Controller;

            filter.OnActionExecuting(context);

            Assert.Equal(user, controller.ViewBag.User);
            Assert.Equal(2, controller.ViewBag.CartCount);
            Assert.Equal(1, controller.ViewBag.WishlistCount);
        }

        [Fact]
        public void Should_Set_Zero_When_User_Authenticated_But_No_Data()
        {
            var userDbMock = new Mock<IUserDb>();
            var userManagerMock = GetUserManagerMock();

            var user = new User { Id = 1, Name = "TestUser" };

            userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            userDbMock.Setup(x => x.GetCartItems(user.Id)).Returns(Enumerable.Empty<Item>().AsQueryable());
            userDbMock.Setup(x => x.GetWishList(user.Id)).Returns(Enumerable.Empty<Item>().AsQueryable());

            var filter = new UserContextFilter(userDbMock.Object, userManagerMock.Object);

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "test") }, "mock"));
            var context = GetActionContext(claimsPrincipal);

            var controller = context.Controller as Controller;

            filter.OnActionExecuting(context);
            
            Assert.Equal(user, controller.ViewBag.User);
            Assert.Equal(0, controller.ViewBag.CartCount);
            Assert.Equal(0, controller.ViewBag.WishlistCount);
        }

        [Fact]
        public void Should_Set_Zero_When_User_Not_Authenticated()
        {
            var userDbMock = new Mock<IUserDb>();
            var userManagerMock = GetUserManagerMock();

            var filter = new UserContextFilter(userDbMock.Object, userManagerMock.Object);

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            var context = GetActionContext(claimsPrincipal);

            var controller = context.Controller as Controller;

            filter.OnActionExecuting(context);
            
            Assert.Null(controller.ViewBag.User);
            Assert.Equal(0, controller.ViewBag.CartCount);
            Assert.Equal(0, controller.ViewBag.WishlistCount);
        }
    }
    public class TestController : Controller 
    {
        
    }
}