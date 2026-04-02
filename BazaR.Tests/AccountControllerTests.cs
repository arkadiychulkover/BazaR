// ==================== BazaR.Tests/Controllers/AccountControllerTests.cs ====================
using BazaR.Controllers;
using BazaR.Data;
using BazaR.Models;
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
using Moq;
using System.Security.Claims;
using Xunit;

namespace BazaR.Tests.Controllers
{
    public class AccountControllerTests
    {
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<SignInManager<User>> _mockSignInManager;
        private readonly AppDbContext _dbContext;
        private readonly AccountController _controller;
        private readonly List<User> _testUsers;

        public AccountControllerTests()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            _mockSignInManager = new Mock<SignInManager<User>>(
                _mockUserManager.Object,
                contextAccessor.Object,
                userPrincipalFactory.Object,
                null, null, null, null);

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _dbContext = new AppDbContext(options);

            _testUsers = GetTestUsers();
            SetupMocks();

            _controller = new AccountController(
                _mockUserManager.Object,
                _mockSignInManager.Object, 
                _dbContext);
        }

        private void SetupMocks()
        {
            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string email) => _testUsers.FirstOrDefault(u => u.Email == email));

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _mockUserManager.Setup(x => x.DeleteAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);

            _mockSignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            _mockSignInManager.Setup(x => x.SignInAsync(It.IsAny<User>(), It.IsAny<bool>(), null))
                .Returns(Task.CompletedTask);

            _mockSignInManager.Setup(x => x.SignOutAsync())
                .Returns(Task.CompletedTask);
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
                ControllerName = "Account",
                ActionName = "Login"
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

        [Fact]
        public async Task Login_Get_ReturnsViewWithExternalLogins()
        {
            var result = await _controller.Login();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Login_Post_WithValidCredentials_RedirectsToReturnUrl()
        {
            SetupControllerContext();
            var result = await _controller.Login("test@example.com", "Password123!", "/");
            Assert.IsType<RedirectResult>(result);
        }

        [Fact]
        public async Task Login_Post_WithEmptyFields_RedirectsWithErrors()
        {
            SetupControllerContext();
            var result = await _controller.Login("", "");
            Assert.IsType<RedirectResult>(result);
        }

        [Fact]
        public async Task Logout_SignsOutAndRedirects()
        {
            SetupAuthenticatedUser();
            var result = await _controller.Logout();
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Register_WithValidData_CreatesUserAndSignsIn()
        {
            SetupControllerContext();
            var result = await _controller.Register("newuser@example.com", "New", "User", "Password123!", "1234567890");
            Assert.IsType<RedirectResult>(result);
        }

        [Fact]
        public void ForgotPassword_Get_ReturnsView()
        {
            var result = _controller.ForgotPassword();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ResetPassword_Get_ReturnsViewWithModel()
        {
            var result = _controller.ResetPassword("test-token", "test@example.com");
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ResetPasswordViewModel>(viewResult.Model);
            Assert.Equal("test-token", model.Token);
            Assert.Equal("test@example.com", model.Email);
        }
    }
}