using BazaR.Data;
using BazaR.Models;
using BazaR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BazaR.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _db;

        public ProfileController(UserManager<User> userManager, AppDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        private async Task<(User user, AccountProfileViewModel vm)> GetUserAndProfileAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new AccountProfileViewModel
            {
                FirstName = user?.Name ?? string.Empty,
                Email = user?.Email ?? string.Empty,
                PhoneNumber = user?.PhoneNumber
            };
            return (user!, vm);
        }

        private async Task<User> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(User);
        }

        public async Task<IActionResult> Profile()
        {
            ViewBag.ActiveMenu = "Profile";
            var (_, profile) = await GetUserAndProfileAsync();
            return View(profile);
        }

        public async Task<IActionResult> Orders()
        {
            ViewBag.ActiveMenu = "Orders";
            var (user, profile) = await GetUserAndProfileAsync();

            var orders = await _db.Orders
                .Where(o => o.UserId == user.Id)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            var vm = new AccountOrdersViewModel
            {
                Profile = profile,
                Orders = orders.Select(o => new OrderVm
                {
                    Id = o.Id,
                    CreatedAt = o.CreatedAt,
                    Status = o.Status,
                    Total = o.OrderItems.Sum(oi => oi.Item.Price * oi.Quantity),
                    Items = o.OrderItems.Select(oi => new OrderItemVm
                    {
                        Name = oi.Item.Name,
                        ImageUrl = oi.Item.ImageUrl,
                        Price = oi.Item.Price,
                        Quantity = oi.Quantity
                    }).ToList()
                }).ToList()
            };

            return View(vm);
        }

        public async Task<IActionResult> Wishlist()
        {
            ViewBag.ActiveMenu = "Wishlist";
            var (user, profile) = await GetUserAndProfileAsync();

            var items = await _db.WishlistItems
                .Where(w => w.UserId == user.Id)
                .Include(w => w.Item)
                .Select(w => new ItemCardVm
                {
                    Id = w.Item.Id,
                    Name = w.Item.Name,
                    Price = w.Item.Price,
                    ImageUrl = w.Item.ImageUrl,
                    InWishlist = true,
                    IsLooked = false
                })
                .ToListAsync();

            var vm = new AccountProductsViewModel
            {
                Profile = profile,
                Items = items
            };

            return View(vm);
        }

        public async Task<IActionResult> LoockedCards()
        {
            ViewBag.ActiveMenu = "LoockedCards";
            var user = await GetCurrentUserAsync();
            var (_, profile) = await GetUserAndProfileAsync();

            var lookedCards = await _db.LookedCards
                .Where(lc => lc.UserId == user.Id && lc.IsLooked)
                .Include(lc => lc.Item)
                .OrderByDescending(lc => lc.LookedAt)
                .Select(lc => new LookedCardItemViewModel
                {
                    Id = lc.Id,
                    ItemId = lc.Item.Id,
                    ItemName = lc.Item.Name,
                    ItemImageUrl = lc.Item.ImageUrl,
                    Price = lc.Item.Price,
                    IsLooked = lc.IsLooked,
                    LookedAt = lc.LookedAt,
                    ViewCount = lc.ViewCount
                })
                .ToListAsync();

            var vm = new LookedCardsViewModel
            {
                Profile = profile,
                LookedCards = lookedCards
            };

            return View(vm);
        }

        public async Task<IActionResult> Mailings()
        {
            ViewBag.ActiveMenu = "Mailings";
            var user = await GetCurrentUserAsync();
            var (_, profile) = await GetUserAndProfileAsync();

            var mailingSetting = await _db.MailingSettings
                .FirstOrDefaultAsync(ms => ms.UserId == user.Id);

            if (mailingSetting == null)
            {
                mailingSetting = new MailingSetting
                {
                    UserId = user.Id,
                    NewsAndUpdates = true,
                    SpecialOffers = true,
                    PersonalRecommendations = true,
                    ProductAlerts = false,
                    WeeklyDigest = false,
                    PreferredFrequency = "weekly"
                };
                _db.MailingSettings.Add(mailingSetting);
                await _db.SaveChangesAsync();
            }

            var vm = new MailingViewModel
            {
                Profile = profile,
                NewsAndUpdates = mailingSetting.NewsAndUpdates,
                SpecialOffers = mailingSetting.SpecialOffers,
                PersonalRecommendations = mailingSetting.PersonalRecommendations,
                ProductAlerts = mailingSetting.ProductAlerts,
                WeeklyDigest = mailingSetting.WeeklyDigest,
                PreferredFrequency = mailingSetting.PreferredFrequency
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> MailingsUpdate(MailingViewModel model)
        {
            var user = await GetCurrentUserAsync();

            var mailingSetting = await _db.MailingSettings
                .FirstOrDefaultAsync(ms => ms.UserId == user.Id);

            if (mailingSetting == null)
            {
                mailingSetting = new MailingSetting { UserId = user.Id };
                _db.MailingSettings.Add(mailingSetting);
            }

            mailingSetting.NewsAndUpdates = model.NewsAndUpdates;
            mailingSetting.SpecialOffers = model.SpecialOffers;
            mailingSetting.PersonalRecommendations = model.PersonalRecommendations;
            mailingSetting.ProductAlerts = model.ProductAlerts;
            mailingSetting.WeeklyDigest = model.WeeklyDigest;
            mailingSetting.PreferredFrequency = model.PreferredFrequency;
            mailingSetting.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Mailings));
        }

        public async Task<IActionResult> Wallet()
        {
            ViewBag.ActiveMenu = "Wallet";
            var user = await GetCurrentUserAsync();
            var (_, profile) = await GetUserAndProfileAsync();

            var wallet = await _db.Wallets
                .FirstOrDefaultAsync(w => w.UserId == user.Id);

            if (wallet == null)
            {
                wallet = new Wallet
                {
                    UserId = user.Id,
                    Balance = 0,
                    MonthlySpent = 0
                };
                _db.Wallets.Add(wallet);
                await _db.SaveChangesAsync();
            }

            var vm = new WalletViewModel
            {
                Profile = profile,
                Balance = wallet.Balance,
                MonthlySpent = wallet.MonthlySpent,
                LastReplenishmentAmount = wallet.LastReplenishmentAmount > 0
                    ? wallet.LastReplenishmentAmount : null,
                LastReplenishmentDate = wallet.LastReplenishment,
                Status = wallet.Balance > 0 ? "Активний гаманець" : "Пустий гаманець"
            };

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Promotions()
        {
            ViewBag.ActiveMenu = "Promotions";
            var user = await GetCurrentUserAsync();
            var (_, profile) = await GetUserAndProfileAsync();

            var subscribedIds = await _db.UserPromotions
                .Where(up => up.UserId == user.Id)
                .Select(up => up.PromotionId)
                .ToHashSetAsync();

            var promotions = await _db.Promotions
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.StartsAt)
                .Select(p => new PromotionItemViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    CategoryName = p.CategoryName,
                    DiscountPercent = p.DiscountPercent,
                    StartsAt = p.StartsAt,
                    EndsAt = p.EndsAt,
                    IsSubscribed = subscribedIds.Contains(p.Id)
                })
                .ToListAsync();

            var vm = new PromotionsViewModel
            {
                Profile = profile,
                Promotions = promotions
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TogglePromotion(int id)
        {
            var user = await GetCurrentUserAsync();

            var existing = await _db.UserPromotions
                .FirstOrDefaultAsync(up => up.UserId == user.Id && up.PromotionId == id);

            if (existing == null)
            {
                _db.UserPromotions.Add(new UserPromotion
                {
                    UserId = user.Id,
                    PromotionId = id,
                    SubscribedAt = DateTime.UtcNow
                });
            }
            else
            {
                _db.UserPromotions.Remove(existing);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Promotions));
        }

        public async Task<IActionResult> BonusAccount()
        {
            ViewBag.ActiveMenu = "BonusAccount";
            var user = await GetCurrentUserAsync();
            var (_, profile) = await GetUserAndProfileAsync();

            var bonusAccount = await _db.BonusAccounts
                .FirstOrDefaultAsync(ba => ba.UserId == user.Id);

            if (bonusAccount == null)
            {
                bonusAccount = new BonusAccount
                {
                    UserId = user.Id,
                    TotalBalance = 0,
                    MonthlyAccrued = 0,
                    MonthlySpent = 0,
                    AccrualRate = 1,
                    ExpirationDate = DateTime.UtcNow.AddYears(1)
                };
                _db.BonusAccounts.Add(bonusAccount);
                await _db.SaveChangesAsync();
            }

            var vm = new BonusAccountViewModel
            {
                Profile = profile,
                TotalBalance = bonusAccount.TotalBalance,
                MonthlyAccrued = bonusAccount.MonthlyAccrued,
                MonthlySpent = bonusAccount.MonthlySpent,
                AccrualRate = bonusAccount.AccrualRate,
                ExpirationDate = bonusAccount.ExpirationDate
            };

            return View(vm);
        }

        public async Task<IActionResult> Premium()
        {
            ViewBag.ActiveMenu = "Premium";
            var user = await GetCurrentUserAsync();
            var (_, profile) = await GetUserAndProfileAsync();

            var premiumSub = await _db.PremiumSubscriptions
                .FirstOrDefaultAsync(p => p.UserId == user.Id);

            if (premiumSub == null)
            {
                premiumSub = new PremiumSubscription
                {
                    UserId = user.Id,
                    IsActive = false,
                    PlanType = "basic",
                    MonthlyPrice = 99
                };
                _db.PremiumSubscriptions.Add(premiumSub);
                await _db.SaveChangesAsync();
            }

            var features = new List<PremiumFeatureViewModel>
            {
                new() { Name = "Пріоритетна підтримка",
                        Description = "24/7 підтримка з пріоритетом",
                        IsAvailable = premiumSub.IsActive },
                new() { Name = "Ексклюзивні знижки",
                        Description = "Доступ до спеціальних пропозицій та акцій",
                        IsAvailable = premiumSub.IsActive },
                new() { Name = "Ранній доступ до новинок",
                        Description = "Першим дізнавайтесь про нові товари та оновлення",
                        IsAvailable = premiumSub.IsActive },
                new() { Name = "Безпечна доставка",
                        Description = "Страхування при доставці",
                        IsAvailable = premiumSub.IsActive }
            };

            var vm = new PremiumViewModel
            {
                Profile = profile,
                IsActive = premiumSub.IsActive,
                PlanType = premiumSub.PlanType,
                StartDate = premiumSub.StartDate,
                EndDate = premiumSub.EndDate,
                MonthlyPrice = premiumSub.MonthlyPrice,
                AutoRenewal = premiumSub.AutoRenewal,
                Features = features
            };

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivatePremium()
        {
            var user = await GetCurrentUserAsync();

            var premiumSub = await _db.PremiumSubscriptions
                .FirstOrDefaultAsync(p => p.UserId == user.Id);

            var wallet = await _db.Wallets
                .FirstOrDefaultAsync(w => w.UserId == user.Id);

            if (premiumSub == null || wallet == null)
                return RedirectToAction(nameof(Premium));

            if (wallet.Balance < premiumSub.MonthlyPrice)
            {
                TempData["PremiumError"] = $"Недостатньо коштів. Потрібно {premiumSub.MonthlyPrice:F2} ₴, на рахунку {wallet.Balance:F2} ₴.";
                return RedirectToAction(nameof(Premium));
            }

            wallet.Balance -= premiumSub.MonthlyPrice;
            wallet.MonthlySpent += premiumSub.MonthlyPrice;

            premiumSub.IsActive = true;
            premiumSub.StartDate = DateTime.UtcNow;
            premiumSub.EndDate = DateTime.UtcNow.AddMonths(1);
            premiumSub.AutoRenewal = true;

            await _db.SaveChangesAsync();

            TempData["PremiumSuccess"] = "Premium успішно активовано!";
            return RedirectToAction(nameof(Premium));
        }

        public async Task<IActionResult> Reviews()
        {
            ViewBag.ActiveMenu = "Reviews";
            var (user, profile) = await GetUserAndProfileAsync();

            var reviews = await _db.Reviews
                .Where(r => r.UserId == user.Id)
                .Include(r => r.Item)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            var vm = new AccountReviewsViewModel
            {
                Profile = profile,
                Reviews = reviews.Select(r => new ReviewVm
                {
                    Id = r.Id,
                    ItemName = r.Item.Name,
                    ItemImageUrl = r.Item.ImageUrl,
                    Rating = r.Rating,
                    Text = r.Comment,
                    CreatedAt = r.CreatedAt
                }).ToList()
            };

            return View(vm);
        }

        public async Task<IActionResult> Messages()
        {
            ViewBag.ActiveMenu = "Messages";
            var (user, profile) = await GetUserAndProfileAsync();

            var messages = await _db.Messages
                .Where(m => m.UserId == user.Id.ToString())
                .OrderByDescending(m => m.DateTime)
                .Select(m => new MessageVm
                {
                    Id = m.Id,
                    Name = m.Name,
                    Content = m.Content,
                    DateTime = m.DateTime
                })
                .ToListAsync();

            var vm = new AccountMessagesViewModel
            {
                Profile = profile,
                Messages = messages
            };

            return View(vm);
        }

        [HttpPost]
        [Route("api/profile/lookedcard/{itemId}")]
        public async Task<IActionResult> MarkAsLooked(int itemId)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
                return Unauthorized();

            var lookedCard = await _db.LookedCards
                .FirstOrDefaultAsync(lc => lc.UserId == user.Id && lc.ItemId == itemId);

            if (lookedCard == null)
            {
                lookedCard = new LookedCard
                {
                    UserId = user.Id,
                    ItemId = itemId,
                    IsLooked = true,
                    ViewCount = 1
                };
                _db.LookedCards.Add(lookedCard);
            }
            else
            {
                lookedCard.IsLooked = true;
                lookedCard.ViewCount++;
                lookedCard.LookedAt = DateTime.UtcNow;
            }

            await _db.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}