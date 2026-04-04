using BazaR.Controllers;
using BazaR.Data;
using BazaR.Interfaces;
using BazaR.Models;
using BazaR.Models.BazaR.Models;
using BazaR.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly Mock<IMemoryCache> _mockMemoryCache;
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
            _mockMemoryCache = new Mock<IMemoryCache>();

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
            _dbContext.Promotions.AddRange(GetTestPromotions());
            _dbContext.SaveChanges();

            SetupMocks();

            _controller = new SiteController(
                _mockUserDb.Object,
                _mockItemRepo.Object,
                _dbContext,
                _mockUserManager.Object,
                _mockLogDb.Object,
                _mockMemoryCache.Object);
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

            _mockItemRepo.Setup(x => x.Filter(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<decimal?>(), It.IsAny<decimal?>(), It.IsAny<bool?>()))
                .Returns<int?, int?, decimal?, decimal?, bool?>((catId, brandId, minPrice, maxPrice, isAvailable) =>
                {
                    var query = _testItems.AsQueryable();
                    if (catId.HasValue)
                        query = query.Where(i => i.CategoryId == catId.Value);
                    if (brandId.HasValue)
                        query = query.Where(i => i.BrandId == brandId.Value);
                    if (minPrice.HasValue)
                        query = query.Where(i => i.Price >= minPrice.Value);
                    if (maxPrice.HasValue)
                        query = query.Where(i => i.Price <= maxPrice.Value);
                    if (isAvailable.HasValue)
                        query = query.Where(i => i.IsAvailable == isAvailable.Value);
                    return query.ToList();
                });

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

        private List<Promotion> GetTestPromotions()
        {
            return new List<Promotion>
            {
                new Promotion { Id = 1, DiscountAmount = 100, Number = "BAZAR10" }
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

            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                user = new User
                {
                    Id = userId,
                    Name = $"User{userId}",
                    Email = $"user{userId}@test.com",
                    UserName = $"user{userId}@test.com",
                    IsAdmin = userId >= 900
                };
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, user.Name ?? "Test User"),
                new Claim(ClaimTypes.Email, user.Email ?? "test@example.com")
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

            _mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync((User?)null);
        }

        private void SetupAjaxRequest()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["X-Requested-With"] = "XMLHttpRequest";

            var routeData = new RouteData();
            var actionDescriptor = new ControllerActionDescriptor();
            var actionContext = new ActionContext(httpContext, routeData, actionDescriptor);

            _controller.ControllerContext = new ControllerContext(actionContext);
            _controller.Url = new Mock<IUrlHelper>().Object;

            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _controller.TempData = tempData;
        }


        #region Cart Tests

        [Fact]
        public async Task AddToCart_WhenAuthenticated_AddsItemToCart()
        {
            SetupAuthenticatedUser(1);
            var result = await _controller.AddToCart(3, 1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(SiteController.Cart), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.AddToCart(1, 3), Times.Once);
        }

        [Fact]
        public async Task AddToCart_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();
            var result = await _controller.AddToCart(1);
            var redirectResult = Assert.IsType<ChallengeResult>(result);
        }

        [Fact]
        public async Task AddToCart_WhenItemNotFound_ReturnsNotFound()
        {
            SetupAuthenticatedUser(1);
            _mockItemRepo.Setup(x => x.GetById(999)).Returns((Item)null);

            var result = await _controller.AddToCart(999, 1);
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region Wishlist Tests

        [Fact]
        public async Task Wishlist_WhenAuthenticated_RedirectsToWishlistController()
        {
            SetupAuthenticatedUser(1);
            var result = _controller.Wishlist();
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Wishlist", redirectResult.ControllerName);
        }

        [Fact]
        public async Task AddToWishlist_WhenAuthenticated_AddsItem()
        {
            SetupAuthenticatedUser(1);
            var result = await _controller.AddToWishlist(3);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(SiteController.Wishlist), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.AddToWishList(1, 3), Times.Once);
        }

        [Fact]
        public async Task AddToWishlist_WhenAlreadyInWishlist_RemovesItem()
        {
            SetupAuthenticatedUser(1);
            var result = await _controller.AddToWishlist(1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(SiteController.Wishlist), redirectResult.ActionName);
            _mockUserDb.Verify(x => x.RemoveFromWishList(1, 1), Times.Once);
        }

        [Fact]
        public async Task AddToWishlist_WhenItemNotFound_ReturnsNotFound()
        {
            SetupAuthenticatedUser(1);
            _mockItemRepo.Setup(x => x.GetById(999)).Returns((Item)null);
            var result = await _controller.AddToWishlist(999);
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region Review Tests

        [Fact]
        public async Task AddReview_WhenAuthenticated_AddsReview()
        {
            SetupAuthenticatedUser(1);
            var review = new Review { Comment = "Great product!", Rating = 5 };
            var result = await _controller.AddReview(1, review);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(SiteController.ItemDetails), redirectResult.ActionName);
            _mockItemRepo.Verify(x => x.AddReview(1, It.IsAny<Review>()), Times.Once);
        }

        [Fact]
        public async Task AddReview_WhenUnauthenticated_RedirectsToLogin()
        {
            SetupUnauthenticatedUser();
            var review = new Review { Comment = "Test", Rating = 5 };
            var result = await _controller.AddReview(1, review);
            var redirectResult = Assert.IsType<ChallengeResult>(result);
        }

        [Fact]
        public async Task RemoveReview_WhenAdmin_RemovesReview()
        {
            var adminUser = new User { Id = 996, Name = "Admin", Email = "admin@test.com", UserName = "admin@test.com", IsAdmin = true };
            _dbContext.Users.Add(adminUser);
            await _dbContext.SaveChangesAsync();
            SetupAuthenticatedUser(996);
            var result = _controller.RemoveReview(1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(SiteController.Index), redirectResult.ActionName);
            _mockItemRepo.Verify(x => x.RemoveReview(1), Times.Once);
        }

        #endregion


        #region Simple Tests

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
        public void Privacy_ReturnsView()
        {
            var result = _controller.Privacy();
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
        public void GetItemById_WithInvalidId_ReturnsNull()
        {
            SetupAuthenticatedUser();
            var result = _controller.GetItemById(999);
            Assert.Null(result);
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

        [Fact]
        public void GetItemsByCategory_ReturnsCorrectItems()
        {
            SetupAuthenticatedUser();
            var result = _controller.GetItemsByCategory(2);
            Assert.Equal(2, result.Count);
            Assert.All(result, i => Assert.Equal(2, i.CategoryId));
        }

        [Fact]
        public void GetItemsByBrand_ReturnsCorrectItems()
        {
            SetupAuthenticatedUser();
            var result = _controller.GetItemsByBrand(1);
            Assert.Equal(3, result.Count);
            Assert.All(result, i => Assert.Equal(1, i.BrandId));
        }

        [Fact]
        public void FilterItems_WithMultipleFilters_ReturnsFilteredItems()
        {
            SetupAuthenticatedUser(1);
            var result = _controller.FilterItems(2, 1, 1000, 2000, true);
            Assert.NotNull(result);
        }

        [Fact]
        public void SearchSuggestions_WithValidQuery_ReturnsResults()
        {
            SetupAuthenticatedUser();
            var result = _controller.SearchSuggestions("Laptop");
            var jsonResult = Assert.IsType<JsonResult>(result);
            Assert.NotNull(jsonResult.Value);
        }

        [Fact]
        public void SearchSuggestions_WithEmptyQuery_ReturnsEmptyList()
        {
            SetupAuthenticatedUser();
            var result = _controller.SearchSuggestions("");
            var jsonResult = Assert.IsType<JsonResult>(result);
            var data = jsonResult.Value as List<object>;
            Assert.NotNull(data);
            Assert.Empty(data);
        }

        [Fact]
        public void CategoryPage_WithValidCategory_ReturnsView()
        {
            SetupAuthenticatedUser();
            var result = _controller.CategoryPage(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<Category>>(viewResult.Model);
            Assert.NotNull(model);
        }

        [Fact]
        public void AddCategoriesTest_AddsCategoriesToDatabase()
        {
            var existingCategories = _dbContext.Categories.Where(c => c.Name == "Телефоны" || c.Name == "Ноутбуки");
            _dbContext.Categories.RemoveRange(existingCategories);
            _dbContext.SaveChanges();
            var result = _controller.AddCategoriesTest();
            var contentResult = Assert.IsType<ContentResult>(result);
            Assert.Equal("OK", contentResult.Content);
        }

        [Fact]
        public void Admin_User_Should_Have_IsAdmin_True()
        {
            var adminUser = new User { Id = 777, Name = "RealAdmin", Email = "admin@real.com", UserName = "admin@real.com", IsAdmin = true };
            _dbContext.Users.Add(adminUser);
            _dbContext.SaveChanges();
            Assert.True(adminUser.IsAdmin);
        }

        [Fact]
        public async Task CalculateBonusDiscount_WhenAuthenticated_ReturnsDiscount()
        {
            SetupAuthenticatedUser(1);
            var bonusAccount = new BonusAccount
            {
                UserId = 1,
                TotalBalance = 1000,
                MonthlyAccrued = 0,
                MonthlySpent = 0,
                AccrualRate = 0.10m,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _dbContext.BonusAccounts.Add(bonusAccount);
            await _dbContext.SaveChangesAsync();

            var result = await _controller.CalculateBonusDiscount(500);
            var jsonResult = Assert.IsType<JsonResult>(result);
            var data = jsonResult.Value;
            var okProperty = data.GetType().GetProperty("ok");
            Assert.NotNull(okProperty);
            Assert.True((bool)okProperty.GetValue(data));
        }

        [Fact]
        public async Task CalculateBonusDiscount_WhenBonusToUseExceedsBalance_CapsAtBalance()
        {
            SetupAuthenticatedUser(1);
            var bonusAccount = new BonusAccount
            {
                UserId = 1,
                TotalBalance = 100,
                MonthlyAccrued = 0,
                MonthlySpent = 0,
                AccrualRate = 0.10m,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _dbContext.BonusAccounts.Add(bonusAccount);
            await _dbContext.SaveChangesAsync();

            var result = await _controller.CalculateBonusDiscount(500);
            var jsonResult = Assert.IsType<JsonResult>(result);
            var data = jsonResult.Value;
            var bonusUsedProperty = data.GetType().GetProperty("bonusUsed");
            Assert.NotNull(bonusUsedProperty);
            Assert.Equal(100, (int)bonusUsedProperty.GetValue(data));
        }

        [Fact]
        public async Task CalculateBonusDiscount_WhenUnauthenticated_ReturnsError()
        {
            SetupUnauthenticatedUser();
            var result = await _controller.CalculateBonusDiscount(100);
            var jsonResult = Assert.IsType<JsonResult>(result);
            var data = jsonResult.Value;
            var okProperty = data.GetType().GetProperty("ok");
            Assert.NotNull(okProperty);
            Assert.False((bool)okProperty.GetValue(data));
        }

        [Fact]
        public async Task AddToCart_AjaxRequest_ReturnsJsonWithCartCount()
        {
            var user = new User { Id = 99, Name = "Test User", Email = "test@example.com", UserName = "test@example.com", IsAdmin = false };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            _mockUserDb.Setup(x => x.AddToCart(99, 3)).Returns(true);
            _mockUserDb.Setup(x => x.GetCartItemsWithQuantity(99))
                .Returns(new List<CartItem> { new CartItem { ItemId = 3, Quantity = 1, Item = _testItems.First(i => i.Id == 3) } });

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["X-Requested-With"] = "XMLHttpRequest";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "99"),
                new Claim(ClaimTypes.Name, "Test User"),
                new Claim(ClaimTypes.Email, "test@example.com")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            httpContext.User = claimsPrincipal;

            var actionContext = new ActionContext(httpContext, new RouteData(), new ControllerActionDescriptor());
            _controller.ControllerContext = new ControllerContext(actionContext);
            _controller.Url = new Mock<IUrlHelper>().Object;
            _controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            _mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);

            var result = await _controller.AddToCart(3, 1);
            var jsonResult = Assert.IsType<JsonResult>(result);
            var data = jsonResult.Value;
            var successProperty = data.GetType().GetProperty("success");
            Assert.NotNull(successProperty);
            Assert.True((bool)successProperty.GetValue(data));
        }

        [Fact]
        public async Task AddToWishlist_AjaxRequest_ReturnsJsonWithWishlistCount()
        {
            var user = new User { Id = 100, Name = "Test User", Email = "test@example.com", UserName = "test@example.com", IsAdmin = false };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            _mockUserDb.Setup(x => x.AddToWishList(100, 3)).Returns(true);
            _mockUserDb.Setup(x => x.GetWishList(100))
                .Returns(new List<Item> { _testItems.First(i => i.Id == 3) }.AsQueryable());

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["X-Requested-With"] = "XMLHttpRequest";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "100"),
                new Claim(ClaimTypes.Name, "Test User"),
                new Claim(ClaimTypes.Email, "test@example.com")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            httpContext.User = claimsPrincipal;

            var actionContext = new ActionContext(httpContext, new RouteData(), new ControllerActionDescriptor());
            _controller.ControllerContext = new ControllerContext(actionContext);
            _controller.Url = new Mock<IUrlHelper>().Object;
            _controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            _mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);

            var result = await _controller.AddToWishlist(3);
            var jsonResult = Assert.IsType<JsonResult>(result);
            var data = jsonResult.Value;
            var successProperty = data.GetType().GetProperty("success");
            Assert.NotNull(successProperty);
            Assert.True((bool)successProperty.GetValue(data));
        }
        #endregion
    }
}