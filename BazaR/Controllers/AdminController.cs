using BazaR.Data;
using BazaR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BazaR.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly AppDbContext _appDbContext;
        private readonly string adminRoleName = "Admin";

        public AdminController(
            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            AppDbContext appDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appDbContext = appDbContext;
        }

        // =========================
        // USERS
        // =========================
        #region

        [HttpGet]
        public IActionResult Index()
        {
            IQueryable<User> users = _appDbContext.Users;
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User us = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (us == null)
            {
                return RedirectToAction(nameof(Index));
            }

            await _userManager.DeleteAsync(us);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            User us = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (us == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(us);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(User user)
        {
            User us = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (us == null)
            {
                return RedirectToAction(nameof(Index));
            }

            us.Name = user.Name;
            us.Email = user.Email;
            //us.PhoneNumber = user.PhoneNumber;
            us.IsAdmin = user.IsAdmin;

            if (user.IsAdmin)
                await _userManager.AddToRoleAsync(us, adminRoleName);

            await _userManager.UpdateAsync(us);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        // =========================
        // USER STATISTIC
        // =========================

        [HttpGet]
        public async Task<IActionResult> UserStatistic(int id)
        {
            User us = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (us == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(us);
        }

        // =========================
        // ITEMS
        // =========================
        #region
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserItem(int id)
        {
            Item it = await _appDbContext.Items.FirstOrDefaultAsync(i => i.Id == id);

            if (it == null)
            {
                return RedirectToAction(nameof(Index));
            }

            int userId = it.UserId;

            _appDbContext.Items.Remove(it);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(UserStatistic), new { id = userId });
        }

        [HttpGet]
        public async Task<IActionResult> EditItem(int id)
        {
            Item it = await _appDbContext.Items
                .Include(i => i.Category)
                .Include(i => i.Brand)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (it == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(it);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItem(Item item)
        {
            Item it = await _appDbContext.Items.FirstOrDefaultAsync(i => i.Id == item.Id);

            if (it == null)
            {
                return RedirectToAction(nameof(Index));
            }

            it.Name = item.Name;
            it.Desc = item.Desc;
            it.Price = item.Price;
            it.Garantia = item.Garantia;
            it.IsAvailable = item.IsAvailable;
            it.ImageUrl = item.ImageUrl;

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(UserStatistic), new { id = it.UserId });
        }
        #endregion

        // =========================
        // REVIEWS (COMMENTS)
        // =========================
        #region

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserReview(int id)
        {
            Review rev = await _appDbContext.Reviews.FirstOrDefaultAsync(r => r.Id == id);

            if (rev == null)
            {
                return RedirectToAction(nameof(Index));
            }

            int userId = rev.UserId;

            _appDbContext.Reviews.Remove(rev);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(UserStatistic), new { id = userId });
        }

        [HttpGet]
        public async Task<IActionResult> EditReview(int id)
        {
            Review rev = await _appDbContext.Reviews.FirstOrDefaultAsync(r => r.Id == id);

            if (rev == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(rev);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditReview(Review review)
        {
            Review rev = await _appDbContext.Reviews.FirstOrDefaultAsync(r => r.Id == review.Id);

            if (rev == null)
            {
                return RedirectToAction(nameof(Index));
            }

            rev.Comment = review.Comment;
            rev.Rating = review.Rating;

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(UserStatistic), new { id = rev.UserId });
        }
        #endregion
    }
}