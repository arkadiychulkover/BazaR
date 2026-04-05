using BazaR.Data;
using BazaR.DTOs;
using BazaR.Interfaces;
using BazaR.Models;
using BazaR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BazaR.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _db;
        private readonly IUserDb _usMan;
        private readonly IMemoryCache _cache;

        public ProfileController(UserManager<User> userManager, AppDbContext db, IUserDb usMan, IMemoryCache cache)
        {
            _userManager = userManager;
            _db = db;
            _usMan = usMan;
            _cache = cache;
        }

        private async Task<(User user, AccountProfileViewModel vm)> GetUserAndProfileAsync()
        {
            var user = await _userManager.Users
                .Include(u => u.OrderRecipients)
                .Include(u => u.DeliveryAddresses)
                .Include(u => u.Pets)
                .Include(u => u.AdditionalInfos)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity!.Name);

            var newMessagesCount = await _db.Messages
                .Where(m => m.UserId == user!.Id && !m.IsRead)
                .CountAsync();

            var vm = new AccountProfileViewModel
            {

                FirstName = user?.FirstName,
                LastName = user?.LastName,
                MiddleName = user?.MiddleName,
                BirthDate = user?.BirthDate,
                Gender = user?.Gender,
                Email = user?.Email ?? string.Empty,
                PhoneNumber = user?.PhoneNumber,
                NewMessagesCount = newMessagesCount,
                OrderRecipients = user?.OrderRecipients ?? new(),
                DeliveryAddresses = user?.DeliveryAddresses ?? new(),
                Pets = user?.Pets ?? new(),
                AdditionalInfos = user?.AdditionalInfos ?? new()
            };
            return (user!, vm);
        }

        private async Task<User> GetCurrentUserAsync() =>
            await _userManager.GetUserAsync(User);

        // ─── Profile ────────────────────────────────────────────────────────────

        public async Task<IActionResult> Profile()
        {
            ViewBag.ActiveMenu = "Profile";
            var (_, profile) = await GetUserAndProfileAsync();
            return View(profile);
        }

        // ─── Orders ─────────────────────────────────────────────────────────────

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
                    Number = o.Number,
                    CreatedAt = o.CreatedAt,
                    Status = o.Status.DisplayStatus(),
                    TotalAmount = o.TotalAmount,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemVm
                    {
                        Name = oi.Item.Name,
                        ImageUrl = oi.Item.ImageUrl,
                        Price = (int)oi.Item.Price,
                        Quantity = oi.Quantity
                    }).ToList()
                }).ToList()
            };

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> OrderDetails(int id)
        {
            ViewBag.ActiveMenu = "Orders";
            var (user, profile) = await GetUserAndProfileAsync();

            var order = await _db.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item)
                        .ThenInclude(i => i.Colors)
                .Include(o => o.City)
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == user.Id);

            if (order == null) return NotFound();

            var vm = new AccountOrderDetailViewModel
            {
                Profile = profile,
                Order = order
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var user = await GetCurrentUserAsync();

            var order = _usMan.GetOrderById(id);
            if (order == null) return NotFound();

            if (order.UserId != user.Id)
                return Forbid();

            var ok = _usMan.CancelOrder(id);
            TempData[ok ? "Ok" : "Error"] = ok ? "Замовлення скасовано." : "Не вдалося скасувати замовлення.";
            return RedirectToAction(nameof(Orders));
        }

        // ─── Wishlist ────────────────────────────────────────────────────────────

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

        // ─── Looked Cards ────────────────────────────────────────────────────────

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

        // ─── Mailings ────────────────────────────────────────────────────────────

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

        // ─── Wallet ──────────────────────────────────────────────────────────────

        public async Task<IActionResult> Wallet()
        {
            ViewBag.ActiveMenu = "Wallet";
            var user = await GetCurrentUserAsync();
            var (_, profile) = await GetUserAndProfileAsync();

            var wallet = await _db.Wallets.FirstOrDefaultAsync(w => w.UserId == user.Id);

            if (wallet == null)
            {
                wallet = new Wallet { UserId = user.Id, Balance = 0, MonthlySpent = 0 };
                _db.Wallets.Add(wallet);
                await _db.SaveChangesAsync();
            }

            var transactions = await _db.WalletTransactions
                .Where(t => t.WalletId == wallet.Id)
                .OrderByDescending(t => t.CreatedAt)
                .Take(50)
                .ToListAsync();

            var vm = new WalletViewModel
            {
                Profile = profile,
                Balance = wallet.Balance,
                MonthlySpent = wallet.MonthlySpent,
                LastReplenishmentAmount = wallet.LastReplenishmentAmount > 0
                    ? wallet.LastReplenishmentAmount : null,
                LastReplenishmentDate = wallet.LastReplenishment,
                Status = wallet.Balance > 0 ? "Активний гаманець" : "Пустий гаманець",
                Transactions = transactions
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TopUpWallet([FromBody] TopUpDto dto)
        {
            if (dto.Amount <= 0 || dto.Amount > 100_000)
                return BadRequest(new { error = "Некоректна сума" });

            var user = await GetCurrentUserAsync();

            var wallet = await _db.Wallets.FirstOrDefaultAsync(w => w.UserId == user.Id);
            if (wallet == null)
            {
                wallet = new Wallet { UserId = user.Id };
                _db.Wallets.Add(wallet);
            }

            wallet.Balance += dto.Amount;
            wallet.LastReplenishmentAmount = dto.Amount;
            wallet.LastReplenishment = DateTime.UtcNow;
            wallet.UpdatedAt = DateTime.UtcNow;

            var transaction = new WalletTransaction
            {
                WalletId = wallet.Id == 0 ? 0 : wallet.Id,
                Type = "replenishment",
                Amount = dto.Amount,
                Description = dto.Method switch
                {
                    "card" => "Поповнення карткою",
                    "crypto" => "Поповнення криптовалютою",
                    "bank" => "Банківський переказ",
                    _ => "Поповнення рахунку"
                },
                CreatedAt = DateTime.UtcNow
            };

            await _db.SaveChangesAsync();

            transaction.WalletId = wallet.Id;
            _db.WalletTransactions.Add(transaction);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                newBalance = wallet.Balance,
                transaction = new
                {
                    transaction.Id,
                    transaction.Type,
                    transaction.Amount,
                    transaction.Description,
                    createdAt = transaction.CreatedAt.ToString("dd.MM.yyyy HH:mm")
                }
            });
        }

        // ─── Promotions ──────────────────────────────────────────────────────────

        [HttpGet]
        public async Task<IActionResult> Promotions()
        {
            ViewBag.ActiveMenu = "Promotions";
            var (_, profile) = await GetUserAndProfileAsync();

            var promotions = await _db.Promotions
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            var vm = new PromotionsViewModel
            {
                Profile = profile,
                Promotions = promotions
            };

            return View(vm);
        }

        // ─── Bonus Account ───────────────────────────────────────────────────────

        public async Task<IActionResult> BonusAccount()
        {
            ViewBag.ActiveMenu = "BonusAccount";
            var user = await GetCurrentUserAsync();
            var (_, profile) = await GetUserAndProfileAsync();

            var bonusAccount = await _db.BonusAccounts.FirstOrDefaultAsync(ba => ba.UserId == user.Id);

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

        // ─── Premium ─────────────────────────────────────────────────────────────

        public async Task<IActionResult> Premium()
        {
            ViewBag.ActiveMenu = "Premium";
            var user = await GetCurrentUserAsync();
            var (_, profile) = await GetUserAndProfileAsync();

            var premiumSub = await _db.PremiumSubscriptions.FirstOrDefaultAsync(p => p.UserId == user.Id);

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
                new() { Name = "Пріоритетна підтримка",      Description = "24/7 підтримка з пріоритетом",                       IsAvailable = premiumSub.IsActive },
                new() { Name = "Ексклюзивні знижки",          Description = "Доступ до спеціальних пропозицій та акцій",           IsAvailable = premiumSub.IsActive },
                new() { Name = "Ранній доступ до новинок",    Description = "Першим дізнавайтесь про нові товари та оновлення",    IsAvailable = premiumSub.IsActive },
                new() { Name = "Безпечна доставка",           Description = "Страхування при доставці",                           IsAvailable = premiumSub.IsActive }
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

            var premiumSub = await _db.PremiumSubscriptions.FirstOrDefaultAsync(p => p.UserId == user.Id);
            var wallet = await _db.Wallets.FirstOrDefaultAsync(w => w.UserId == user.Id);

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

        // ─── Reviews ─────────────────────────────────────────────────────────────

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

        // ─── Messages ────────────────────────────────────────────────────────────

        public async Task<IActionResult> Messages(int page = 1)
        {
            ViewBag.ActiveMenu = "Messages";
            var (user, profile) = await GetUserAndProfileAsync();

            const int pageSize = 4;

            if (page < 1)
                page = 1;

            var totalCount = await _db.Messages
                .AsNoTracking()
                .Where(m => m.UserId == user.Id)
                .CountAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            if (totalPages <= 0)
                totalPages = 1;

            if (page > totalPages)
                page = totalPages;

            var cacheKey = $"user_messages:{user.Id}:page:{page}";

            if (!_cache.TryGetValue(cacheKey, out AccountMessagesViewModel vm))
            {
                var messages = await _db.Messages
                    .AsNoTracking()
                    .Where(m => m.UserId == user.Id)
                    .OrderByDescending(m => m.DateTime)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(m => new MessageVm
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Content = m.Content,
                        SenderName = m.SenderName,
                        DateTime = m.DateTime,
                        IsRead = m.IsRead,
                        SenderId = m.SenderId
                    })
                    .ToListAsync();

                var unreadCount = await _db.Messages
                    .AsNoTracking()
                    .Where(m => m.UserId == user.Id && !m.IsRead)
                    .CountAsync();

                vm = new AccountMessagesViewModel
                {
                    Profile = profile,
                    Messages = messages,
                    NewMessagesCount = unreadCount,
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalPages = totalPages
                };

                _cache.Set(cacheKey, vm, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
                    SlidingExpiration = TimeSpan.FromMinutes(1)
                });
            }
            else
            {
                vm.Profile = profile;
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> MarkMessageAsRead([FromBody] int id)
        {
            var user = await GetCurrentUserAsync();

            var message = await _db.Messages
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == user.Id);

            if (message != null && !message.IsRead)
            {
                message.IsRead = true;
                await _db.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetNewMessagesCount()
        {
            var user = await GetCurrentUserAsync();
            var count = await _db.Messages
                .Where(m => m.UserId == user.Id && !m.IsRead)
                .CountAsync();
            return Json(new { count });
        }

        // ─── Personal Info API ───────────────────────────────────────────────────

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePersonalField([FromBody] UpdateFieldDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            switch (dto.Field)
            {
                case "firstName": user.FirstName = dto.Value; await _userManager.UpdateAsync(user); break;
                case "lastName": user.LastName = dto.Value; await _userManager.UpdateAsync(user); break;
                case "middleName": user.MiddleName = dto.Value; await _userManager.UpdateAsync(user); break;
                case "birthDate": user.BirthDate = DateOnly.TryParse(dto.Value, out var d) ? d : null; await _userManager.UpdateAsync(user); break;
                case "gender": user.Gender = dto.Value; await _userManager.UpdateAsync(user); break;
                case "phoneNumber": await _userManager.SetPhoneNumberAsync(user, dto.Value); break;
                case "email":
                    if (string.IsNullOrWhiteSpace(dto.Value)) return BadRequest("Email не може бути порожнім");
                    var result = await _userManager.SetEmailAsync(user, dto.Value);
                    if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description ?? "Помилка");
                    await _userManager.SetUserNameAsync(user, dto.Value);
                    break;
                default: return BadRequest("Невідоме поле");
            }

            return Ok(new { success = true, value = dto.Value });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRecipient([FromBody] int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var rec = await _db.OrderRecipients.FirstOrDefaultAsync(r => r.Id == id && r.UserId == user!.Id);
            if (rec == null) return NotFound();
            _db.OrderRecipients.Remove(rec);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecipient([FromBody] OrderRecipientDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var recipient = new OrderRecipient
            {
                UserId = user.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                Phone = dto.Phone
            };
            _db.OrderRecipients.Add(recipient);
            await _db.SaveChangesAsync();
            return Ok(new { success = true, id = recipient.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDeliveryAddress([FromBody] DeliveryAddressDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var address = new DeliveryAddress
            {
                UserId = user.Id,
                City = dto.City,
                Street = dto.Street,
                Building = dto.Building,
                Apartment = dto.Apartment,
                PostalCode = dto.PostalCode
            };
            _db.DeliveryAddresses.Add(address);
            await _db.SaveChangesAsync();
            return Ok(new { success = true, id = address.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPet([FromBody] PetDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var pet = new Pet { UserId = user.Id, Name = dto.Name, Type = dto.Type, Breed = dto.Breed };
            _db.Pets.Add(pet);
            await _db.SaveChangesAsync();
            return Ok(new { success = true, id = pet.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePet([FromBody] int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var pet = await _db.Pets.FirstOrDefaultAsync(p => p.Id == id && p.UserId == user!.Id);
            if (pet == null) return NotFound();
            _db.Pets.Remove(pet);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAdditionalInfo([FromBody] AdditionalInfo dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var existing = await _db.AdditionalInfos.FirstOrDefaultAsync(a => a.Id == dto.Id && a.UserId == user.Id);
            if (existing == null) { dto.UserId = user.Id; _db.AdditionalInfos.Add(dto); }
            else { existing.Key = dto.Key; existing.Value = dto.Value; }
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAdditionalInfo([FromBody] int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var info = await _db.AdditionalInfos.FirstOrDefaultAsync(a => a.Id == id && a.UserId == user!.Id);
            if (info == null) return NotFound();
            _db.AdditionalInfos.Remove(info);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}