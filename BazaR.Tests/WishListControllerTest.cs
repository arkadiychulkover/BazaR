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
using Xunit;

namespace BazaR.Tests
{
    public class WishListControllerTest : IDisposable
    {
        private readonly Mock<IUserDb> _mockUserDb;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly AppDbContext _dbContext;
        private readonly WishlistController _controller;
        private readonly List<Item> _testItems;
        private readonly List<User> _testUsers;
        private readonly List<WishlistItem> _testWishlistItems;

        public WishListControllerTest()
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
                .Returns(true)
                .Verifiable();

            _mockUserDb.Setup(x => x.RemoveFromWishList(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true)
                .Verifiable();

            var currentUser = _testUsers[0];
            _mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(currentUser);
        }

        private List<Item> GetTestItems()
        {
            return new List<Item>
            {
                new Item
                {
                    Id = 1,
                    Name = "MacBook Pro",
                    Desc = "Powerful laptop with M1 chip",
                    Price = 2000,
                    CategoryId = 2,
                    BrandId = 1,
                    UserId = 1,
                    IsAvailable = true,
                    Garantia = 12,
                    ImageUrl = "/images/test.jpg"
                },
                new Item
                {
                    Id = 2,
                    Name = "Dell XPS",
                    Desc = "High-end Windows laptop",
                    Price = 1500,
                    CategoryId = 2,
                    BrandId = 2,
                    UserId = 1,
                    IsAvailable = true,
                    Garantia = 24
                },
                new Item
                {
                    Id = 3,
                    Name = "iPhone 13",
                    Desc = "Latest iPhone model",
                    Price = 800,
                    CategoryId = 3,
                    BrandId = 1,
                    UserId = 1,
                    IsAvailable = false,
                    Garantia = 12
                }
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
                new User
                {
                    Id = 1,
                    Name = "Test User",
                    Email = "test@example.com",
                    UserName = "test@example.com",
                    IsAdmin = false
                },
                new User
                {
                    Id = 2,
                    Name = "Admin User",
                    Email = "admin@example.com",
                    UserName = "admin@example.com",
                    IsAdmin = true
                }
            };
        }

        private void SetupControllerContext()
        {
            var httpContext = new DefaultHttpContext();

            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.Setup(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("/Site/Index");

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
        }

        private void SetupUnauthenticatedUser()
        {
            SetupControllerContext();

            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _controller.ControllerContext.HttpContext.User = claimsPrincipal;
        }

        [Fact]
        public void Index_WhenAuthenticated_ReturnsViewWithItems()
        {
            SetupAuthenticatedUser(1);
            var result = _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Item>>(viewResult.Model);
            Assert.Equal(2, model.Count);
            Assert.Contains(model, i => i.Id == 1);
            Assert.Contains(model, i => i.Id == 2);
            Assert.DoesNotContain(model, i => i.Id == 3);
        }

        [Fact]
        public void Index_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();
            var result = _controller.Index();
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Site", redirectResult.ControllerName);
            Assert.Equal("Нужно войти в аккаунт.", _controller.TempData["Error"]);
        }

        [Fact]
        public void Add_WhenAuthenticated_AddsItemToWishlist()
        {
            SetupAuthenticatedUser(1);
            int itemId = 3;
            var result = _controller.Add(itemId);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(WishlistController.Index), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.AddToWishList(1, itemId), Times.Once);
        }

        [Fact]
        public void Add_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();
            int itemId = 1;
            var result = _controller.Add(itemId);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Site", redirectResult.ControllerName);
            _mockUserDb.Verify(x => x.AddToWishList(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Remove_WhenAuthenticated_RemovesItemFromWishlist()
        {
            SetupAuthenticatedUser(1);
            int itemId = 1;
            var result = _controller.Remove(itemId);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(WishlistController.Index), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.RemoveFromWishList(1, itemId), Times.Once);
        }

        [Fact]
        public void Remove_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();
            int itemId = 1;
            var result = _controller.Remove(itemId);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Site", redirectResult.ControllerName);
            _mockUserDb.Verify(x => x.RemoveFromWishList(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Add_WhenItemAlreadyInWishlist_StillCallsAddMethod()
        {
            SetupAuthenticatedUser(1);
            int itemId = 1;
            var result = _controller.Add(itemId);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(WishlistController.Index), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.AddToWishList(1, itemId), Times.Once);
        }

        [Fact]
        public void Add_ThenIndex_ShowsNewItem()
        {
            SetupAuthenticatedUser(1);
            int newItemId = 3;

            var initialResult = _controller.Index();
            var initialView = Assert.IsType<ViewResult>(initialResult);
            var initialModel = Assert.IsType<List<Item>>(initialView.Model);
            Assert.DoesNotContain(initialModel, i => i.Id == newItemId);

            _controller.Add(newItemId);

            _mockUserDb.Setup(x => x.GetWishList(1))
                .Returns(new List<Item>
                {
                    _testItems.First(i => i.Id == 1),
                    _testItems.First(i => i.Id == 2),
                    _testItems.First(i => i.Id == 3)
                }.AsQueryable());

            var resultAfterAdd = _controller.Index();
            var viewResultAfterAdd = Assert.IsType<ViewResult>(resultAfterAdd);
            var modelAfterAdd = Assert.IsType<List<Item>>(viewResultAfterAdd.Model);

            Assert.Contains(modelAfterAdd, i => i.Id == newItemId);
            Assert.Equal(3, modelAfterAdd.Count);
        }

        [Fact]
        public void Remove_ThenIndex_RemovesItemFromList()
        {
            SetupAuthenticatedUser(1);
            int itemToRemove = 1;

            var initialResult = _controller.Index();
            var initialView = Assert.IsType<ViewResult>(initialResult);
            var initialModel = Assert.IsType<List<Item>>(initialView.Model);
            Assert.Contains(initialModel, i => i.Id == itemToRemove);
            Assert.Equal(2, initialModel.Count);

            _controller.Remove(itemToRemove);

            _mockUserDb.Setup(x => x.GetWishList(1))
                .Returns(new List<Item>
                {
                    _testItems.First(i => i.Id == 2)
                }.AsQueryable());

            var resultAfterRemove = _controller.Index();
            var viewResultAfterRemove = Assert.IsType<ViewResult>(resultAfterRemove);
            var modelAfterRemove = Assert.IsType<List<Item>>(viewResultAfterRemove.Model);

            Assert.DoesNotContain(modelAfterRemove, i => i.Id == itemToRemove);
            Assert.Single(modelAfterRemove);
            Assert.Equal(2, modelAfterRemove[0].Id);
        }
    }
}