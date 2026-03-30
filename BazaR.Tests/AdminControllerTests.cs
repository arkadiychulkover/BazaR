// ==================== BazaR.Tests/Controllers/AdminControllerTests.cs ====================
using BazaR.Controllers;
using BazaR.Data;
using BazaR.Interfaces;
using BazaR.Models;
using BazaR.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;
using Xunit;

namespace BazaR.Tests.Controllers
{
    public class AdminControllerTests : IDisposable
    {
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<RoleManager<IdentityRole<int>>> _mockRoleManager;
        private readonly Mock<SignInManager<User>> _mockSignInManager;
        private readonly Mock<IUserDb> _mockUserDb;
        private readonly Mock<IUserStatistick> _mockStatistickRepo;
        private readonly AppDbContext _dbContext;
        private readonly AdminController _controller;
        private readonly List<User> _testUsers;
        private readonly List<Item> _testItems;

        public AdminControllerTests()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            var roleStoreMock = new Mock<IRoleStore<IdentityRole<int>>>();
            _mockRoleManager = new Mock<RoleManager<IdentityRole<int>>>(
                roleStoreMock.Object, null, null, null, null);

            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            _mockSignInManager = new Mock<SignInManager<User>>(
                _mockUserManager.Object,
                contextAccessor.Object,
                userPrincipalFactory.Object,
                null, null, null, null);

            _mockUserDb = new Mock<IUserDb>();
            _mockStatistickRepo = new Mock<IUserStatistick>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            _dbContext = new AppDbContext(options);

            _testUsers = GetTestUsers();
            _testItems = GetTestItems();

            _dbContext.Users.AddRange(_testUsers);
            _dbContext.Items.AddRange(_testItems);
            _dbContext.SaveChanges();

            SetupMocks();

            _controller = new AdminController(
                _mockUserManager.Object,
                _mockRoleManager.Object,
                _mockSignInManager.Object,
                _dbContext,
                _mockUserDb.Object,
                _mockStatistickRepo.Object);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        private void SetupMocks()
        {
            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string id) => _testUsers.FirstOrDefault(u => u.Id.ToString() == id));

            _mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);

            _mockUserManager.Setup(x => x.DeleteAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);

            _mockUserManager.Setup(x => x.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            _mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _mockUserManager.Setup(x => x.RemoveFromRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _mockStatistickRepo.Setup(x => x.GetUsersCountForDayAsync())
                .Returns(5);

            _mockStatistickRepo.Setup(x => x.GetUsersCountForWeekAsync())
                .Returns(20);

            _mockStatistickRepo.Setup(x => x.GetUsersCountForMonthAsync())
                .Returns(100);
        }

        private List<User> GetTestUsers()
        {
            return new List<User>
            {
                new User { Id = 1, Name = "Test User", Email = "test@example.com", UserName = "test@example.com", IsAdmin = false },
                new User { Id = 2, Name = "Admin User", Email = "admin@example.com", UserName = "admin@example.com", IsAdmin = true }
            };
        }

        private List<Item> GetTestItems()
        {
            return new List<Item>
            {
                new Item { Id = 1, Name = "Test Item", Desc = "Test Description", Price = 100, IsAvailable = true, Garantia = 12, CategoryId = 1, BrandId = 1, UserId = 1 }
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

        [Fact]
        public void Index_ReturnsViewWithUsers()
        {
            SetupAuthenticatedAdmin();
            var activeUsersService = new ActiveUsersService();
            var result = _controller.Index(activeUsersService);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task DeleteUser_WithValidId_DeletesUser()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.DeleteUser(1);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task EditUser_Get_ReturnsViewWithUser()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.EditUser(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task EditUser_Post_UpdatesUser()
        {
            SetupAuthenticatedAdmin();
            var user = new User { Id = 1, Name = "Updated Name", Email = "updated@example.com" };
            var result = await _controller.EditUser(user);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task BlockUser_LocksUserAccount()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.BlockUser(1);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task UnblockUser_UnlocksUserAccount()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.UnblockUser(1);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task UserStatistic_ReturnsViewWithUserStats()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.UserStatistic(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task DeleteUserItem_RemovesItem()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.DeleteUserItem(1);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task EditItem_Get_ReturnsViewWithItem()
        {
            SetupAuthenticatedAdmin();

            int itemId = 1;

            // Проверяем и создаем категорию, если её нет
            var category = await _dbContext.Categories.FindAsync(1);
            if (category == null)
            {
                category = new Category
                {
                    Id = 1,
                    Name = "Test Category",
                    DisplayOrder = 1
                };
                _dbContext.Categories.Add(category);
                await _dbContext.SaveChangesAsync();
            }

            // Проверяем и создаем бренд, если его нет
            var brand = await _dbContext.Brands.FindAsync(1);
            if (brand == null)
            {
                brand = new Brand
                {
                    Id = 1,
                    Name = "Test Brand"
                };
                _dbContext.Brands.Add(brand);
                await _dbContext.SaveChangesAsync();
            }

            // Проверяем существование товара
            var existingItem = await _dbContext.Items.FindAsync(itemId);
            if (existingItem == null)
            {
                var newItem = new Item
                {
                    Id = itemId,
                    Name = "Test Item",
                    Desc = "Test Description",
                    Price = 100,
                    IsAvailable = true,
                    Garantia = 12,
                    CategoryId = 1,
                    BrandId = 1,
                    UserId = 1
                };
                _dbContext.Items.Add(newItem);
                await _dbContext.SaveChangesAsync();
            }

            var result = await _controller.EditItem(itemId);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task EditItem_Post_UpdatesItem()
        {
            SetupAuthenticatedAdmin();
            var item = new Item { Id = 1, Name = "Updated Item", Price = 200 };
            var result = await _controller.EditItem(item);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task DeleteUserReview_RemovesReview()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.DeleteUserReview(1);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task DeleteOrder_RemovesOrder()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.DeleteOrder(1);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task PopularCategories_ReturnsView()
        {
            SetupAuthenticatedAdmin();
            _mockStatistickRepo.Setup(x => x.GetPopularCategoryAsync())
                .ReturnsAsync(new Dictionary<Category, int>());

            var result = await _controller.PopularCategories();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task GetLog_ReturnsViewWithLogs()
        {
            SetupAuthenticatedAdmin();
            var result = await _controller.GetLog();
            Assert.IsType<ViewResult>(result);
        }
    }
}