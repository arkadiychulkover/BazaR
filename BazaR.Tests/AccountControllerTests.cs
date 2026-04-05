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
using Microsoft.AspNetCore.Authentication;

namespace BazaR.Tests.Controllers
{
    public class AccountControllerTests : IDisposable
    {
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<SignInManager<User>> _mockSignInManager;
        private readonly AppDbContext _dbContext;
        private readonly AccountController _controller;
        private readonly List<User> _testUsers;

        public AccountControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _dbContext = new AppDbContext(options);

            _testUsers = GetTestUsers();

            _dbContext.Users.AddRange(_testUsers);
            _dbContext.SaveChanges();

            _mockUserManager = SetupMockUserManager();
            _mockSignInManager = SetupMockSignInManager();

            _controller = new AccountController(
                _mockUserManager.Object,
                _mockSignInManager.Object,
                _dbContext);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        private Mock<UserManager<User>> SetupMockUserManager()
        {
            var store = new Mock<IUserStore<User>>();
            var mock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            mock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((string email) => _testUsers.FirstOrDefault(u => u.Email == email));

            mock.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string id) => _testUsers.FirstOrDefault(u => u.Id.ToString() == id));

            mock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            mock.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            mock.Setup(x => x.DeleteAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);

            mock.Setup(x => x.GeneratePasswordResetTokenAsync(It.IsAny<User>()))
                .ReturnsAsync("test-reset-token");

