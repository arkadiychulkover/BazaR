using BazaR.Controllers;
using BazaR.Data;
using BazaR.Interfaces;
using BazaR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;

namespace BazaR.Tests.Controllers
{
    public class WishlistControllerTests : IDisposable
    {
        private readonly Mock<IUserDb> _mockUserDb;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly AppDbContext _dbContext;
        private readonly WishlistController _controller;
        private readonly List<Item> _testItems;
        private readonly List<User> _testUsers;
        private readonly List<WishlistItem> _testWishlistItems;

        public WishlistControllerTests()
        {
            _mockUserDb = new Mock<IUserDb>();

            var userStoreMock = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _dbContext = new AppDbContext(options);

            _testItems = GetTestItems();
            _testUsers = GetTestUsers();
            _testWishlistItems = GetTestWishlistItems();

            _dbContext.Items.AddRange(_testItems);
            _dbContext.Users.AddRange(_testUsers);
            _dbContext.SaveChanges();

            SetupMocks();

            _controller = new WishlistController(
                _mockUserDb.Object,
                _mockUserManager.Object);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        private void SetupMocks()
        {
            _mockUserDb.Setup(x => x.GetWishList(It.IsAny<int>()))
                .Returns<int>(userId =>
                {
                    var wishlistItemIds = _testWishlistItems
                        .Where(wi => wi.UserId == userId)
                        .Select(wi => wi.ItemId)
                        .ToList();
                    return _testItems
                        .Where(i => wishlistItemIds.Contains(i.Id))
                        .AsQueryable();
                });

            _mockUserDb.Setup(x => x.AddToWishList(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            _mockUserDb.Setup(x => x.RemoveFromWishList(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);
        }

        private List<Item> GetTestItems()
        {
            return new List<Item>
            {
                new Item { Id = 1, Name = "MacBook Pro", Desc = "Powerful laptop", Price = 2000, IsAvailable = true, Garantia = 12, CategoryId = 1, BrandId = 1, UserId = 1, ImageUrl = "/test1.jpg" },
                new Item { Id = 2, Name = "Dell XPS", Desc = "High-end laptop", Price = 1500, IsAvailable = true, Garantia = 24, CategoryId = 1, BrandId = 1, UserId = 1, ImageUrl = "/test2.jpg" },
                new Item { Id = 3, Name = "iPhone 13", Desc = "Latest iPhone", Price = 800, IsAvailable = false, Garantia = 12, CategoryId = 1, BrandId = 1, UserId = 1, ImageUrl = "/test3.jpg" }
            };
        }

        private List<WishlistItem> GetTestWishlistItems()
        {
            return new List<WishlistItem>
            {
                new WishlistItem { Id = 1, UserId = 1, ItemId = 1 },
                new WishlistItem { Id = 2, UserId = 1, ItemId = 2 }
            };
        }

        private List<User> GetTestUsers()
        {
            return new List<User>
            {
                new User { Id = 1, Name = "Test User", Email = "test@example.com", UserName = "test@example.com",
                          FirstName = "Test", LastName = "User", PhoneNumber = "1234567890", IsAdmin = false },
                new User { Id = 2, Name = "Admin User", Email = "admin@example.com", UserName = "admin@example.com",
                          FirstName = "Admin", LastName = "User", PhoneNumber = "0987654321", IsAdmin = true }
            };
        }

        private void SetupControllerContext()
        {
            var httpContext = new DefaultHttpContext();

            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.Setup(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns((UrlActionContext context) => $"/{context.Controller ?? "Wishlist"}/{context.Action}");

            var actionDescriptor = new ControllerActionDescriptor
            {
                ControllerName = "Wishlist",
                ActionName = "Index"
            };

            var actionContext = new ActionContext(httpContext, new RouteData(), actionDescriptor);

            _controller.ControllerContext = new ControllerContext(actionContext);
            _controller.Url = urlHelper.Object;

            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _controller.TempData = tempData;
        }

        private void SetupAuthenticatedUser(int userId = 1)
        {
            SetupControllerContext();

            var user = _testUsers.FirstOrDefault(u => u.Id == userId);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, user?.Name ?? "Test User"),
                new Claim(ClaimTypes.Email, user?.Email ?? "test@example.com")
            };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _controller.ControllerContext.HttpContext.User = claimsPrincipal;

            _mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);
        }

        private void SetupUnauthenticatedUser()
        {
            SetupControllerContext();

            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _controller.ControllerContext.HttpContext.User = claimsPrincipal;
        }

        [Fact]
        public async Task Index_WhenAuthenticated_ReturnsViewWithItems()
        {
            SetupAuthenticatedUser();
            var result = await _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Item>>(viewResult.Model);
            Assert.Equal(2, model.Count);
            Assert.Contains(model, i => i.Id == 1);
            Assert.Contains(model, i => i.Id == 2);
        }

        [Fact]
        public async Task Index_WhenAuthenticatedWithEmptyWishlist_ReturnsEmptyList()
        {
            SetupAuthenticatedUser();

            _mockUserDb.Setup(x => x.GetWishList(1))
                .Returns(new List<Item>().AsQueryable());

            var result = await _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Item>>(viewResult.Model);
            Assert.Empty(model);
        }

        [Fact]
        public async Task Index_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();
            var result = await _controller.Index();
            Assert.IsType<ChallengeResult>(result);
        }

        [Fact]
        public async Task Add_WhenAuthenticated_AddsItemToWishlist()
        {
            SetupAuthenticatedUser();
            var result = await _controller.Add(3);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(WishlistController.Index), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.AddToWishList(1, 3), Times.Once);
        }

