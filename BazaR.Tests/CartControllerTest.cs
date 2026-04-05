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
    public class CartControllerTests : IDisposable
    {
        private readonly Mock<IUserDb> _mockUserDb;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly AppDbContext _dbContext;
        private readonly CartController _controller;
        private readonly List<Item> _testItems;
        private readonly List<User> _testUsers;
        private readonly List<CartItem> _testCartItems;

        public CartControllerTests()
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
            _testCartItems = GetTestCartItems();

            _dbContext.Items.AddRange(_testItems);
            _dbContext.Users.AddRange(_testUsers);
            _dbContext.SaveChanges();

            SetupMocks();

            _controller = new CartController(
                _mockUserDb.Object,
                _mockUserManager.Object);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        private void SetupMocks()
        {
            _mockUserDb.Setup(x => x.GetCartItemsWithQuantity(It.IsAny<int>()))
                .Returns<int>(userId =>
                    _testCartItems
                        .Where(ci => ci.UserId == userId)
                        .Select(ci => new CartItem
                        {
                            Id = ci.Id,
                            UserId = ci.UserId,
                            ItemId = ci.ItemId,
                            Quantity = ci.Quantity,
                            Item = _testItems.FirstOrDefault(i => i.Id == ci.ItemId)
                        })
                        .ToList());

            _mockUserDb.Setup(x => x.AddToCart(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            _mockUserDb.Setup(x => x.RemoveFromCart(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            _mockUserDb.Setup(x => x.ClearCart(It.IsAny<int>()))
                .Returns(true);

            _mockUserDb.Setup(x => x.CreateOrder(It.IsAny<int>(), It.IsAny<Order>()))
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

        private List<CartItem> GetTestCartItems()
        {
            return new List<CartItem>
            {
                new CartItem { Id = 1, UserId = 1, ItemId = 1, Quantity = 1 },
                new CartItem { Id = 2, UserId = 1, ItemId = 2, Quantity = 2 },
                new CartItem { Id = 3, UserId = 1, ItemId = 3, Quantity = 1 }
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
                .Returns((UrlActionContext context) => $"/{context.Controller ?? "Cart"}/{context.Action}");

            var actionDescriptor = new ControllerActionDescriptor
            {
                ControllerName = "Cart",
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
            Assert.Equal("Cart", viewResult.ViewName);
            var model = Assert.IsType<List<Item>>(viewResult.Model);
            Assert.Equal(3, model.Count);
            Assert.NotNull(viewResult.ViewData["CartItems"]);
            Assert.NotNull(viewResult.ViewData["TotalAmount"]);
            Assert.NotNull(viewResult.ViewData["TotalQuantity"]);
        }

        [Fact]
        public async Task Index_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();
            var result = await _controller.Index();
            Assert.IsType<ChallengeResult>(result);
        }

        [Fact]
        public async Task Add_WhenAuthenticated_AddsItemToCart()
        {
            SetupAuthenticatedUser();
            var result = await _controller.Add(3, 2);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.AddToCart(1, 3), Times.Exactly(2));
        }

        [Fact]
        public async Task Add_WithInvalidQuantity_UsesDefaultQuantity()
        {
            SetupAuthenticatedUser();
            var result = await _controller.Add(1, 0);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.AddToCart(1, 1), Times.Once);
        }

        [Fact]
        public async Task Add_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();
            var result = await _controller.Add(1);
            Assert.IsType<ChallengeResult>(result);
        }

        [Fact]
        public async Task Remove_WhenAuthenticated_RemovesItem()
        {
            SetupAuthenticatedUser();
            var result = await _controller.Remove(1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.RemoveFromCart(1, 1), Times.Once);
        }

        [Fact]
        public async Task Remove_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();
            var result = await _controller.Remove(1);
            Assert.IsType<ChallengeResult>(result);
        }

        [Fact]
        public async Task Clear_WhenAuthenticated_ClearsCart()
        {
            SetupAuthenticatedUser();
            var result = await _controller.Clear();
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.ClearCart(1), Times.Once);
        }

        [Fact]
        public async Task Clear_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();
            var result = await _controller.Clear();
            Assert.IsType<ChallengeResult>(result);
        }

        [Fact]
        public async Task Checkout_WhenAuthenticated_ReturnsView()
        {
            SetupAuthenticatedUser();
            var result = await _controller.Checkout();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Item>>(viewResult.Model);
            Assert.Equal(3, model.Count);
            Assert.NotNull(viewResult.ViewData["CartItems"]);
        }

        [Fact]
        public async Task Checkout_WhenCartEmpty_RedirectsToIndexWithError()
        {
            SetupAuthenticatedUser();
            _mockUserDb.Setup(x => x.GetCartItemsWithQuantity(1))
                .Returns(new List<CartItem>());

            var result = await _controller.Checkout();
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            Assert.Equal("Корзина пустая.", _controller.TempData["Error"]);
        }

        [Fact]
        public async Task Checkout_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();
            var result = await _controller.Checkout();
            Assert.IsType<ChallengeResult>(result);
        }

        [Fact]
        public async Task CreateOrder_WhenAuthenticated_CreatesOrder()
        {
            SetupAuthenticatedUser();
            var result = await _controller.CreateOrder("Test Address", "Cash", "Courier");
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.CreateOrder(1, It.IsAny<Order>()), Times.Once);
            _mockUserDb.Verify(x => x.ClearCart(1), Times.Once);
            Assert.Equal("Заказ создан!", _controller.TempData["Ok"]);
        }

        [Fact]
        public async Task CreateOrder_WhenCartEmpty_RedirectsToIndexWithError()
        {
            SetupAuthenticatedUser();
            _mockUserDb.Setup(x => x.GetCartItemsWithQuantity(1))
                .Returns(new List<CartItem>());

            var result = await _controller.CreateOrder("Address", "Cash", "Courier");
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            Assert.Equal("Корзина пустая.", _controller.TempData["Error"]);
            _mockUserDb.Verify(x => x.CreateOrder(It.IsAny<int>(), It.IsAny<Order>()), Times.Never);
        }

        [Fact]
        public async Task CreateOrder_WhenCreateOrderFails_RedirectsToCheckoutWithError()
        {
            SetupAuthenticatedUser();
            _mockUserDb.Setup(x => x.CreateOrder(It.IsAny<int>(), It.IsAny<Order>()))
                .Returns(false);

            var result = await _controller.CreateOrder("Test Address", "Cash", "Courier");
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Checkout), redirectResult.ActionName);
            Assert.Equal("Не удалось создать заказ.", _controller.TempData["Error"]);
        }

        [Fact]
        public async Task CreateOrder_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();
            var result = await _controller.CreateOrder("Address", "Cash", "Courier");
            Assert.IsType<ChallengeResult>(result);
        }

        [Fact]
        public async Task CreateOrder_CalculatesTotalAmountCorrectly()
        {
            SetupAuthenticatedUser();

            Order createdOrder = null;
            _mockUserDb.Setup(x => x.CreateOrder(It.IsAny<int>(), It.IsAny<Order>()))
                .Callback<int, Order>((userId, order) => createdOrder = order)
                .Returns(true);

            var result = await _controller.CreateOrder("Test Address", "Cash", "Courier");

            Assert.NotNull(createdOrder);
            Assert.Equal(5800, createdOrder.TotalAmount);
            Assert.Equal(3, createdOrder.OrderItems.Count);
            Assert.Equal(OrderStatus.New, createdOrder.Status);
            Assert.Equal(OrderPaymentMethod.PayNow, createdOrder.PaymentMethod);
            Assert.Equal(OrderDeliveryMethod.SelfPickup, createdOrder.DeliveryMethod);
            Assert.Equal(OrderPaymentStatus.Pending, createdOrder.PaymentStatus);
            Assert.Equal("Test Address", createdOrder.Address);
        }
    }
}