            mock.Setup(x => x.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            mock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync((ClaimsPrincipal principal) =>
                {
                    var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
                    return _testUsers.FirstOrDefault(u => u.Id.ToString() == userId);
                });

            mock.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns((ClaimsPrincipal principal) => principal.FindFirstValue(ClaimTypes.NameIdentifier));

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

            mock.Setup(x => x.PasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            mock.Setup(x => x.SignInAsync(It.IsAny<User>(), It.IsAny<AuthenticationProperties>(),
                    It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            mock.Setup(x => x.SignOutAsync())
                .Returns(Task.CompletedTask);

            mock.Setup(x => x.ExternalLoginSignInAsync(It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            mock.Setup(x => x.GetExternalLoginInfoAsync(It.IsAny<string>()))
                .ReturnsAsync((string userId) => null);

            mock.Setup(x => x.ConfigureExternalAuthenticationProperties(It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new AuthenticationProperties());

            mock.Setup(x => x.GetExternalAuthenticationSchemesAsync())
                .ReturnsAsync(new List<AuthenticationScheme>());

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

        private void SetupControllerContext()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "https";
            httpContext.Request.Host = new HostString("localhost");

            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.Setup(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("/Site/Index");
            urlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns("https://localhost/Account/ResetPassword");

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
                new Claim(ClaimTypes.Email, user?.Email ?? "test@example.com"),
                new Claim(ClaimTypes.GivenName, user?.FirstName ?? ""),
                new Claim(ClaimTypes.Surname, user?.LastName ?? "")
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
        public async Task Login_Get_ReturnsViewWithExternalLogins()
        {
            var result = await _controller.Login();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.Model);
        }

        [Fact]
        public async Task Login_Get_WithReturnUrl_PassesReturnUrlToView()
        {
            var returnUrl = "/test";
            var result = await _controller.Login(returnUrl);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<LoginViewModel>(viewResult.Model);
            Assert.Equal(returnUrl, model.ReturnUrl);
        }

        [Fact]
        public async Task Login_Post_WithValidCredentials_RedirectsToReturnUrl()
        {
            SetupControllerContext();
            var result = await _controller.Login("test@example.com", "Password123!", "/dashboard", true);
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/dashboard", redirectResult.Url);
        }

        [Fact]
        public async Task Login_Post_WithNullReturnUrl_RedirectsToSiteIndex()
        {
            SetupControllerContext();
            var result = await _controller.Login("test@example.com", "Password123!", null, true);
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/Site/Index", redirectResult.Url);
        }

        [Fact]
        public async Task Login_Post_WithEmptyEmail_RedirectsWithErrors()
        {
            SetupControllerContext();
            var result = await _controller.Login("", "password", "/");
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/", redirectResult.Url);
            Assert.NotNull(_controller.TempData["LoginEmailInvalid"]);
        }

        [Fact]
        public async Task Login_Post_WithEmptyPassword_RedirectsWithErrors()
        {
            SetupControllerContext();
            var result = await _controller.Login("test@example.com", "", "/");
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/", redirectResult.Url);
            Assert.NotNull(_controller.TempData["LoginPasswordInvalid"]);
        }

        [Fact]
        public async Task Login_Post_WithNonExistentEmail_RedirectsWithError()
        {
            SetupControllerContext();
            var result = await _controller.Login("nonexistent@example.com", "password", "/");
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/", redirectResult.Url);
            Assert.NotNull(_controller.TempData["LoginEmailError"]);
        }

        [Fact]
        public async Task Login_Post_WhenAlreadyAuthenticated_RedirectsToReturnUrl()
        {
            SetupAuthenticatedUser(1);
            var result = await _controller.Login("test@example.com", "password", "/dashboard");
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/dashboard", redirectResult.Url);
        }

        [Fact]
        public async Task Logout_SignsOutAndRedirectsToIndex()
        {
            SetupAuthenticatedUser();
            var result = await _controller.Logout();
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Site", redirectResult.ControllerName);
            _mockSignInManager.Verify(x => x.SignOutAsync(), Times.Once);
        }

        [Fact]
        public async Task Register_WithValidData_CreatesUserAndSignsIn()
        {
            SetupControllerContext();
            var result = await _controller.Register("newuser@example.com", "New", "User", "Password123!", "1234567890", "/");
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/", redirectResult.Url);
            _mockUserManager.Verify(x => x.CreateAsync(It.IsAny<User>(), "Password123!"), Times.Once);
        }

        [Fact]
        public async Task Register_WithEmptyFirstName_ReturnsErrors()
        {
            SetupControllerContext();
            var result = await _controller.Register("new@example.com", "", "User", "Password123!", "1234567890", "/");
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/", redirectResult.Url);
            Assert.NotNull(_controller.TempData["RegFirstNameInvalid"]);
        }

        [Fact]
        public async Task Register_WithEmptyLastName_ReturnsErrors()
        {
            SetupControllerContext();
            var result = await _controller.Register("new@example.com", "New", "", "Password123!", "1234567890", "/");
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/", redirectResult.Url);
            Assert.NotNull(_controller.TempData["RegLastNameInvalid"]);
        }

        [Fact]
        public async Task Register_WithEmptyEmail_ReturnsErrors()
        {
            SetupControllerContext();
            var result = await _controller.Register("", "New", "User", "Password123!", "1234567890", "/");
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/", redirectResult.Url);
            Assert.NotNull(_controller.TempData["RegEmailInvalid"]);
        }

        [Fact]
        public async Task Register_WithEmptyPassword_ReturnsErrors()
        {
            SetupControllerContext();
            var result = await _controller.Register("new@example.com", "New", "User", "", "1234567890", "/");
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/", redirectResult.Url);
            Assert.NotNull(_controller.TempData["RegPasswordInvalid"]);
        }

        [Fact]
        public async Task Register_WithEmptyPhone_ReturnsErrors()
        {
            SetupControllerContext();
            var result = await _controller.Register("new@example.com", "New", "User", "Password123!", "", "/");
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/", redirectResult.Url);
            Assert.NotNull(_controller.TempData["RegPhoneInvalid"]);
        }

        [Fact]
        public async Task Register_WithExistingEmail_ReturnsError()
        {
            SetupControllerContext();
            var result = await _controller.Register("test@example.com", "New", "User", "Password123!", "1234567890", "/");
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/", redirectResult.Url);
            Assert.NotNull(_controller.TempData["RegEmailError"]);
        }

        [Fact]
        public async Task Register_WhenAlreadyAuthenticated_Redirects()
        {
            SetupAuthenticatedUser(1);
            var result = await _controller.Register("new@example.com", "New", "User", "Password123!", "1234567890", "/");
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/", redirectResult.Url);
        }

        [Fact]
        public void AccessDenied_ReturnsView()
        {
            var result = _controller.AccessDenied("/test");
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ForgotPassword_Get_ReturnsView()
        {
            var result = _controller.ForgotPassword();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task ForgotPassword_Post_WithValidEmail_RedirectsToConfirmation()
        {
            SetupControllerContext();
            var result = await _controller.ForgotPassword("test@example.com");
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AccountController.ForgotPasswordConfirmation), redirectResult.ActionName);
        }

        [Fact]
        public async Task ForgotPassword_Post_WithInvalidEmail_StillRedirectsToConfirmation()
        {
            SetupControllerContext();
            var result = await _controller.ForgotPassword("nonexistent@example.com");
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AccountController.ForgotPasswordConfirmation), redirectResult.ActionName);
        }

        [Fact]
        public void ForgotPasswordConfirmation_ReturnsView()
        {
            var result = _controller.ForgotPasswordConfirmation();
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

        [Fact]
        public async Task ResetPassword_Post_WithValidData_RedirectsToConfirmation()
        {
            SetupControllerContext();
            var model = new ResetPasswordViewModel
            {
                Email = "test@example.com",
                Token = "valid-token",
                Password = "NewPassword123!",
                ConfirmPassword = "NewPassword123!"
            };
            var result = await _controller.ResetPassword(model);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(AccountController.ResetPasswordConfirmation), redirectResult.ActionName);
        }

        [Fact]
        public async Task ResetPassword_Post_WithInvalidModel_ReturnsView()
        {
            SetupControllerContext();
            var model = new ResetPasswordViewModel();
            _controller.ModelState.AddModelError("Email", "Required");
            var result = await _controller.ResetPassword(model);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(model, viewResult.Model);
        }

        [Fact]
        public void ResetPasswordConfirmation_ReturnsView()
        {
            var result = _controller.ResetPasswordConfirmation();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task DeleteAccount_WhenAuthenticated_DeletesUserAndRedirects()
        {
            SetupAuthenticatedUser(1);

            _dbContext.WishlistItems.RemoveRange(_dbContext.WishlistItems);
            _dbContext.CartItems.RemoveRange(_dbContext.CartItems);
            _dbContext.Reviews.RemoveRange(_dbContext.Reviews);
            _dbContext.Orders.RemoveRange(_dbContext.Orders);
            _dbContext.OrderRecipients.RemoveRange(_dbContext.OrderRecipients);
            _dbContext.DeliveryAddresses.RemoveRange(_dbContext.DeliveryAddresses);
            await _dbContext.SaveChangesAsync();

            var result = await _controller.DeleteAccount();
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Site", redirectResult.ControllerName);
            _mockUserManager.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAccount_WhenNotAuthenticated_RedirectsToIndex()
        {
            SetupUnauthenticatedUser();
            var result = await _controller.DeleteAccount();
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Site", redirectResult.ControllerName);
        }

        [Fact]
        public async Task RefreshRoles_WhenAuthenticated_RefreshesSignIn()
        {
            SetupAuthenticatedUser(1);
            var result = await _controller.RefreshRoles();
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Site", redirectResult.ControllerName);
        }

        [Fact]
        public async Task RefreshRoles_WhenNotAuthenticated_StillRedirects()
        {
            SetupUnauthenticatedUser();
            var result = await _controller.RefreshRoles();
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Site", redirectResult.ControllerName);
        }

        [Fact]
        public void ExternalLogin_RedirectsToChallenge()
        {
            SetupControllerContext();
            var result = _controller.ExternalLogin("Google", "/");
            Assert.IsType<ChallengeResult>(result);
        }

        [Fact]
        public void ExternalLogin_WhenAuthenticated_Redirects()
        {
            SetupAuthenticatedUser(1);
            var result = _controller.ExternalLogin("Google", "/dashboard");
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/dashboard", redirectResult.Url);
        }

        [Fact]
        public void LinkLogin_WhenAuthorized_ReturnsChallenge()
        {
            SetupAuthenticatedUser(1);
            var result = _controller.LinkLogin("Google");
            Assert.IsType<ChallengeResult>(result);
        }
    }
}