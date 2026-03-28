using BazaR.Filters;
using BazaR.Interfaces;
using BazaR.Models;
using BazaR.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BazaR.Tests
{
    public class OnlineResourceFilterTests
    {
        private Mock<UserManager<User>> GetUserManagerMock()
        {
            var store = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        }

        private ResourceExecutingContext GetResourceContext(string controller = "Home", string action = "Index", string queryCategory = null)
        {
            var httpContext = new DefaultHttpContext();
            if (queryCategory != null)
                httpContext.Request.QueryString = new QueryString($"?category={queryCategory}");

            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "test") }, "mock"));

            var routeData = new RouteData();
            routeData.Values["controller"] = controller;
            routeData.Values["action"] = action;

            var actionContext = new Microsoft.AspNetCore.Mvc.ActionContext(httpContext, routeData, new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());

            return new ResourceExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new List<Microsoft.AspNetCore.Mvc.ModelBinding.IValueProviderFactory>()
            );
        }

        [Fact]
        public async Task Should_Call_AddUser_And_PingUser_When_UserExists()
        {
            var userManagerMock = GetUserManagerMock();
            var statsRepoMock = new Mock<IUserStatistick>();
            var activeUsers = new ActiveUsersService();

            var user = new User { Id = 1, Name = "TestUser" };
            userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);

            var filter = new OnlineResourceFilter(activeUsers, userManagerMock.Object, statsRepoMock.Object);
            var context = GetResourceContext();

            bool nextCalled = false;
            ResourceExecutionDelegate next = () =>
            {
                nextCalled = true;
                return Task.FromResult<ResourceExecutedContext>(null);
            };

            await filter.OnResourceExecutionAsync(context, next);

            userManagerMock.Verify(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);
            statsRepoMock.Verify(x => x.AddUser(user), Times.Once);
            Assert.True(activeUsers.GetUsers().ContainsKey(user.Id));
            Assert.True(nextCalled);
        }

        [Fact]
        public async Task Should_Call_AddUserCategoryVisit_When_CategoryPage_And_ValidCategory()
        {
            var userManagerMock = GetUserManagerMock();
            var statsRepoMock = new Mock<IUserStatistick>();
            var activeUsers = new ActiveUsersService();

            var user = new User { Id = 1, Name = "TestUser" };
            userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);

            var filter = new OnlineResourceFilter(activeUsers, userManagerMock.Object, statsRepoMock.Object);
            var context = GetResourceContext(controller: "Site", action: "CategoryPage", queryCategory: "5");

            bool nextCalled = false;
            ResourceExecutionDelegate next = () =>
            {
                nextCalled = true;
                return Task.FromResult<ResourceExecutedContext>(null);
            };

            await filter.OnResourceExecutionAsync(context, next);

            statsRepoMock.Verify(x => x.AddUserCategoryVisit(user, 5), Times.Once);
            Assert.True(nextCalled);
        }

        [Fact]
        public async Task Should_Not_Call_AddUserCategoryVisit_When_InvalidCategory()
        {
            var userManagerMock = GetUserManagerMock();
            var statsRepoMock = new Mock<IUserStatistick>();
            var activeUsers = new ActiveUsersService();

            var user = new User { Id = 1, Name = "TestUser" };
            userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);

            var filter = new OnlineResourceFilter(activeUsers, userManagerMock.Object, statsRepoMock.Object);
            var context = GetResourceContext(controller: "Site", action: "CategoryPage", queryCategory: "abc");

            bool nextCalled = false;
            ResourceExecutionDelegate next = () =>
            {
                nextCalled = true;
                return Task.FromResult<ResourceExecutedContext>(null);
            };

            await filter.OnResourceExecutionAsync(context, next);

            statsRepoMock.Verify(x => x.AddUserCategoryVisit(It.IsAny<User>(), It.IsAny<int>()), Times.Never);
            Assert.True(nextCalled);
        }

        [Fact]
        public async Task Should_Call_Next_Even_When_UserIsNull()
        {
            var userManagerMock = GetUserManagerMock();
            var statsRepoMock = new Mock<IUserStatistick>();
            var activeUsers = new ActiveUsersService();

            userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync((User)null);

            var filter = new OnlineResourceFilter(activeUsers, userManagerMock.Object, statsRepoMock.Object);
            var context = GetResourceContext();

            bool nextCalled = false;
            ResourceExecutionDelegate next = () =>
            {
                nextCalled = true;
                return Task.FromResult<ResourceExecutedContext>(null);
            };

            await filter.OnResourceExecutionAsync(context, next);

            Assert.Empty(activeUsers.GetUsers());
            statsRepoMock.Verify(x => x.AddUser(It.IsAny<User>()), Times.Never);
            Assert.True(nextCalled);
        }
    }
}