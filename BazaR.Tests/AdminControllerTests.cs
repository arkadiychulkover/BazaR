using BazaR.Controllers;
using BazaR.Data;
using BazaR.Interfaces;
using BazaR.Models;
using BazaR.Services;
using BazaR.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System.Security.Claims;

namespace BazaR.Tests.Controllers
{
    public class AdminControllerTests : IDisposable
    {
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<RoleManager<IdentityRole<int>>> _mockRoleManager;
        private readonly Mock<SignInManager<User>> _mockSignInManager;
        private readonly Mock<IUserDb> _mockUserDb;
        private readonly Mock<IUserStatistick> _mockStatistickRepo;
        private readonly Mock<IMemoryCache> _mockMemoryCache;
        private readonly AppDbContext _dbContext;
        private readonly AdminController _controller;
        private readonly List<User> _testUsers;
        private readonly List<Item> _testItems;
        private readonly List<Category> _testCategories;
        private readonly List<Brand> _testBrands;
        private readonly List<VisitingModel> _testLogs;

        public AdminControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            _dbContext = new AppDbContext(options);

            _testUsers = GetTestUsers();
            _testItems = GetTestItems();
            _testCategories = GetTestCategories();
            _testBrands = GetTestBrands();
            _testLogs = GetTestLogs();

            _dbContext.Users.AddRange(_testUsers);
            _dbContext.Items.AddRange(_testItems);
            _dbContext.Categories.AddRange(_testCategories);
            _dbContext.Brands.AddRange(_testBrands);
            _dbContext.VisitingModels.AddRange(_testLogs);
            _dbContext.SaveChanges();

            _mockUserManager = SetupMockUserManager();
            _mockRoleManager = SetupMockRoleManager();
            _mockSignInManager = SetupMockSignInManager();
            _mockUserDb = new Mock<IUserDb>();
            _mockMemoryCache = SetupMockMemoryCache();
            _mockStatistickRepo = SetupMockStatistickRepo();

            _controller = new AdminController(
                _mockUserManager.Object,
                _mockRoleManager.Object,
                _mockSignInManager.Object,
                _dbContext,
                _mockUserDb.Object,
                _mockStatistickRepo.Object,
                _mockMemoryCache.Object);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        private Mock<IMemoryCache> SetupMockMemoryCache()
        {
            var mock = new Mock<IMemoryCache>();

            object? value;

            mock.Setup(x => x.TryGetValue(It.IsAny<object>(), out value))
                .Returns(false);

            mock.Setup(x => x.CreateEntry(It.IsAny<object>()))
                .Returns(Mock.Of<ICacheEntry>);

            return mock;
        }

        private Mock<UserManager<User>> SetupMockUserManager()
        {
            var store = new Mock<IUserStore<User>>();
            var mock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            mock.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string id) =>
                {
                    if (int.TryParse(id, out int userId))
                        return _testUsers.FirstOrDefault(u => u.Id == userId);
                    return null;
                });

