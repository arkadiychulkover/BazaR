using BazaR.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BazaR.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IUserDb _usMan;

        public WishlistController(IUserDb usMan)
        {
            _usMan = usMan;
        }

        private int? CurrentUserId => HttpContext.Session.GetInt32("uid");
        private bool IsAuthenticated => CurrentUserId.HasValue;

        private IActionResult RequireLogin(string? returnUrl = null)
        {
            TempData["Error"] = "Нужно войти в аккаунт.";
            return RedirectToAction("Index", "Site", new { returnUrl });
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(Index)));
            var userId = CurrentUserId!.Value;
            var items = _usMan.GetWishList(userId).ToList();
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int id)
        {
            if (!IsAuthenticated) return RequireLogin();
            _usMan.AddToWishList(CurrentUserId!.Value, id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            if (!IsAuthenticated) return RequireLogin();
            _usMan.RemoveFromWishList(CurrentUserId!.Value, id);
            return RedirectToAction(nameof(Index));
        }
    }
}
