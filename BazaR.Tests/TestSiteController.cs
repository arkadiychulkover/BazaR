// ==================== BazaR.Tests/Controllers/SiteControllerTests.cs ====================
using BazaR.Controllers;
using BazaR.Data;
using BazaR.Interfaces;
using BazaR.Models;
using BazaR.Models.BazaR.Models;
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

namespace BazaR.Tests.Controllers
{
    public class SiteControllerTests : IDisposable
    {
        private readonly Mock<IUserDb> _mockUserDb;
        private readonly Mock<IItemRepository> _mockItemRepo;
        private readonly Mock<ILogDb> _mockLogDb;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly AppDbContext _dbContext;
        private readonly SiteController _controller;
        private readonly List<Category> _testCategories;
        private readonly List<Item> _testItems;
        private readonly List<User> _testUsers;
        private readonly List<CartItem> _testCartItems;
        private readonly List<WishlistItem> _testWishlistItems;

        public SiteControllerTests()
        {
            _mockUserDb = new Mock<IUserDb>();
            _mockItemRepo = new Mock<IItemRepository>();
            _mockLogDb = new Mock<ILogDb>();

            var userStoreMock = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            _dbContext = new AppDbContext(options);

            _testCategories = GetTestCategories();
            _testItems = GetTestItems();
            _testUsers = GetTestUsers();
            _testCartItems = GetTestCartItems();
            _testWishlistItems = GetTestWishlistItems();

            _dbContext.Categories.AddRange(_testCategories);
            _dbContext.Items.AddRange(_testItems);
            _dbContext.Users.AddRange(_testUsers);
            _dbContext.SaveChanges();

            SetupMocks();

            _controller = new SiteController(
                _mockUserDb.Object,
                _mockItemRepo.Object,
                _dbContext,
                _mockUserManager.Object,
                _mockLogDb.Object);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        private void SetupMocks()
        {
            _mockUserDb.Setup(x => x.GetCartItems(It.IsAny<int>()))
                .Returns<int>(userId =>
                {
                    var cartItemIds = _testCartItems
                        .Where(ci => ci.UserId == userId)
                        .Select(ci => ci.ItemId)
                        .ToList();
                    return _testItems
                        .Where(i => cartItemIds.Contains(i.Id))
                        .AsQueryable();
                });

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

            _mockUserDb.Setup(x => x.AddToCart(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            _mockUserDb.Setup(x => x.RemoveFromCart(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            _mockUserDb.Setup(x => x.SetCartQuantity(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            _mockUserDb.Setup(x => x.ClearCart(It.IsAny<int>()))
                .Returns(true);

            _mockUserDb.Setup(x => x.AddToWishList(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            _mockUserDb.Setup(x => x.RemoveFromWishList(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            _mockUserDb.Setup(x => x.CreateOrder(It.IsAny<int>(), It.IsAny<Order>()))
                .Returns<int, Order>((userId, order) =>
                {
                    order.Id = 1;
                    order.Number = $"ORDER-001";
                    return true;
                });

            _mockUserDb.Setup(x => x.GetOrderById(It.IsAny<int>()))
                .Returns<int>(id => new Order
                {
                    Id = id,
                    Number = $"ORDER-{id}",
                    UserId = 1,
                    TotalAmount = 1000,
                    Status = OrderStatus.New,
                    CreatedAt = DateTime.UtcNow
                });

            _mockUserDb.Setup(x => x.GetUserOrders(It.IsAny<int>()))
                .Returns<int>(userId => new List<Order>
                {
                    new Order { Id = 1, Number = "ORDER-001", UserId = userId, TotalAmount = 1000, Status = OrderStatus.New, CreatedAt = DateTime.UtcNow },
                    new Order { Id = 2, Number = "ORDER-002", UserId = userId, TotalAmount = 500, Status = OrderStatus.Shipped, CreatedAt = DateTime.UtcNow }
                }.AsQueryable());

            _mockUserDb.Setup(x => x.CancelOrder(It.IsAny<int>()))
                .Returns(true);

            _mockItemRepo.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns<int>(id => _testItems.FirstOrDefault(i => i.Id == id));

            _mockItemRepo.Setup(x => x.GetAll())
                .Returns(_testItems);

            _mockItemRepo.Setup(x => x.GetByCategory(It.IsAny<int>()))
                .Returns<int>(categoryId => _testItems.Where(i => i.CategoryId == categoryId).ToList());

            _mockItemRepo.Setup(x => x.GetByBrand(It.IsAny<int>()))
                .Returns<int>(brandId => _testItems.Where(i => i.BrandId == brandId).ToList());

            _mockItemRepo.Setup(x => x.Search(It.IsAny<string>()))
                .Returns<string>(query => _testItems.Where(i => i.Name.Contains(query)).ToList());

            _mockItemRepo.Setup(x => x.GetCategoryById(It.IsAny<int>()))
                .Returns<int>(id => _testCategories.FirstOrDefault(c => c.Id == id));

            _mockItemRepo.Setup(x => x.Create(It.IsAny<Item>()))
                .Returns(true);

            _mockItemRepo.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Item>()))
                .Returns<int, Item>((id, item) => item);

            _mockItemRepo.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns(true);

            _mockItemRepo.Setup(x => x.AddReview(It.IsAny<int>(), It.IsAny<Review>()))
                .Returns(true);

            _mockItemRepo.Setup(x => x.RemoveReview(It.IsAny<int>()))
                .Returns(true);

            _mockLogDb.Setup(x => x.LogPageVisitAsync(
                    It.IsAny<int>(),
                    It.IsAny<UserAction>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>(),
                    It.IsAny<SearchFilters>()))
                .Returns(Task.CompletedTask);

            var currentUser = _testUsers[0];
            _mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(currentUser);
        }

        private List<Category> GetTestCategories()
        {
            return new List<Category>
            {
                new Category { Id = 1, Name = "Electronics", ParentCategoryId = null, DisplayOrder = 1 },
                new Category { Id = 2, Name = "Laptops", ParentCategoryId = 1, DisplayOrder = 1 },
                new Category { Id = 3, Name = "Smartphones", ParentCategoryId = 1, DisplayOrder = 2 }
            };
        }

        private List<Item> GetTestItems()
        {
            return new List<Item>
            {
                new Item { Id = 1, Name = "MacBook Pro", Desc = "Powerful laptop", Price = 2000, CategoryId = 2, BrandId = 1, UserId = 1, IsAvailable = true, Garantia = 12, ImageUrl = "/images/test.jpg" },
                new Item { Id = 2, Name = "Dell XPS", Desc = "High-end laptop", Price = 1500, CategoryId = 2, BrandId = 1, UserId = 1, IsAvailable = true, Garantia = 24 },
                new Item { Id = 3, Name = "iPhone 13", Desc = "Latest iPhone", Price = 800, CategoryId = 3, BrandId = 1, UserId = 1, IsAvailable = false, Garantia = 12 }
            };
        }

        private List<CartItem> GetTestCartItems()
        {
            return new List<CartItem>
            {
                new CartItem { Id = 1, UserId = 1, ItemId = 1, Quantity = 1 },
                new CartItem { Id = 2, UserId = 1, ItemId = 2, Quantity = 2 }
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
                new User { Id = 1, Name = "Test User", Email = "test@example.com", UserName = "test@example.com", IsAdmin = false },
                new User { Id = 2, Name = "Admin User", Email = "admin@example.com", UserName = "admin@example.com", IsAdmin = true }
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
                ControllerName = "Site",
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
        public void Index_ReturnsViewResult_WithLayoutData()
        {
            SetupAuthenticatedUser();
            var result = _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.ViewData["Categories"]);
        }

        [Fact]
        public void Browse_ReturnsViewResult_WithItems()
        {
            SetupAuthenticatedUser();
            var result = _controller.Browse(null, null, 1, "default", null, null, null);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Item>>(viewResult.Model);
            Assert.NotNull(model);
        }

        [Fact]
        public async Task Browse_WithQuery_FiltersItems()
        {
            SetupAuthenticatedUser();

            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == 2);
            if (category == null)
            {
                category = new Category { Id = 2, Name = "Laptops", ParentCategoryId = 1, DisplayOrder = 1 };
                _dbContext.Categories.Add(category);
            }

            var brand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.Id == 1);
            if (brand == null)
            {
                brand = new Brand { Id = 1, Name = "Test Brand" };
                _dbContext.Brands.Add(brand);
            }

            await _dbContext.SaveChangesAsync();

            var testItem = new Item
            {
                Id = 100,
                Name = "UniqueTestMacBook",
                Desc = "Test Description",
                Price = 1000,
                CategoryId = 2,
                BrandId = 1,
                UserId = 1,
                IsAvailable = true,
                Garantia = 12
            };
            _dbContext.Items.Add(testItem);
            await _dbContext.SaveChangesAsync();

            var result = _controller.Browse("UniqueTestMacBook", null, 1, "default", null, null, null);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Item>>(viewResult.Model);
            Assert.Single(model);
            Assert.Contains(model, i => i.Name.Contains("UniqueTestMacBook"));
        }

        [Fact]
        public async Task Browse_WithCategoryIds_FiltersByCategory()
        {
            SetupAuthenticatedUser();

            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == 2);
            if (category == null)
            {
                category = new Category { Id = 2, Name = "Laptops", ParentCategoryId = 1, DisplayOrder = 1 };
                _dbContext.Categories.Add(category);
            }

            var brand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.Id == 1);
            if (brand == null)
            {
                brand = new Brand { Id = 1, Name = "Test Brand" };
                _dbContext.Brands.Add(brand);
            }

            await _dbContext.SaveChangesAsync();

            var existingItems = _dbContext.Items.Where(i => i.CategoryId == 2).ToList();
            _dbContext.Items.RemoveRange(existingItems);

            var testItem1 = new Item
            {
                Id = 101,
                Name = "Test Laptop 1",
                Desc = "Test",
                Price = 1000,
                CategoryId = 2,
                BrandId = 1,
                UserId = 1,
                IsAvailable = true,
                Garantia = 12
            };
            var testItem2 = new Item
            {
                Id = 102,
                Name = "Test Laptop 2",
                Desc = "Test",
                Price = 1500,
                CategoryId = 2,
                BrandId = 1,
                UserId = 1,
                IsAvailable = true,
                Garantia = 12
            };
            _dbContext.Items.AddRange(testItem1, testItem2);
            await _dbContext.SaveChangesAsync();

            var result = _controller.Browse(null, new List<int> { 2 }, 1, "default", null, null, null);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Item>>(viewResult.Model);
            Assert.Equal(2, model.Count);
            Assert.All(model, i => Assert.Equal(2, i.CategoryId));
        }

        [Fact]
        public void Cart_WhenAuthenticated_ReturnsViewWithCartItems()
        {
            SetupAuthenticatedUser(1);
            var result = _controller.Cart();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Item>>(viewResult.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void Cart_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();
            var result = _controller.Cart();
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public void AddToCart_WhenAuthenticated_AddsItemToCart()
        {
            SetupAuthenticatedUser(1);
            var result = _controller.AddToCart(3, 1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(SiteController.Cart), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.AddToCart(1, 3), Times.Once);
        }

        [Fact]
        public void AddToCart_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();
            var result = _controller.AddToCart(1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public void RemoveFromCart_WhenAuthenticated_RemovesItem()
        {
            SetupAuthenticatedUser(1);
            var result = _controller.RemoveFromCart(1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(SiteController.Cart), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.RemoveFromCart(1, 1), Times.Once);
        }

        [Fact]
        public void ClearCart_WhenAuthenticated_ClearsCart()
        {
            SetupAuthenticatedUser(1);
            var result = _controller.ClearCart();
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(SiteController.Cart), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.ClearCart(1), Times.Once);
        }

        [Fact]
        public void Wishlist_WhenAuthenticated_RedirectsToWishlistController()
        {
            SetupAuthenticatedUser(1);
            var result = _controller.Wishlist();
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Wishlist", redirectResult.ControllerName);
        }

        [Fact]
        public void Wishlist_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();
            var result = _controller.Wishlist();
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public void AddToWishlist_WhenAuthenticated_AddsItem()
        {
            SetupAuthenticatedUser(1);
            var result = _controller.AddToWishlist(3);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(SiteController.Wishlist), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.AddToWishList(1, 3), Times.Once);
        }

        [Fact]
        public void AddToWishlist_WhenAlreadyInWishlist_RemovesItem()
        {
            SetupAuthenticatedUser(1);
            var result = _controller.AddToWishlist(1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(SiteController.Wishlist), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.RemoveFromWishList(1, 1), Times.Once);
        }

        [Fact]
        public void RemoveFromWishlist_WhenAuthenticated_RemovesItem()
        {
            SetupAuthenticatedUser(1);
            var result = _controller.RemoveFromWishlist(1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(SiteController.Wishlist), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.RemoveFromWishList(1, 1), Times.Once);
        }

        [Fact]
        public void Orders_WhenAuthenticated_ReturnsOrders()
        {
            SetupAuthenticatedUser(1);
            var result = _controller.Orders();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Order>>(viewResult.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void AccessDenied_ReturnsView()
        {
            var result = _controller.AccessDenied();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Error_ReturnsView()
        {
            var result = _controller.Error();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void GetAllItems_ReturnsAllItems()
        {
            SetupAuthenticatedUser();
            var result = _controller.GetAllItems();
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void GetItemById_WithValidId_ReturnsItem()
        {
            SetupAuthenticatedUser();
            var result = _controller.GetItemById(1);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void SearchItems_ReturnsMatchingItems()
        {
            SetupAuthenticatedUser();
            var result = _controller.SearchItems("Mac");
            Assert.Single(result);
        }

        [Fact]
        public void ValidatePromo_WithValidCode_ReturnsSuccess()
        {
            SetupAuthenticatedUser();
            var result = _controller.ValidatePromo("BAZAR10");
            var jsonResult = Assert.IsType<JsonResult>(result);
            var data = jsonResult.Value;
            var validProperty = data.GetType().GetProperty("valid");
            Assert.NotNull(validProperty);
            Assert.True((bool)validProperty.GetValue(data));
        }

        [Fact]
        public void ValidatePromo_WithInvalidCode_ReturnsFailure()
        {
            SetupAuthenticatedUser();
            var result = _controller.ValidatePromo("INVALID");
            var jsonResult = Assert.IsType<JsonResult>(result);
            var data = jsonResult.Value;
            var validProperty = data.GetType().GetProperty("valid");
            Assert.NotNull(validProperty);
            Assert.False((bool)validProperty.GetValue(data));
        }
    }
}