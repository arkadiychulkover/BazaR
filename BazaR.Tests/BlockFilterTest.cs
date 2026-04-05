using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System.Security.Claims;
using static System.Net.WebRequestMethods;
using Moq;
using Microsoft.AspNetCore.Identity;
using BazaR.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using BazaR.Filters;

namespace BazaR.Tests
{
    public class BlockFilterTest
    {
        private Mock<UserManager<User>> GetManagerMock() 
        {
            var strore = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(strore.Object, null, null, null, null, null, null, null, null);
        }
        private ResourceExecutingContext GetContext()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }));

            var routeData = new RouteData();
            routeData.Values["controller"] = "Account";
            routeData.Values["action"] = "Test";

            var actionContext = new ActionContext(
                httpContext,
                routeData,
                new ActionDescriptor()
            );

            return new ResourceExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new List<IValueProviderFactory>()
            );
        }

        [Fact]
        public async Task Should_Redirect_When_User_Is_Blocked() 
        {
            var UserManagerMock = GetManagerMock();
            var context = GetContext();
            User us = new User() 
            {
                Id = 1,
                LockoutEnabled = true,
                LockoutEnd = DateTimeOffset.UtcNow.AddHours(1),
            };

            UserManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(us);
            var filter = new BlockResourseFilter(UserManagerMock.Object);

            bool nextCalled = false;
            ResourceExecutionDelegate next = () => { 
                nextCalled = true;
                return Task.FromResult<ResourceExecutedContext>(null);
            };

            await filter.OnResourceExecutionAsync(context, next);

            Assert.False(nextCalled);
            Assert.IsType<RedirectToActionResult>(context.Result);

            var result = context.Result as RedirectToActionResult;
            Assert.Equal("AccessDenied", result.ActionName);
            Assert.Equal("Account", result.ControllerName);
        }

        [Fact]
        public async Task Should_Call_Next_When_User_Not_Blocked()
        {
            var UserManagerMock = GetManagerMock();
            var context = GetContext();

            UserManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync((User)null);
            var filter = new BlockResourseFilter(UserManagerMock.Object);

            bool nextCalled = false;
            ResourceExecutionDelegate next = () => 
            {
                nextCalled = true;
                return Task.FromResult<ResourceExecutedContext>(null);
            };

            await filter.OnResourceExecutionAsync(context, next);

            Assert.True(nextCalled);
            Assert.IsNotType<RedirectToActionResult>(context.Result);
        }

        [Fact]
        public async Task Should_Call_Next_When_User_Is_Null()
        {
            var UserManagerMock = GetManagerMock();
            var context = GetContext();
            User us = new User()
            {
                Id = 1,
                LockoutEnabled = false,
            };

            var filter = new BlockResourseFilter(UserManagerMock.Object);
            bool nextCalled = false;
            ResourceExecutionDelegate next = () =>
            {
                nextCalled = true;
                return Task.FromResult<ResourceExecutedContext>(null);
            };

            await filter.OnResourceExecutionAsync(context, next);

            Assert.True(nextCalled);
            Assert.IsNotType<RedirectToActionResult>(context.Result);
        }
    }
}
