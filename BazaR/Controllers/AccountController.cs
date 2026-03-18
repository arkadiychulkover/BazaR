using BazaR.Helper;
using BazaR.Models;
using BazaR.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BazaR.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            var schemes = await _signInManager.GetExternalAuthenticationSchemesAsync();

            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = schemes.ToList()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View("AccessDenied");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, string? returnUrl = null, bool remember = true)
        {
            returnUrl ??= Url.Action("Index", "Site");

            email = (email ?? string.Empty).Trim();

            if (User.Identity?.IsAuthenticated ?? false)
                return Redirect(returnUrl);

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                TempData["OpenAuth"] = "login";
                TempData["LoginEmail"] = email;
                TempData["LoginEmailInvalid"] = string.IsNullOrWhiteSpace(email);
                TempData["LoginPasswordInvalid"] = string.IsNullOrWhiteSpace(password);
                TempData["LoginEmailError"] = string.IsNullOrWhiteSpace(email)
                    ? "Введіть адресу ел. пошти або номер телефону"
                    : "";
                TempData["LoginPasswordError"] = string.IsNullOrWhiteSpace(password)
                    ? "Введіть пароль"
                    : "";

                return Redirect(returnUrl);
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                TempData["OpenAuth"] = "login";
                TempData["LoginEmail"] = email;
                TempData["LoginEmailInvalid"] = true;
                TempData["LoginPasswordInvalid"] = true;
                TempData["LoginEmailError"] = "Введено невірну адресу ел. пошти або номер телефону";
                TempData["LoginPasswordError"] = "Введено невірний пароль";

                return Redirect(returnUrl);
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, remember, true);

            if (!result.Succeeded)
            {
                TempData["OpenAuth"] = "login";
                TempData["LoginEmail"] = email;
                TempData["LoginEmailInvalid"] = true;
                TempData["LoginPasswordInvalid"] = true;
                TempData["LoginEmailError"] = "Введено невірну адресу ел. пошти або номер телефону";
                TempData["LoginPasswordError"] = "Введено невірний пароль";

                return Redirect(returnUrl);
            }

            TempData["Ok"] = "Успішний вхід.";
            return Redirect(returnUrl);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult LinkLogin(string provider)
        {
            var redirectUrl = Url.Action("LinkLoginCallback", "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(
                provider,
                redirectUrl,
                _userManager.GetUserId(User)
            );
            return Challenge(properties, provider);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LinkLoginCallback()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            var info = await _signInManager.GetExternalLoginInfoAsync(user.Id.ToString());
            if (info == null)
            {
                TempData["Error"] = "Не вдалося отримати дані від Google.";
                return RedirectToAction("Profile", "Profile");
            }

            var result = await _userManager.AddLoginAsync(user, info);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                TempData["Success"] = "Акаунт успішно прив'язано до Google.";
            }
            else
            {
                TempData["Error"] = result.Errors.FirstOrDefault()?.Description
                    ?? "Помилка при прив'язці акаунту.";
            }

            return RedirectToAction("Profile", "Profile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string email, string firstName, string lastName, string password, string phoneNumber, string? returnUrl = null)
        {
            returnUrl ??= Url.Action("Index", "Site");

            email = (email ?? string.Empty).Trim();
            firstName = (firstName ?? string.Empty).Trim();
            lastName = (lastName ?? string.Empty).Trim();
            phoneNumber = (phoneNumber ?? string.Empty).Trim();

            if (User.Identity?.IsAuthenticated ?? false)
                return Redirect(returnUrl);

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(phoneNumber))
            {
                TempData["OpenAuth"] = "register";
                TempData["RegFirstName"] = firstName;
                TempData["RegLastName"] = lastName;
                TempData["RegEmail"] = email;
                TempData["RegPhone"] = phoneNumber;

                TempData["RegFirstNameInvalid"] = string.IsNullOrWhiteSpace(firstName);
                TempData["RegLastNameInvalid"] = string.IsNullOrWhiteSpace(lastName);
                TempData["RegEmailInvalid"] = string.IsNullOrWhiteSpace(email);
                TempData["RegPhoneInvalid"] = string.IsNullOrWhiteSpace(phoneNumber);
                TempData["RegPasswordInvalid"] = string.IsNullOrWhiteSpace(password);

                TempData["RegFirstNameError"] = string.IsNullOrWhiteSpace(firstName) ? "Введіть своє ім'я" : "";
                TempData["RegLastNameError"] = string.IsNullOrWhiteSpace(lastName) ? "Введіть своє прізвище" : "";
                TempData["RegPhoneError"] = string.IsNullOrWhiteSpace(phoneNumber) ? "Введіть номер мобільного телефону" : "";
                TempData["RegEmailError"] = string.IsNullOrWhiteSpace(email) ? "Введіть свою ел. пошту" : "";
                TempData["RegPasswordError"] = string.IsNullOrWhiteSpace(password) ? "Введіть пароль" : "";

                return Redirect(returnUrl);
            }

            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                TempData["OpenAuth"] = "register";
                TempData["RegFirstName"] = firstName;
                TempData["RegLastName"] = lastName;
                TempData["RegEmail"] = email;
                TempData["RegPhone"] = phoneNumber;

                TempData["RegEmailInvalid"] = true;
                TempData["RegEmailError"] = "Email вже використовується";

                return Redirect(returnUrl);
            }

            var user = new User
            {
                Email = email,
                UserName = email,
                Name = (firstName + " " + lastName).Trim(),
                PhoneNumber = phoneNumber,
                IsAdmin = false
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                TempData["OpenAuth"] = "register";
                TempData["RegFirstName"] = firstName;
                TempData["RegLastName"] = lastName;
                TempData["RegEmail"] = email;
                TempData["RegPhone"] = phoneNumber;

                var errors = result.Errors.Select(x => x.Description).ToList();
                var hasEmailError = errors.Any(x => x.Contains("email", StringComparison.OrdinalIgnoreCase));

                TempData["RegEmailInvalid"] = hasEmailError;
                TempData["RegPasswordInvalid"] = !hasEmailError;

                TempData["RegEmailError"] = hasEmailError ? "Введіть коректну ел. пошту" : "";
                TempData["RegPasswordError"] = !hasEmailError ? errors.FirstOrDefault() ?? "Некоректний пароль" : "";

                return Redirect(returnUrl);
            }

            // Добавляем роль User по умолчанию
            await _userManager.AddToRoleAsync(user, "User");

            await _signInManager.SignInAsync(user, new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(12),
                AllowRefresh = true
            });

            TempData["Ok"] = "Реєстрація вдала.";
            return Redirect(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Ok"] = "Ви вийшли з акаунту.";
            return RedirectToAction("Index", "Site");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string? returnUrl = null)
        {
            returnUrl ??= Url.Action("Index", "Site");

            if (User.Identity?.IsAuthenticated ?? false)
                return Redirect(returnUrl);

            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
        {
            returnUrl ??= Url.Action("Index", "Site");

            if (!string.IsNullOrEmpty(remoteError))
            {
                TempData["OpenAuth"] = "login";
                TempData["LoginEmailInvalid"] = true;
                TempData["LoginEmailError"] = $"Помилка зовнішнього входу: {remoteError}";
                return Redirect(returnUrl);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                TempData["OpenAuth"] = "login";
                TempData["LoginEmailInvalid"] = true;
                TempData["LoginEmailError"] = "Не вдалося отримати дані зовнішнього входу.";
                return Redirect(returnUrl);
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(
                info.LoginProvider,
                info.ProviderKey,
                isPersistent: true,
                bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return Redirect(returnUrl);
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrWhiteSpace(email))
            {
                TempData["OpenAuth"] = "login";
                TempData["LoginEmailInvalid"] = true;
                TempData["LoginEmailError"] = "Зовнішній сервіс не повернув email.";
                return Redirect(returnUrl);
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                var firstNameClaim = info.Principal.FindFirstValue(ClaimTypes.GivenName) ?? "";
                var lastNameClaim = info.Principal.FindFirstValue(ClaimTypes.Surname) ?? "";

                user = new User
                {
                    Email = email,
                    UserName = email,
                    Name = $"{firstNameClaim} {lastNameClaim}".Trim(),
                    IsAdmin = false
                };

                var createResult = await _userManager.CreateAsync(user);

                if (!createResult.Succeeded)
                {
                    TempData["OpenAuth"] = "login";
                    TempData["LoginEmailInvalid"] = true;
                    TempData["LoginEmailError"] = createResult.Errors.FirstOrDefault()?.Description ?? "Не вдалося створити користувача.";
                    return Redirect(returnUrl);
                }

                // Добавляем роль User
                await _userManager.AddToRoleAsync(user, "User");
            }

            var addLoginResult = await _userManager.AddLoginAsync(user, info);

            if (!addLoginResult.Succeeded &&
                !addLoginResult.Errors.Any(e => e.Code == "LoginAlreadyAssociated"))
            {
                TempData["OpenAuth"] = "login";
                TempData["LoginEmailInvalid"] = true;
                TempData["LoginEmailError"] = addLoginResult.Errors.FirstOrDefault()?.Description ?? "Не вдалося прив'язати зовнішній вхід.";
                return Redirect(returnUrl);
            }

            await _signInManager.SignInAsync(user, new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(12),
                AllowRefresh = true
            });

            return Redirect(returnUrl);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Index", "Site");

            await _signInManager.SignOutAsync();

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                TempData["Error"] = result.Errors.FirstOrDefault()?.Description
                    ?? "Не вдалося видалити акаунт.";
                return RedirectToAction("Profile", "Profile");
            }

            TempData["Ok"] = "Акаунт успішно видалено.";
            return RedirectToAction("Index", "Site");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RefreshRoles()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                await _signInManager.RefreshSignInAsync(user);
                TempData["Ok"] = "Ролі оновлено";
            }
            return RedirectToAction("Index", "Site");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            if (!ModelState.IsValid)
                return View(model: email);

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

            var emailHelper = new EmailHelper();
            await emailHelper.SendEmailPasswordReset(user.Email!, link!);

            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordViewModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (!ModelState.IsValid)
                return View(resetPassword);

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
                return RedirectToAction(nameof(ResetPasswordConfirmation));

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return View(resetPassword);
            }

            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}