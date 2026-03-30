using BazaR.Data;
using BazaR.Filters;
using BazaR.Interfaces;
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
        private readonly SignInManager<User> _signInManager;
        private readonly AppDbContext _appDbContext;
        private readonly IUserDb _UserRepo;
        private readonly string adminRoleName = "Admin";

        public AdminController(
            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            SignInManager<User> signInManager,
            AppDbContext appDbContext,
            IUserDb userDb)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
            _UserRepo = userDb;
        }

        #region Users

        [HttpGet]
        public IActionResult Index()
        {
            IQueryable<User> users = _appDbContext.Users;
            ViewBag.Active = 0;
            ViewBag.ForMonth = 0;
            ViewBag.ForWeek = 0;
            ViewBag.ForDay = 0;
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User us = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (us != null)
            {
                await _userManager.DeleteAsync(us);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            User us = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (us == null) return RedirectToAction(nameof(Index));
            return View(us);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(User user)
        {
            User us = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (us == null) return RedirectToAction(nameof(Index));

            us.Name = user.Name;
            us.Email = user.Email;
            us.PhoneNumber = user.PhoneNumber;
            us.IsAdmin = user.IsAdmin;

            // Управление ролью Admin
            if (user.IsAdmin)
            {
                if (!await _userManager.IsInRoleAsync(us, adminRoleName))
                    await _userManager.AddToRoleAsync(us, adminRoleName);
            }
            else
            {
                if (await _userManager.IsInRoleAsync(us, adminRoleName))
                    await _userManager.RemoveFromRoleAsync(us, adminRoleName);
            }

            await _userManager.UpdateAsync(us);

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null && currentUser.Id == us.Id)
                await _signInManager.RefreshSignInAsync(us);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlockUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return RedirectToAction(nameof(Index));

            user.LockoutEnabled = true;
            user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(1);

            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnblockUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return RedirectToAction(nameof(Index));

            user.LockoutEnd = null;

            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> UserStatistic(int id)
        {
            User us = await _appDbContext.Users
                .Include(u => u.SellingItems).ThenInclude(i => i.Category)
                .Include(u => u.SellingItems).ThenInclude(i => i.Brand)
                .Include(u => u.Reviews).ThenInclude(r => r.Item)
                .Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (us == null) return RedirectToAction(nameof(Index));
            return View(us);
        }

        #region Items
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserItem(int id)
        {
            Item it = await _appDbContext.Items.FirstOrDefaultAsync(i => i.Id == id);
            if (it != null)
            {
                int userId = it.UserId;
                _appDbContext.Items.Remove(it);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(UserStatistic), new { id = userId });
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditItem(int id)
        {
            Item it = await _appDbContext.Items
                .Include(i => i.Category)
                .Include(i => i.Brand)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (it == null) return RedirectToAction(nameof(Index));
            return View(it);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItem(Item item)
        {
            Item it = await _appDbContext.Items.FirstOrDefaultAsync(i => i.Id == item.Id);
            if (it != null)
            {
                it.Name = item.Name;
                it.Desc = item.Desc;
                it.Price = item.Price;
                it.Garantia = item.Garantia;
                it.IsAvailable = item.IsAvailable;
                it.ImageUrl = item.ImageUrl;

                await _appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(UserStatistic), new { id = it.UserId });
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Reviews
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserReview(int id)
        {
            Review rev = await _appDbContext.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if (rev != null)
            {
                int userId = rev.UserId;
                _appDbContext.Reviews.Remove(rev);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(UserStatistic), new { id = userId });
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditReview(int id)
        {
            Review rev = await _appDbContext.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if (rev == null) return RedirectToAction(nameof(Index));
            return View(rev);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditReview(Review review)
        {
            Review rev = await _appDbContext.Reviews.FirstOrDefaultAsync(r => r.Id == review.Id);
            if (rev != null)
            {
                rev.Comment = review.Comment;
                rev.Rating = review.Rating;
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(UserStatistic), new { id = rev.UserId });
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Orders
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            Order or = await _appDbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (or != null)
            {
                int userid = or.UserId;
                _appDbContext.Orders.Remove(or);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(UserStatistic), new { id = userid });
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetails(int id)
        {
            Order order = await _appDbContext.Orders
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Item)
                .Include(o => o.City)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return RedirectToAction(nameof(Index));

            ViewBag.Items = order.OrderItems;
            ViewBag.City = order.City;
            ViewBag.User = order.User;

            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> EditOrder(int id)
        {
            Order order = await _appDbContext.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return RedirectToAction(nameof(Index));
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOrder(int id,
            OrderStatus status,
            OrderPaymentMethod paymentMethod,
            OrderPaymentStatus paymentStatus,
            OrderDeliveryMethod deliveryMethod,
            string? ttn)
        {
            Order order = await _appDbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return RedirectToAction(nameof(Index));

            order.Status = status;
            order.PaymentMethod = paymentMethod;
            order.PaymentStatus = paymentStatus;
            order.DeliveryMethod = deliveryMethod;
            order.Ttn = ttn?.Trim();

            await _appDbContext.SaveChangesAsync();
            TempData["Ok"] = $"Замовлення №{order.Number} оновлено.";
            return RedirectToAction(nameof(OrderDetails), new { id });
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> PopularCategories()
        {
            var popularDict = await _appDbContext.Categories
                .OrderBy(c => c.Name)
                .Take(20)
                .ToDictionaryAsync(c => c.Name, c => 0);
            return View(popularDict);
        }

        [HttpGet]
        public IActionResult GetLog()
        {
            var log = _appDbContext.VisitingModels.ToList();
            foreach (var _ in log)
                Console.WriteLine("\n\nLOGS\n\n");
            return View(log);
        }

        [HttpGet]
        public IActionResult GetLogByUserId(int id)
        {
            var log = _appDbContext.VisitingModels.Where(v => v.UserId == id).ToList();
            return View(log);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLogs() 
        {
            _appDbContext.VisitingModels.RemoveRange(_appDbContext.VisitingModels);
            await _appDbContext.SaveChangesAsync();

            await _appDbContext.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('VisitingModels', RESEED, 0)");
            return RedirectToAction("GetLog");
        }
    }
}