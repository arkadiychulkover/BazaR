using BazaR.Data;
using BazaR.Filters;
using BazaR.Interfaces;
using BazaR.Models;
using BazaR.ViewModels;
using BazaR.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

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
        private readonly IUserStatistick _StatistickRepo;
        private readonly IMemoryCache _cache;
        private readonly string adminRoleName = "Admin";

        public AdminController(
            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            SignInManager<User> signInManager,
            AppDbContext appDbContext,
            IUserDb userDb, IUserStatistick StatistickRepo,
            IMemoryCache cache)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
            _UserRepo = userDb;
            _StatistickRepo = StatistickRepo;
            _cache = cache;
        }

        #region Users

        [HttpGet]
        public IActionResult Index([FromServices] ActiveUsersService service)
        {
            IQueryable<User> users = _appDbContext.Users;
            ViewBag.Active = service.GetOnlineUsersCount();
            ViewBag.ForMonth = _StatistickRepo.GetUsersCountForMonthAsync();
            ViewBag.ForWeek = _StatistickRepo.GetUsersCountForWeekAsync();
            ViewBag.ForDay = _StatistickRepo.GetUsersCountForDayAsync();
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
                //CACHE
                _cache.Remove("browse:*");

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
                //CACHE
                _cache.Remove("browse:*");

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

        #region Promotions
        [HttpGet]
        public async Task<IActionResult> Promotions()
        {
            var promotions = await _appDbContext.Promotions.ToListAsync();
            return View(promotions);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPromotion(Promotion prom)
        {
            _appDbContext.Promotions.Add(prom);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePromotion(int id)
        {
            var promotion = await _appDbContext.Promotions.FindAsync(id);

            if (promotion != null)
            {
                _appDbContext.Promotions.Remove(promotion);
                await _appDbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Promotions));
        }
        #endregion

        #region Mails
        [HttpGet]
        public async Task<IActionResult> IndexMail(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();
            //CACHE
            var cacheKey = $"messages:{user.Id}";

            if (!_cache.TryGetValue(cacheKey, out List<Message> messages))
            {
                messages = await _appDbContext.Messages
                    .Where(m => m.UserId == id)
                    .OrderByDescending(m => m.DateTime)
                    .ToListAsync();

                _cache.Set(cacheKey, messages, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
                    SlidingExpiration = TimeSpan.FromMinutes(1)
                });
            }

            var model = new AdminMessageViewModel
            {
                UserId = id,
                UserName = user.Name ?? user.Email ?? "—",
                Messages = messages
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(SendMessageViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(vm.UserId.ToString());

                var messages = await _appDbContext.Messages
                    .Where(m => m.UserId == vm.UserId)
                    .OrderByDescending(m => m.DateTime)
                    .ToListAsync();

                var model = new AdminMessageViewModel
                {
                    UserId = vm.UserId,
                    UserName = user?.Name ?? user?.Email ?? "—",
                    Messages = messages,
                    NewMessage = vm
                };

                return View("IndexMail", model);
            }
            var sender = await _userManager.GetUserAsync(User);
            var message = new Message
            {
                UserId = vm.UserId,
                Name = vm.Name.Trim(),
                Content = vm.Content.Trim(),
                DateTime = DateTime.UtcNow,
                IsRead = false,
                SenderId = sender.Id,
                SenderName = sender?.Name ?? sender?.Email
            };

            _appDbContext.Messages.Add(message);
            await _appDbContext.SaveChangesAsync();
            //CACHE
            _cache.Remove("messages:{user.Id}");
            TempData["Success"] = "Сообщение успешно отправлено.";
            return RedirectToAction(nameof(IndexMail), new { id = vm.UserId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMessage(int id, int userId)
        {
            var message = await _appDbContext.Messages.FindAsync(id);

            if (message != null && message.UserId == userId)
            {
                _appDbContext.Messages.Remove(message);
                await _appDbContext.SaveChangesAsync();
            }
            //CACHE
            _cache.Remove("messages:{user.Id}");
            return RedirectToAction(nameof(IndexMail), new { id = userId });
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> PopularCategories()
        {
            var popularDict = await _StatistickRepo.GetPopularCategoryAsync();
            return View(popularDict);
        }

        [HttpGet]
        public async Task<IActionResult> GetLog()
        {
            List<VisitingModel> log = _appDbContext.VisitingModels.ToList();
            foreach (VisitingModel model in log) 
            {
                if(model.SearchFilters != null)
                    Console.WriteLine(model.SearchFilters.Id);
                Console.WriteLine("\n\nLOGS\n\n");
            }
            return View(log);
        }

        [HttpGet]
        public async Task<IActionResult> GetLogByUserId(int id) 
        {
            List<VisitingModel> log = _appDbContext.VisitingModels.Where<VisitingModel>(v => v.UserId == id).ToList();
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