        [Fact]
        public async Task Add_WhenAuthenticated_AddsExistingItemAgain()
        {
            SetupAuthenticatedUser();
            var result = await _controller.Add(1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(WishlistController.Index), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.AddToWishList(1, 1), Times.Once);
        }

        [Fact]
        public async Task Add_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();
            var result = await _controller.Add(1);
            Assert.IsType<ChallengeResult>(result);
        }

        [Fact]
        public async Task Remove_WhenAuthenticated_RemovesItemFromWishlist()
        {
            SetupAuthenticatedUser();
            var result = await _controller.Remove(1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(WishlistController.Index), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.RemoveFromWishList(1, 1), Times.Once);
        }

        [Fact]
        public async Task Remove_WhenAuthenticated_RemovesNonExistentItem()
        {
            SetupAuthenticatedUser();
            var result = await _controller.Remove(999);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(WishlistController.Index), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.RemoveFromWishList(1, 999), Times.Once);
        }

        [Fact]
        public async Task Remove_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();
            var result = await _controller.Remove(1);
            Assert.IsType<ChallengeResult>(result);
        }

        [Fact]
        public async Task Add_WithMultipleItems_AddsEachItem()
        {
            SetupAuthenticatedUser();

            await _controller.Add(1);
            await _controller.Add(2);
            await _controller.Add(3);

            _mockUserDb.Verify(x => x.AddToWishList(1, 1), Times.Once);
            _mockUserDb.Verify(x => x.AddToWishList(1, 2), Times.Once);
            _mockUserDb.Verify(x => x.AddToWishList(1, 3), Times.Once);
        }

        [Fact]
        public async Task Remove_WithMultipleItems_RemovesEachItem()
        {
            SetupAuthenticatedUser();

            await _controller.Remove(1);
            await _controller.Remove(2);

            _mockUserDb.Verify(x => x.RemoveFromWishList(1, 1), Times.Once);
            _mockUserDb.Verify(x => x.RemoveFromWishList(1, 2), Times.Once);
        }

        [Fact]
        public async Task Index_AfterAddingItem_ShowsUpdatedWishlist()
        {
            SetupAuthenticatedUser();

            var resultBefore = await _controller.Index();
            var viewResultBefore = Assert.IsType<ViewResult>(resultBefore);
            var modelBefore = Assert.IsType<List<Item>>(viewResultBefore.Model);
            Assert.Equal(2, modelBefore.Count);

            _mockUserDb.Setup(x => x.GetWishList(1))
                .Returns(() =>
                {
                    var wishlistItemIds = _testWishlistItems
                        .Where(wi => wi.UserId == 1)
                        .Select(wi => wi.ItemId)
                        .ToList();
                    if (!wishlistItemIds.Contains(3))
                        wishlistItemIds.Add(3);
                    return _testItems
                        .Where(i => wishlistItemIds.Contains(i.Id))
                        .AsQueryable();
                });

            var resultAfter = await _controller.Index();
            var viewResultAfter = Assert.IsType<ViewResult>(resultAfter);
            var modelAfter = Assert.IsType<List<Item>>(viewResultAfter.Model);
            Assert.Equal(3, modelAfter.Count);
        }
    }
}