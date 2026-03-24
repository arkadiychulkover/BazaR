using BazaR.Interfaces;
using BazaR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BazaR.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IUserDb _usMan;
        private readonly UserManager<User> _userManager;

        public WishlistController(IUserDb usMan, UserManager<User> userManager)
        {
            _usMan = usMan;
            _userManager = userManager;
        }

        private User? CurrentUser => User.Identity?.IsAuthenticated == true
            ? _userManager.GetUserAsync(User).Result
            : null;
        private bool IsAuthenticated => User.Identity?.IsAuthenticated == true;

        private IActionResult RequireLogin(string? returnUrl = null)
        {
            TempData["Error"] = "Нужно войти в аккаунт.";
            return RedirectToAction("Index", "Site", new { returnUrl });
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(Index)));
            var userId = CurrentUser!.Id;
            var items = _usMan.GetWishList(userId).ToList();
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id)
        {
            if (!IsAuthenticated) return RequireLogin();
            _usMan.AddToWishList(CurrentUser!.Id, id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            if (!IsAuthenticated) return RequireLogin();
            _usMan.RemoveFromWishList(CurrentUser!.Id, id);
            return RedirectToAction(nameof(Index));
        }
    }
}