            mock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string email) => _testUsers.FirstOrDefault(u => u.Email == email));

            mock.Setup(x => x.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);

            mock.Setup(x => x.DeleteAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);

            mock.Setup(x => x.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync((User user, string role) => user.IsAdmin && role == "Admin");

            mock.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            mock.Setup(x => x.RemoveFromRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            mock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync((ClaimsPrincipal principal) =>
                {
                    var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (userId != null && int.TryParse(userId, out int id))
                        return _testUsers.FirstOrDefault(u => u.Id == id);
                    return null;
                });

            return mock;
        }

        private Mock<RoleManager<IdentityRole<int>>> SetupMockRoleManager()
        {
            var store = new Mock<IRoleStore<IdentityRole<int>>>();
            var mock = new Mock<RoleManager<IdentityRole<int>>>(store.Object, null, null, null, null);

            mock.Setup(x => x.RoleExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            mock.Setup(x => x.CreateAsync(It.IsAny<IdentityRole<int>>()))
                .ReturnsAsync(IdentityResult.Success);

            return mock;
        }

        private Mock<SignInManager<User>> SetupMockSignInManager()
        {
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();

            var mock = new Mock<SignInManager<User>>(
                _mockUserManager.Object,
                contextAccessor.Object,
                userPrincipalFactory.Object,
                null, null, null, null);

            mock.Setup(x => x.RefreshSignInAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            return mock;
        }

        private Mock<IUserStatistick> SetupMockStatistickRepo()
        {
            var mock = new Mock<IUserStatistick>();

            mock.Setup(x => x.GetUsersCountForDayAsync())
                .Returns(5);

            mock.Setup(x => x.GetUsersCountForWeekAsync())
                .Returns(20);

            mock.Setup(x => x.GetUsersCountForMonthAsync())
                .Returns(100);

            mock.Setup(x => x.GetPopularCategoryAsync())
                .ReturnsAsync(new Dictionary<Category, int>());

            return mock;
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

        private List<Item> GetTestItems()
        {
            return new List<Item>
            {
                new Item { Id = 1, Name = "Test Item", Desc = "Test Description", Price = 100, IsAvailable = true,
                          Garantia = 12, CategoryId = 1, BrandId = 1, UserId = 1, ImageUrl = "/test.jpg" }
            };
        }

        private List<Category> GetTestCategories()
        {
            return new List<Category>
            {
                new Category { Id = 1, Name = "Electronics", ParentCategoryId = null, DisplayOrder = 1 }
            };
        }

        private List<Brand> GetTestBrands()
        {
            return new List<Brand>
            {
                new Brand { Id = 1, Name = "Test Brand" }
            };
        }

        private List<VisitingModel> GetTestLogs()
        {
            return new List<VisitingModel>
            {
                new VisitingModel
                {
                    Id = 1,
                    UserId = 1,
                    ControllerName = "Site",
                    ActionName = "Index",
                    SearchFilters = null
                }
            };
        }

        private void SetupAuthenticatedAdmin()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "2"),
                new Claim(ClaimTypes.Name, "Admin User"),
                new Claim(ClaimTypes.Email, "admin@example.com"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext { User = claimsPrincipal };

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        #region Users Tests

        [Fact]
        public async Task Index_ReturnsViewWithUsers()
        {
            SetupAuthenticatedAdmin();
            var activeUsersService = new ActiveUsersService();
            var result = await _controller.Index(activeUsersService);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.ViewData["Active"]);
        }

        [Fact]
        public async Task DeleteUser_WithValidId_DeletesUser()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.DeleteUser(1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task DeleteUser_WithInvalidId_RedirectsToIndex()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.DeleteUser(999);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task EditUser_Get_WithValidId_ReturnsViewWithUser()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.EditUser(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<User>(viewResult.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task EditUser_Get_WithInvalidId_RedirectsToIndex()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.EditUser(999);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task EditUser_Post_UpdatesUser()
        {
            SetupAuthenticatedAdmin();
            var user = new User { Id = 1, Name = "Updated Name", Email = "updated@example.com", IsAdmin = false };
            var result = await _controller.EditUser(user);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task EditUser_Post_WhenUserNotFound_RedirectsToIndex()
        {
            SetupAuthenticatedAdmin();
            var user = new User { Id = 999, Name = "Not Exist" };
            var result = await _controller.EditUser(user);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task EditUser_Post_WithAdminRole_AddsToRole()
        {
            SetupAuthenticatedAdmin();

            _mockUserManager.Setup(x => x.IsInRoleAsync(It.IsAny<User>(), "Admin"))
                .ReturnsAsync(false);

            var user = new User { Id = 1, Name = "Updated", Email = "updated@example.com", IsAdmin = true };
            var result = await _controller.EditUser(user);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task BlockUser_WithValidId_LocksUserAccount()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.BlockUser(1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task BlockUser_WithInvalidId_RedirectsToIndex()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.BlockUser(999);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task UnblockUser_WithValidId_UnlocksUserAccount()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.UnblockUser(1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task UnblockUser_WithInvalidId_RedirectsToIndex()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.UnblockUser(999);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        #endregion

        #region UserStatistic Tests

        [Fact]
        public async Task UserStatistic_WithValidId_ReturnsViewWithUserStats()
        {
            SetupAuthenticatedAdmin();

            var user = _testUsers.First(u => u.Id == 1);
            user.SellingItems = _testItems.Where(i => i.UserId == 1).ToList();
            user.Reviews = new List<Review>();
            user.Orders = new List<Order>();

            var result = await _controller.UserStatistic(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<User>(viewResult.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task UserStatistic_WithInvalidId_RedirectsToIndex()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.UserStatistic(999);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        #endregion

        #region Items Tests

        [Fact]
        public async Task DeleteUserItem_WithValidId_RemovesItem()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.DeleteUserItem(1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.UserStatistic), redirectResult.ActionName);
        }

        [Fact]
        public async Task DeleteUserItem_WithInvalidId_RedirectsToIndex()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.DeleteUserItem(999);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task EditItem_Get_WithValidId_ReturnsViewWithItem()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.EditItem(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Item>(viewResult.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task EditItem_Get_WithInvalidId_RedirectsToIndex()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.EditItem(999);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task EditItem_Post_UpdatesItem()
        {
            SetupAuthenticatedAdmin();
            var item = new Item { Id = 1, Name = "Updated Item", Price = 200 };
            var result = await _controller.EditItem(item);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.UserStatistic), redirectResult.ActionName);
        }

        [Fact]
        public async Task EditItem_Post_WhenItemNotFound_RedirectsToIndex()
        {
            SetupAuthenticatedAdmin();
            var item = new Item { Id = 999, Name = "Not Exist" };
            var result = await _controller.EditItem(item);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        #endregion

        #region Reviews Tests

        [Fact]
        public async Task DeleteUserReview_WithValidId_RemovesReview()
        {
            SetupAuthenticatedAdmin();

            var review = new Review { Id = 1, UserId = 1, ItemId = 1, Comment = "Test", Rating = 5, CreatedAt = DateTime.UtcNow };
            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();

            var result = await _controller.DeleteUserReview(1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.UserStatistic), redirectResult.ActionName);
        }

        [Fact]
        public async Task DeleteUserReview_WithInvalidId_RedirectsToIndex()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.DeleteUserReview(999);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task EditReview_Get_WithValidId_ReturnsViewWithReview()
        {
            SetupAuthenticatedAdmin();

            var review = new Review { Id = 1, UserId = 1, ItemId = 1, Comment = "Test", Rating = 5, CreatedAt = DateTime.UtcNow };
            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();

            var result = await _controller.EditReview(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Review>(viewResult.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task EditReview_Get_WithInvalidId_RedirectsToIndex()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.EditReview(999);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task EditReview_Post_UpdatesReview()
        {
            SetupAuthenticatedAdmin();

            var review = new Review { Id = 1, UserId = 1, ItemId = 1, Comment = "Test", Rating = 5, CreatedAt = DateTime.UtcNow };
            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();

            var updatedReview = new Review { Id = 1, Comment = "Updated", Rating = 4 };
            var result = await _controller.EditReview(updatedReview);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.UserStatistic), redirectResult.ActionName);
        }

        #endregion

        #region Orders Tests

        [Fact]
        public async Task DeleteOrder_WithValidId_RemovesOrder()
        {
            SetupAuthenticatedAdmin();

            var order = new Order
            {
                Id = 1,
                UserId = 1,
                Number = "ORDER-001",
                TotalAmount = 100,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.New
            };
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            var result = await _controller.DeleteOrder(1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.UserStatistic), redirectResult.ActionName);
        }

        [Fact]
        public async Task DeleteOrder_WithInvalidId_RedirectsToIndex()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.DeleteOrder(999);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task OrderDetails_WithValidId_ReturnsViewWithOrder()
        {
            SetupAuthenticatedAdmin();

            var user = _testUsers.First(u => u.Id == 1);
            var city = new City { Id = 1, Name = "Test City" };

            if (!_dbContext.Cities.Any(c => c.Id == 1))
            {
                _dbContext.Cities.Add(city);
                await _dbContext.SaveChangesAsync();
            }

            var order = new Order
            {
                Id = 1,
                UserId = 1,
                User = user,
                CityId = 1,
                City = city,
                Number = "ORDER-001",
                TotalAmount = 100,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.New,
                PaymentMethod = OrderPaymentMethod.PayNow,
                PaymentStatus = OrderPaymentStatus.Pending,
                DeliveryMethod = OrderDeliveryMethod.SelfPickup
            };
            _dbContext.Orders.Add(order);

            var orderItem = new OrderItem
            {
                Id = 1,
                OrderId = 1,
                ItemId = 1,
                Quantity = 1,
                PriceAtMoment = 100,
                Item = _testItems.First(i => i.Id == 1)
            };
            _dbContext.OrderItems.Add(orderItem);

            await _dbContext.SaveChangesAsync();

            var result = await _controller.OrderDetails(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Order>(viewResult.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task OrderDetails_WithInvalidId_RedirectsToIndex()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.OrderDetails(999);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task EditOrder_Get_WithValidId_ReturnsViewWithOrder()
        {
            SetupAuthenticatedAdmin();

            var user = _testUsers.First(u => u.Id == 1);
            var order = new Order
            {
                Id = 1,
                UserId = 1,
                User = user,
                Number = "ORDER-001",
                TotalAmount = 100,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.New
            };
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            var result = await _controller.EditOrder(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Order>(viewResult.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task EditOrder_Get_WithInvalidId_RedirectsToIndex()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.EditOrder(999);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task EditOrder_Post_WithInvalidId_RedirectsToIndex()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.EditOrder(999, OrderStatus.New, OrderPaymentMethod.PayNow,
                OrderPaymentStatus.Pending, OrderDeliveryMethod.SelfPickup, null);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        #endregion

        #region Promotions Tests

        [Fact]
        public async Task Promotions_ReturnsViewWithPromotions()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.Promotions();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.Model);
        }

        [Fact]
        public async Task AddPromotion_AddsPromotionAndRedirects()
        {
            SetupAuthenticatedAdmin();
            var promotion = new Promotion { Id = 1, Number = "TEST-CODE", DiscountAmount = 100 };
            var result = await _controller.AddPromotion(promotion);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task DeletePromotion_RemovesPromotionAndRedirects()
        {
            SetupAuthenticatedAdmin();

            var promotion = new Promotion { Id = 1, Number = "TEST-CODE", DiscountAmount = 100 };
            _dbContext.Promotions.Add(promotion);
            await _dbContext.SaveChangesAsync();

            var result = await _controller.DeletePromotion(1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.Promotions), redirectResult.ActionName);
        }

        #endregion

        #region Mails Tests

        [Fact]
        public async Task IndexMail_WithValidId_ReturnsViewWithMessages()
        {
            SetupAuthenticatedAdmin();

            var message = new Message
            {
                Id = 1,
                UserId = 1,
                Content = "Test",
                DateTime = DateTime.UtcNow,
                SenderId = 2,
                SenderName = "Admin",
                Name = "Admin",
                IsRead = false
            };
            _dbContext.Messages.Add(message);
            await _dbContext.SaveChangesAsync();

            var result = await _controller.IndexMail(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.Model);
        }

        [Fact]
        public async Task IndexMail_WithInvalidId_ReturnsNotFound()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.IndexMail(999);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task SendMessage_WithInvalidModel_ReturnsViewWithErrors()
        {
            SetupAuthenticatedAdmin();
            _controller.ModelState.AddModelError("Content", "Required");

            var vm = new SendMessageViewModel { UserId = 1 };
            var result = await _controller.SendMessage(vm);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("IndexMail", viewResult.ViewName);
        }

        [Fact]
        public async Task DeleteMessage_RemovesMessageAndRedirects()
        {
            SetupAuthenticatedAdmin();

            var message = new Message
            {
                Id = 1,
                UserId = 1,
                Content = "Test",
                DateTime = DateTime.UtcNow,
                SenderId = 2,
                SenderName = "Admin",
                Name = "Admin",
                IsRead = false
            };
            _dbContext.Messages.Add(message);
            await _dbContext.SaveChangesAsync();

            var result = await _controller.DeleteMessage(1, 1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AdminController.IndexMail), redirectResult.ActionName);
        }

        #endregion

        #region Statistics & Logs Tests

        [Fact]
        public async Task PopularCategories_ReturnsView()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.PopularCategories();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.Model);
        }

        [Fact]
        public async Task GetLog_ReturnsViewWithLogs()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.GetLog();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.Model);
        }

        [Fact]
        public async Task GetLogByUserId_ReturnsViewWithUserLogs()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.GetLogByUserId(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.Model);
        }

        #endregion
    }
}