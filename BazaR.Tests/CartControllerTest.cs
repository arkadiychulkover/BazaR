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
    public class CartControllerTest : IDisposable
    {
        private readonly Mock<IUserDb> _mockUserDb;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly AppDbContext _dbContext;
        private readonly CartController _controller;
        private readonly List<Item> _testItems;
        private readonly List<User> _testUsers;
        private readonly List<CartItem> _testCartItems;

        public CartControllerTest()
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
            // Мок для получения товаров в корзине с количеством
            _mockUserDb.Setup(x => x.GetCartItemsWithQuantity(It.IsAny<int>()))
                .Returns<int>(userId =>
                    _testCartItems
                        .Where(ci => ci.UserId == userId)
                        .Select(ci => new CartItem
                        {
                            ItemId = ci.ItemId,
                            Item = _testItems.FirstOrDefault(i => i.Id == ci.ItemId),
                            Quantity = ci.Quantity
                        })
                        .ToList());

            // Мок для добавления товара в корзину
            _mockUserDb.Setup(x => x.AddToCart(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true)
                .Verifiable();

            // Мок для удаления товара из корзины
            _mockUserDb.Setup(x => x.RemoveFromCart(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true)
                .Verifiable();

            // Мок для очистки корзины
            _mockUserDb.Setup(x => x.ClearCart(It.IsAny<int>()))
                .Returns(true)
                .Verifiable();

            // Мок для создания заказа
            _mockUserDb.Setup(x => x.CreateOrder(It.IsAny<int>(), It.IsAny<Order>()))
                .Returns<int, Order>((userId, order) =>
                {
                    order.Id = 1;
                    order.Number = "ORDER-001";
                    return true;
                })
                .Verifiable();

            // Мок для получения пользователя
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

        private List<CartItem> GetTestCartItems()
        {
            return new List<CartItem>
            {
                new CartItem
                {
                    Id = 1,
                    UserId = 1,
                    ItemId = 1,
                    Quantity = 1
                },
                new CartItem
                {
                    Id = 2,
                    UserId = 1,
                    ItemId = 2,
                    Quantity = 2
                },
                new CartItem
                {
                    Id = 3,
                    UserId = 1,
                    ItemId = 3,
                    Quantity = 1
                }
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
            SetupAuthenticatedUser();

            var result = _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Item>>(viewResult.Model);

            Assert.Equal(3, model.Count);
            Assert.NotNull(viewResult.ViewData["CartItems"]);
            Assert.NotNull(viewResult.ViewData["TotalAmount"]);
            Assert.NotNull(viewResult.ViewData["TotalQuantity"]);

            var cartItems = viewResult.ViewData["CartItems"] as Dictionary<int, int>;
            Assert.NotNull(cartItems);
            Assert.Equal(1, cartItems[1]); // MacBook Pro: 1 штука
            Assert.Equal(2, cartItems[2]); // Dell XPS: 2 штуки
            Assert.Equal(1, cartItems[3]); // iPhone 13: 1 штука

            var totalAmount = (int)viewResult.ViewData["TotalAmount"];
            var expectedTotal = (2000 * 1) + (1500 * 2) + (800 * 1); // 2000 + 3000 + 800 = 5800
            Assert.Equal(expectedTotal, totalAmount);

            var totalQuantity = (int)viewResult.ViewData["TotalQuantity"];
            Assert.Equal(4, totalQuantity);
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
        public void Add_WhenAuthenticated_AddsItemToCart()
        {
            SetupAuthenticatedUser(1);
            int itemId = 1;
            int quantity = 2;

            var result = _controller.Add(itemId, quantity);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.AddToCart(1, itemId), Times.Exactly(quantity));
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
            _mockUserDb.Verify(x => x.AddToCart(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Remove_WhenAuthenticated_RemovesItemFromCart()
        {
            SetupAuthenticatedUser(1);
            int itemId = 1;

            var result = _controller.Remove(itemId);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.RemoveFromCart(1, itemId), Times.Once);
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
            _mockUserDb.Verify(x => x.RemoveFromCart(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Clear_WhenAuthenticated_ClearsCart()
        {
            SetupAuthenticatedUser(1);

            var result = _controller.Clear();

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.ClearCart(1), Times.Once);
        }

        [Fact]
        public void Clear_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();

            var result = _controller.Clear();

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Site", redirectResult.ControllerName);
            _mockUserDb.Verify(x => x.ClearCart(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Checkout_WhenAuthenticated_ReturnsView()
        {
            SetupAuthenticatedUser(1);

            var result = _controller.Checkout();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Item>>(viewResult.Model);
            Assert.Equal(3, model.Count);
            Assert.NotNull(viewResult.ViewData["CartItems"]);
        }

        [Fact]
        public void Checkout_WhenCartEmpty_RedirectsToIndex()
        {
            SetupAuthenticatedUser(1);

            _mockUserDb.Setup(x => x.GetCartItemsWithQuantity(1))
                .Returns(new List<CartItem>());

            var result = _controller.Checkout();

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            Assert.Equal("Корзина пустая.", _controller.TempData["Error"]);
        }

        [Fact]
        public void Checkout_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();

            var result = _controller.Checkout();

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Site", redirectResult.ControllerName);
        }

        [Fact]
        public void CreateOrder_WhenAuthenticated_CreatesOrder()
        {
            SetupAuthenticatedUser(1);
            string address = "Test Address 123";
            string paymentMethod = "Cash";
            string deliveryMethod = "Courier";

            var result = _controller.CreateOrder(address, paymentMethod, deliveryMethod);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.CreateOrder(1, It.IsAny<Order>()), Times.Once);
            _mockUserDb.Verify(x => x.ClearCart(1), Times.Once);
            Assert.Equal("Заказ создан!", _controller.TempData["Ok"]);
        }

        [Fact]
        public void CreateOrder_WhenCartEmpty_RedirectsToIndex()
        {
            SetupAuthenticatedUser(1);

            _mockUserDb.Setup(x => x.GetCartItemsWithQuantity(1))
                .Returns(new List<CartItem>());

            var result = _controller.CreateOrder("Address", "Cash", "Courier");

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            Assert.Equal("Корзина пустая.", _controller.TempData["Error"]);
            _mockUserDb.Verify(x => x.CreateOrder(It.IsAny<int>(), It.IsAny<Order>()), Times.Never);
        }

        [Fact]
        public void CreateOrder_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();

            var result = _controller.CreateOrder("Address", "Cash", "Courier");

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Site", redirectResult.ControllerName);
            _mockUserDb.Verify(x => x.CreateOrder(It.IsAny<int>(), It.IsAny<Order>()), Times.Never);
        }

        [Fact]
        public void Add_WithQuantityZero_SetsQuantityToOne()
        {
            SetupAuthenticatedUser(1);
            int itemId = 1;
            int quantity = 0;

            var result = _controller.Add(itemId, quantity);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.AddToCart(1, itemId), Times.Exactly(1));
        }

        [Fact]
        public void Add_WithNegativeQuantity_SetsQuantityToOne()
        {
            SetupAuthenticatedUser(1);
            int itemId = 1;
            int quantity = -5;

            var result = _controller.Add(itemId, quantity);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(CartController.Index), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.AddToCart(1, itemId), Times.Exactly(1));
        }

        [Fact]
        public void CreateOrder_CalculatesTotalAmountCorrectly()
        {
            SetupAuthenticatedUser(1);
            string address = "Test Address";
            string paymentMethod = "Cash";
            string deliveryMethod = "Courier";

            Order createdOrder = null;
            _mockUserDb.Setup(x => x.CreateOrder(It.IsAny<int>(), It.IsAny<Order>()))
                .Callback<int, Order>((userId, order) => createdOrder = order)
                .Returns(true);

            var result = _controller.CreateOrder(address, paymentMethod, deliveryMethod);

            Assert.NotNull(createdOrder);
            decimal expectedTotal = (2000 * 1) + (1500 * 2) + (800 * 1);
            Assert.Equal(expectedTotal, createdOrder.TotalAmount);
            Assert.Equal(address, createdOrder.Address);
            Assert.Equal(paymentMethod, createdOrder.PaymentMethod);
            Assert.Equal(deliveryMethod, createdOrder.DeliveryMethod);
            Assert.Equal(3, createdOrder.OrderItems.Count);
        }
    }
}