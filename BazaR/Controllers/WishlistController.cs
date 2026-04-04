using BazaR.Interfaces;
using BazaR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BazaR.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        #region Private Fields

        private readonly IUserDb _usMan;
        private readonly UserManager<User> _userManager;

        #endregion

        #region Constructor

        public WishlistController(IUserDb usMan, UserManager<User> userManager)
        {
            _usMan = usMan;
            _userManager = userManager;
        }

        #endregion

        #region Public Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var items = _usMan.GetWishList(user.Id).ToList();
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            _usMan.AddToWishList(user.Id, id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            _usMan.RemoveFromWishList(user.Id, id);
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}