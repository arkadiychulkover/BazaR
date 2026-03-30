using BazaR.Data;
using BazaR.Filters;
using BazaR.Interfaces;
using BazaR.Models;
using BazaR.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BazaR.Controllers
{
    [ServiceFilter(typeof(BlockResourseFilter))]
    [ServiceFilter(typeof(LoggerActionFilter))]
    [ServiceFilter(typeof(OnlineResourceFilter))]
    public class SiteController : Controller
    {
        private readonly IUserDb _usMan;
        private readonly ILogDb _log;
        private readonly IItemRepository _itMan;
        private readonly AppDbContext _db;
        private readonly UserManager<User> _userManager;

        private const int PageSize = 12;

        public SiteController(IUserDb usMan, IItemRepository itMan, AppDbContext db, UserManager<User> userManager, ILogDb log)
        {
            _usMan = usMan;
            _itMan = itMan;
            _db = db;
            _userManager = userManager;
            _log = log;
        }

        private User? CurrentUser => User.Identity?.IsAuthenticated == true
            ? _userManager.GetUserAsync(User).Result
            : null;

        private bool IsAuthenticated => User.Identity?.IsAuthenticated == true;

        private bool IsAdmin => CurrentUser?.IsAdmin == true;

        private void SetLayoutData()
        {
            ViewBag.User = CurrentUser;

            if (IsAuthenticated && CurrentUser != null)
            {
                var userId = CurrentUser.Id;
                ViewBag.CartCount = _usMan.GetCartItems(userId)?.Count() ?? 0;
                ViewBag.WishlistCount = _usMan.GetWishList(userId)?.Count() ?? 0;
            }
            else
            {
                ViewBag.CartCount = 0;
                ViewBag.WishlistCount = 0;
            }

            ViewBag.Query = HttpContext.Request.Query["query"].ToString();
        }

        private IActionResult RequireLogin(string? returnUrl = null)
        {
            TempData["Error"] = "Нужно войти в аккаунт.";
            return RedirectToAction(nameof(Index), new { returnUrl = returnUrl ?? Url.Action(nameof(Index)) });
        }

        private IActionResult RequireAdmin()
        {
            return RedirectToAction(nameof(AccessDenied));
        }

        [HttpGet]
        public IActionResult Index()
        {
            SetLayoutData();

            var categories = _db.Categories.ToList();

            // Акційні пропозиції (дорогі, доступні)
            var featuredItems = _db.Items
                .Where(i => i.IsAvailable)
                .OrderByDescending(i => i.Price)
                .Take(5)
                .ToList();

            // Зараз шукають (по кількості відгуків)
            var trendingItems = _db.Items
                .Where(i => i.IsAvailable)
                .OrderByDescending(i => i.Reviews.Count)
                .Take(5)
                .ToList();

            // Рекомендації (нові товари)
            var recommendedItems = _db.Items
                .Where(i => i.IsAvailable)
                .OrderByDescending(i => i.Id)
                .Take(5)
                .ToList();

            // Найбільш очікувані (за середнім рейтингом)
            var popularItems = _db.Items
                .Where(i => i.IsAvailable)
                .OrderByDescending(i => i.Reviews.Any() ? i.Reviews.Average(r => r.Rating) : 0)
                .Take(5)
                .ToList();

            ViewBag.Categories = categories;
            ViewBag.FeaturedItems = featuredItems;
            ViewBag.TrendingItems = trendingItems;
            ViewBag.RecommendedItems = recommendedItems;
            ViewBag.PopularItems = popularItems;

            return View();
        }

        [HttpGet]
        public IActionResult Browse(string? query, List<int>? categoryIds, int page = 1,
    string sort = "default", decimal? minPrice = null, decimal? maxPrice = null,
    List<int>? brandIds = null)
        {
            SetLayoutData();
            if (page < 1) page = 1;

            // Получаем все категории с их фильтрами и брендами
            var allCategories = _db.Categories
                .Include(c => c.CategoryBrands)
                    .ThenInclude(cb => cb.Brand)
                .Include(c => c.Filters) // ВАЖНО: загружаем фильтры категорий
                .ToList();

            ViewBag.AllCategories = allCategories;
            ViewBag.MainCategories = allCategories.Where(c => c.ParentCategoryId == null)
                .OrderBy(c => c.DisplayOrder)
                .ToList();

            IQueryable<Item> itemsQuery = _db.Items
                .Include(i => i.Brand)
                .Include(i => i.Category)
                .Include(i => i.Reviews)
                .Include(i => i.Characteristics) // ВАЖНО: загружаем характеристики
                .AsQueryable()
                .AsNoTracking();


            // Фильтр по поисковому запросу
            if (!string.IsNullOrWhiteSpace(query))
                itemsQuery = itemsQuery.Where(i => i.Name.Contains(query) ||
                                                  (i.Desc != null && i.Desc.Contains(query)));

            // Получаем ID всех подкатегорий для выбранных категорий
            var allCategoryIds = new List<int>();
            Category? currentCategory = null;

            if (categoryIds != null && categoryIds.Any())
            {
                foreach (var catId in categoryIds)
                {
                    allCategoryIds.Add(catId);
                    allCategoryIds.AddRange(GetSubCategoryIds(catId));
                }
                allCategoryIds = allCategoryIds.Distinct().ToList();
                itemsQuery = itemsQuery.Where(i => allCategoryIds.Contains(i.CategoryId));

                // Для фильтра брендов в выбранной категории
                currentCategory = allCategories.FirstOrDefault(c => c.Id == categoryIds[0]);
                List<Brand> allBrands = new List<Brand>();
                if (currentCategory != null)
                {
                    ViewBag.CurrentCategory = currentCategory;
                    ViewBag.CategoryPath = GetCategoryPath(categoryIds[0], allCategories);
                    ViewBag.SubCategories = allCategories
                        .Where(c => c.ParentCategoryId == categoryIds[0])
                        .OrderBy(c => c.DisplayOrder)
                        .ToList();

                    // Бренды для текущей категории
                    allBrands = currentCategory.CategoryBrands?
                        .Select(cb => cb.Brand)
                        .OrderBy(b => b.Name)
                        .ToList() ?? new List<Brand>();

                    if (currentCategory.ParentCategory != null) 
                    {
                        allBrands.AddRange(
                        currentCategory.ParentCategory.CategoryBrands
                        .Select(cb => cb.Brand)
                        .OrderBy(b => b.Name)
                        .ToList() ?? new List<Brand>());
                    }

                    //Console.WriteLine(allBrands.Count() + " Brand count ==============================");
                    ViewBag.CategoryBrands = allBrands;
                }
            }

            // Фильтр по цене
            if (minPrice.HasValue)
                itemsQuery = itemsQuery.Where(i => i.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                itemsQuery = itemsQuery.Where(i => i.Price <= maxPrice.Value);

            // Фильтр по брендам
            if (brandIds != null && brandIds.Any())
                itemsQuery = itemsQuery.Where(i => brandIds.Contains(i.BrandId));

            // Собираем динамические фильтры из QueryString
            var selectedFilters = new Dictionary<string, List<string>>();
            foreach (var key in Request.Query.Keys.Where(k => k.StartsWith("filter_")))
            {
                var filterKey = key.Substring("filter_".Length);
                var values = Request.Query[key].ToString().Split(',').ToList();
                selectedFilters[filterKey] = values;

                // Применяем фильтр
                itemsQuery = itemsQuery.Where(i => i.Characteristics
                    .Any(c => c.Key == filterKey && values.Contains(c.Value)));
            }
            ViewBag.SelectedFilters = selectedFilters;

            // Собираем варианты для каждого фильтра категории (из всех товаров в категории, без учета текущих фильтров)
            var filterOptions = new Dictionary<string, List<string>>();

            if (currentCategory != null && currentCategory.Filters != null && currentCategory.Filters.Any())
            {
                // Запрос для получения всех товаров в категории (без фильтров)
                IQueryable<Item> itemsForOptionsQuery = _db.Items
                    .Include(i => i.Characteristics)
                    .AsQueryable();

                if (allCategoryIds.Any())
                {
                    itemsForOptionsQuery = itemsForOptionsQuery.Where(i => allCategoryIds.Contains(i.CategoryId));
                }

                var itemsInCategory = itemsForOptionsQuery.ToList();

                foreach (var filter in currentCategory.Filters)
                {
                    var values = itemsInCategory
                        .SelectMany(i => i.Characteristics.Where(c => c.Key == filter.Key))
                        .Select(c => c.Value)
                        .Where(v => !string.IsNullOrEmpty(v))
                        .Distinct()
                        .OrderBy(v => v)
                        .ToList();

                    filterOptions[filter.Key] = values;
                }
            }
            else
            {
                // Если нет фильтров категории, но есть характеристики товаров, 
                // можем создать динамические фильтры на основе характеристик
                IQueryable<Item> itemsForOptionsQuery = _db.Items
                    .Include(i => i.Characteristics)
                    .AsQueryable();

                if (allCategoryIds.Any())
                {
                    itemsForOptionsQuery = itemsForOptionsQuery.Where(i => allCategoryIds.Contains(i.CategoryId));
                }

                var itemsInCategory = itemsForOptionsQuery.ToList();

                // Группируем характеристики по ключам
                var allCharKeys = itemsInCategory
                    .SelectMany(i => i.Characteristics)
                    .Select(c => c.Key)
                    .Distinct()
                    .ToList();

                foreach (var key in allCharKeys)
                {
                    var values = itemsInCategory
                        .SelectMany(i => i.Characteristics.Where(c => c.Key == key))
                        .Select(c => c.Value)
                        .Where(v => !string.IsNullOrEmpty(v))
                        .Distinct()
                        .OrderBy(v => v)
                        .ToList();

                    if (values.Any())
                    {
                        filterOptions[key] = values;
                    }
                }
            }

            ViewBag.FilterOptions = filterOptions;

            // Применяем сортировку к уже отфильтрованным данным
            var items = itemsQuery.ToList();

            items = sort switch
            {
                "price_asc" => items.OrderBy(i => i.Price).ToList(),
                "price_desc" => items.OrderByDescending(i => i.Price).ToList(),
                "name_asc" => items.OrderBy(i => i.Name).ToList(),
                "name_desc" => items.OrderByDescending(i => i.Name).ToList(),
                "rating_desc" => items
                                    .OrderByDescending(i => i.Reviews
                                        .Select(r => r.Rating)
                                        .DefaultIfEmpty(0)
                                        .Average())
                                    .ToList(),
                "newest" => items.OrderByDescending(i => i.Id).ToList(),
                _ => items.OrderBy(i => i.Id).ToList()
            };

            var total = items.Count;
            var totalPages = (int)Math.Ceiling(total / (double)PageSize);
            var paged = items.Skip((page - 1) * PageSize).Take(PageSize).ToList();

            // Сохраняем параметры для фильтров
            ViewBag.Query = query ?? "";
            ViewBag.CategoryIds = categoryIds ?? new List<int>();
            ViewBag.BrandIds = brandIds ?? new List<int>();
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.Page = page;
            ViewBag.PageSize = PageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalItems = total;
            ViewBag.CurrentSort = sort;

            return View(paged);
        }

        [HttpGet]
        public IActionResult SearchSuggestions(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Json(new List<object>());

            query = query.Trim().ToLower();

            // Получаем все категории
            var categories = _db.Categories
                .Where(c => c.Name.ToLower().Contains(query))
                .Select(c => new { Type = "Category", Id = c.Id, Name = c.Name })
                .ToList();

            // Получаем бренды внутри категорий
            var brands = _db.CategoryBrands
                .Include(cb => cb.Brand)
                .Include(cb => cb.Category)
                .Where(cb => cb.Brand.Name.ToLower().Contains(query))
                .Select(cb => new
                {
                    Type = "Brand",
                    CategoryId = cb.CategoryId,
                    BrandId = cb.BrandId,
                    Name = $"{cb.Category.Name} {cb.Brand.Name}"
                })
                .ToList();

            var results = categories.Cast<object>().Concat(brands.Cast<object>()).ToList();

            return Json(results);
        }

        private List<int> GetSubCategoryIds(int categoryId)
        {
            var ids = new List<int>();
            var subCats = _db.Categories.Where(c => c.ParentCategoryId == categoryId).ToList();
            foreach (var subCat in subCats)
            {
                ids.Add(subCat.Id);
                ids.AddRange(GetSubCategoryIds(subCat.Id));
            }
            return ids;
        }

        private List<Category> GetCategoryPath(int categoryId, List<Category> allCategories)
        {
            var path = new List<Category>();
            var current = allCategories.FirstOrDefault(c => c.Id == categoryId);

            while (current != null)
            {
                path.Insert(0, current);
                current = current.ParentCategoryId.HasValue
                    ? allCategories.FirstOrDefault(c => c.Id == current.ParentCategoryId.Value)
                    : null;
            }

            return path;
        }

        [HttpGet]
        public IActionResult CategoryPage(int category)
        {
            List<int> categorysId = GetSubCategoryIds(category);
            List<Category> categories = new();
            ViewBag.CategotyName = _itMan.GetCategoryById(category).Name;

            foreach (int cat in categorysId)
            {
                categories.Add(_itMan.GetCategoryById(cat));
            }
            return View(categories.Take(12).ToList());
        }

        [HttpGet]
        public IActionResult ItemDetails(int id)
        {
            SetLayoutData();

            var item = _itMan.GetById(id);
            if (item == null) return NotFound();

            var images = item.Colors?.Select(c => c.Color).ToList() ?? new List<string>();
            if (!images.Any() && !string.IsNullOrWhiteSpace(item.ImageUrl))
                images.Add(item.ImageUrl);

            var sameCategoryItems = _itMan.GetByCategory(item.CategoryId)
                .Where(i => i.Id != id)
                .ToList();

            const int recommendedTarget = 20;
            const int sponsoredTarget = 20;
            var parentCatId = item.Category?.ParentCategoryId;
            var recommendedItems = parentCatId.HasValue
                ? _itMan.GetByCategory(parentCatId.Value)
                    .Where(i => i.Id != id && i.CategoryId != item.CategoryId)
                    .Take(recommendedTarget)
                    .ToList()
                : new List<Item>();

            if (recommendedItems.Count < recommendedTarget)
            {
                var needed = recommendedTarget - recommendedItems.Count;
                var extraIds = recommendedItems.Select(r => r.Id).ToHashSet();
                extraIds.Add(id);
                var extra = sameCategoryItems.Where(i => !extraIds.Contains(i.Id)).Take(needed);
                recommendedItems.AddRange(extra);
            }

            if (recommendedItems.Count < recommendedTarget)
            {
                var exclude = recommendedItems.Select(r => r.Id).ToHashSet();
                exclude.Add(id);
                var filler = _itMan.GetAll()
                    .Where(i => !exclude.Contains(i.Id))
                    .Take(recommendedTarget - recommendedItems.Count)
                    .ToList();
                recommendedItems.AddRange(filler);
            }

            var relatedItems = sameCategoryItems.Take(sponsoredTarget).ToList();
            if (relatedItems.Count < sponsoredTarget)
            {
                var excludeRel = relatedItems.Select(i => i.Id).ToHashSet();
                excludeRel.Add(id);
                var fillerRel = _itMan.GetAll()
                    .Where(i => !excludeRel.Contains(i.Id))
                    .Take(sponsoredTarget - relatedItems.Count)
                    .ToList();
                relatedItems.AddRange(fillerRel);
            }

            var bundleItems = item.ComplectItems
                .SelectMany(ci => ci.Complect.Items)
                .Where(ci => ci.ItemId != id)
                .Select(ci => ci.Item)
                .Where(i => i != null)
                .GroupBy(i => i.Id)
                .Select(g => g.First())
                .Take(1)
                .ToList();

            var demoBundleItems = !bundleItems.Any()
                ? sameCategoryItems.Take(1).ToList()
                : new List<Item>();

            var vm = new ViewModels.ItemDetailsViewModel
            {
                Item = item,
                Images = images,
                Category = item.Category,
                Seller = item.User,
                RelatedItems = relatedItems,
                RecommendedItems = recommendedItems,
                BundleItems = bundleItems,
                DemoBundleItems = demoBundleItems,
                IsAuthenticated = IsAuthenticated
            };

            if (IsAuthenticated)
            {
                var userId = CurrentUser!.Id;
                vm.IsInCart = _usMan.GetCartItems(userId).Any(i => i.Id == id);
                vm.IsInWishlist = _usMan.GetWishList(userId).Any(i => i.Id == id);
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult Cart()
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(Cart)));

            SetLayoutData();

            var userId = CurrentUser!.Id;
            var cartItems = _usMan.GetCartItemsWithQuantity(userId);
            var items = cartItems.Select(ci => ci.Item).ToList();

            var totalAmount = cartItems.Sum(ci => ci.Item.Price * ci.Quantity);
            var totalQuantity = cartItems.Sum(ci => ci.Quantity);

            ViewBag.CartItems = cartItems.ToDictionary(ci => ci.ItemId, ci => ci.Quantity);
            ViewBag.TotalAmount = totalAmount;
            ViewBag.TotalQuantity = totalQuantity;

            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int itemId, int quantity = 1)
        {
            if (!IsAuthenticated)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, requireLogin = true, message = "Требуется авторизация" });
                }
                return RequireLogin();
            }

            if (quantity < 1) quantity = 1;

            var item = _itMan.GetById(itemId);
            if (item == null) return NotFound();

            var userId = CurrentUser!.Id;

            for (int i = 0; i < quantity; i++)
            {
                _usMan.AddToCart(userId, itemId);
            }

            var cartItems = _usMan.GetCartItemsWithQuantity(userId);
            var cartCount = cartItems.Sum(ci => ci.Quantity);

            TempData["Ok"] = "Додано в кошик.";

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true, cartCount });
            }

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int itemId)
        {
            if (!IsAuthenticated) return RequireLogin();

            var userId = CurrentUser!.Id;

            _usMan.RemoveFromCart(userId, itemId);

            var cartItems = _usMan.GetCartItemsWithQuantity(userId);
            var cartCount = cartItems.Sum(ci => ci.Quantity);

            TempData["Ok"] = "Видалено з кошика.";

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true, cartCount });
            }

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCartQuantity(int itemId, int quantity)
        {
            if (!IsAuthenticated) return RequireLogin();

            var userId = CurrentUser!.Id;

            _usMan.SetCartQuantity(userId, itemId, quantity);

            var newCartItems = _usMan.GetCartItemsWithQuantity(userId);
            var newCartCount = newCartItems.Sum(ci => ci.Quantity);
            var newTotalAmount = newCartItems.Sum(ci => ci.Item.Price * ci.Quantity);

            return Json(new
            {
                success = true,
                cartCount = newCartCount,
                totalAmount = newTotalAmount
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ClearCart()
        {
            if (!IsAuthenticated) return RequireLogin();

            var userId = CurrentUser!.Id;

            _usMan.ClearCart(userId);

            TempData["Ok"] = "Корзина очищена.";
            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        public IActionResult GetCartJson()
        {
            if (!IsAuthenticated)
                return Json(new { items = new object[0], total = 0, count = 0 });

            var userId = CurrentUser!.Id;
            var cartItems = _usMan.GetCartItemsWithQuantity(userId);

            var result = cartItems.Select(ci => new
            {
                id = ci.ItemId,
                name = ci.Item.Name,
                price = ci.Item.Price,
                quantity = ci.Quantity,
                subtotal = ci.Item.Price * ci.Quantity,
                image = !string.IsNullOrEmpty(ci.Item.ImageUrl)
                    ? ci.Item.ImageUrl
                    : (ci.Item.Colors?.FirstOrDefault()?.Color ?? "/images/items/default.jpg")
            });

            return Json(new
            {
                items = result,
                total = cartItems.Sum(ci => ci.Item.Price * ci.Quantity),
                count = cartItems.Sum(ci => ci.Quantity)
            });
        }

        [HttpGet]
        public IActionResult Wishlist()
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action("Index", "Wishlist"));
            return RedirectToAction("Index", "Wishlist");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToWishlist(int id)
        {
            if (!IsAuthenticated)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, requireLogin = true, message = "Требуется авторизация" });
                }
                return RequireLogin();
            }

            var item = _itMan.GetById(id);
            if (item == null) return NotFound();

            var userId = CurrentUser!.Id;

            // Toggle: if already in wishlist — remove, otherwise add
            var alreadyIn = _usMan.GetWishList(userId).Any(i => i.Id == id);
            bool added;
            if (alreadyIn)
            {
                _usMan.RemoveFromWishList(userId, id);
                added = false;
                TempData["Ok"] = "Видалено з обраного.";
            }
            else
            {
                _usMan.AddToWishList(userId, id);
                added = true;
                TempData["Ok"] = "Додано в обране.";
            }

            var wishlistCount = _usMan.GetWishList(userId).Count();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true, wishlistCount, added });
            }

            return RedirectToAction(nameof(Wishlist));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromWishlist(int id)
        {
            if (!IsAuthenticated) return RequireLogin();

            var userId = CurrentUser!.Id;

            _usMan.RemoveFromWishList(userId, id);

            var wishlistCount = _usMan.GetWishList(userId).Count();

            TempData["Ok"] = "Удалено из избранного.";

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true, wishlistCount });
            }

            return RedirectToAction(nameof(Wishlist));
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(Checkout)));

            SetLayoutData();

            var userId = CurrentUser!.Id;
            var bonus = _db.BonusAccounts.FirstOrDefault(x => x.UserId == userId);

            ViewBag.BonusBalance = bonus?.TotalBalance ?? 0;
            var cartItems = _usMan.GetCartItemsWithQuantity(userId);

            if (!cartItems.Any())
            {
                TempData["Error"] = "Кошик порожній.";
                return RedirectToAction(nameof(Cart));
            }

            var items = cartItems.Select(ci => ci.Item).ToList();
            ViewBag.CartItems = cartItems.ToDictionary(ci => ci.ItemId, ci => ci.Quantity);

            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(
            string address, string paymentMethod, string deliveryMethod,
            string? promoCode,
            string? lastName, string? firstName, string? patronymic, string? phone,
            string? recipientLastName, string? recipientFirstName, string? recipientPatronymic, string? recipientPhone,
            string? bazarPickupPointId, string? postPickupPointId,
            bool saveContacts = false, int bonusToUse = 0)
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(Checkout)));

            var userId = CurrentUser!.Id;

            var cartItems = _usMan.GetCartItemsWithQuantity(userId);
            if (!cartItems.Any())
            {
                TempData["Error"] = "Кошик порожній.";
                return RedirectToAction(nameof(Cart));
            }

            if (string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(firstName)
                || string.IsNullOrWhiteSpace(patronymic) || string.IsNullOrWhiteSpace(phone))
            {
                TempData["Error"] = "Заповніть усі обов'язкові поля в блоці «Ваші контактні дані» (включно з по батькові).";
                return RedirectToAction(nameof(Checkout));
            }

            if (string.IsNullOrWhiteSpace(recipientLastName) || string.IsNullOrWhiteSpace(recipientFirstName)
                || string.IsNullOrWhiteSpace(recipientPhone))
            {
                TempData["Error"] = "Заповніть прізвище, ім'я та телефон отримувача. По батькові в блоці отримувача — необов'язково.";
                return RedirectToAction(nameof(Checkout));
            }

            if (string.IsNullOrWhiteSpace(paymentMethod))
            {
                TempData["Error"] = "Оберіть спосіб оплати.";
                return RedirectToAction(nameof(Checkout));
            }

            if (deliveryMethod == "Самовивіз BAZA-R")
            {
                if (!int.TryParse(bazarPickupPointId, out var bPid) || bPid is < 1 or > 15)
                {
                    TempData["Error"] = "Оберіть місто та точку самовивозу BAZA-R.";
                    return RedirectToAction(nameof(Checkout));
                }

                if (string.IsNullOrWhiteSpace(address) || address.Trim().Length < 10)
                {
                    TempData["Error"] = "Підтвердіть точку самовивозу BAZA-R (натисніть «Підтвердити» у вікні вибору).";
                    return RedirectToAction(nameof(Checkout));
                }
            }
            else if (deliveryMethod == "Відділення пошти")
            {
                if (!int.TryParse(postPickupPointId, out var pPid) || pPid is < 101 or > 210)
                {
                    TempData["Error"] = "Оберіть місто та відділення пошти.";
                    return RedirectToAction(nameof(Checkout));
                }

                if (string.IsNullOrWhiteSpace(address) || address.Trim().Length < 10)
                {
                    TempData["Error"] = "Підтвердіть відділення пошти (натисніть «Підтвердити» у вікні вибору).";
                    return RedirectToAction(nameof(Checkout));
                }
            }
            else if (deliveryMethod == "Кур'єр")
            {
                if (string.IsNullOrWhiteSpace(address) || address.Trim().Length < 8)
                {
                    TempData["Error"] = "Вкажіть повну адресу для доставки кур'єром.";
                    return RedirectToAction(nameof(Checkout));
                }
            }
            else
            {
                TempData["Error"] = "Оберіть спосіб доставки.";
                return RedirectToAction(nameof(Checkout));
            }

            if (saveContacts)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    user.LastName = lastName;
                    user.FirstName = firstName;
                    user.Patronymic = patronymic;
                    user.PhoneNumber = phone;

                    if (!string.IsNullOrWhiteSpace(firstName) || !string.IsNullOrWhiteSpace(lastName))
                        user.Name = $"{firstName} {lastName}".Trim();

                    await _userManager.UpdateAsync(user);
                }
            }

            decimal discount = 0;
            if (!string.IsNullOrWhiteSpace(promoCode))
            {
                discount = GetPromoDiscount(promoCode);
            }

            decimal rawTotal = cartItems.Sum(ci => ci.Item.Price * ci.Quantity);

            var bonusAccount = await _db.BonusAccounts
                .FirstOrDefaultAsync(x => x.UserId == userId);

            int availableBonus = bonusAccount?.TotalBalance ?? 0;

            if (bonusToUse < 0)
                bonusToUse = 0;

            if (bonusToUse > availableBonus)
                bonusToUse = availableBonus;

            decimal bonusDiscount = bonusToUse / 10m;

            decimal finalTotal = rawTotal - discount - bonusDiscount;
            if (finalTotal < 0) finalTotal = 0;

            var addr = (address ?? "").Trim();
            var rp = (recipientPatronymic ?? "").Trim();
            var recipMiddle = string.IsNullOrEmpty(rp) ? "" : " " + rp;
            var recipLine = $"Отримувач: {recipientLastName!.Trim()} {recipientFirstName!.Trim()}{recipMiddle} · {recipientPhone!.Trim()}";
            var fullAddress = string.IsNullOrEmpty(addr) ? recipLine : addr + Environment.NewLine + recipLine;

            var pmEnum = paymentMethod switch
            {
                "Оплатити зараз" => OrderPaymentMethod.PayNow,
                "Оплата при отриманні" => OrderPaymentMethod.PayOnDelivery,
                "Карткою у відділенні" => OrderPaymentMethod.PayByCard,
                _ => OrderPaymentMethod.PayNow
            };

            var dmEnum = deliveryMethod switch
            {
                "Самовивіз BAZA-R" => OrderDeliveryMethod.SelfPickup,
                "Відділення пошти" => OrderDeliveryMethod.NovaPoshta,
                "Кур'єр" => OrderDeliveryMethod.CourierNovaPoshta,
                _ => OrderDeliveryMethod.SelfPickup
            };

            var order = new Order
            {
                OrderItems = new List<OrderItem>(),
                TotalAmount = finalTotal,
                Address = fullAddress,
                PaymentMethod = pmEnum,
                DeliveryMethod = dmEnum,
                CityId = 1
            };

            foreach (var cartItem in cartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ItemId = cartItem.ItemId,
                    Quantity = cartItem.Quantity,
                    PriceAtMoment = cartItem.Item.Price
                });
            }

            var ok = _usMan.CreateOrder(userId, order);
            if (!ok)
            {
                TempData["Error"] = "Не вдалося створити замовлення.";
                return RedirectToAction(nameof(Checkout));
            }

            if (bonusToUse > 0)
            {
                if (bonusAccount == null)
                {
                    bonusAccount = new BonusAccount
                    {
                        UserId = userId,
                        TotalBalance = 0,
                        MonthlyAccrued = 0,
                        MonthlySpent = bonusToUse,
                        AccrualRate = 0.10m,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    _db.BonusAccounts.Add(bonusAccount);
                }
                else
                {
                    bonusAccount.TotalBalance -= bonusToUse;
                    bonusAccount.MonthlySpent += bonusToUse;
                    bonusAccount.UpdatedAt = DateTime.UtcNow;
                }

                if (bonusAccount.TotalBalance < 0)
                    bonusAccount.TotalBalance = 0;
            }

            int bonusToAdd = (int)Math.Floor(finalTotal * 0.10m);

            if (bonusToAdd > 0)
            {
                if (bonusAccount == null)
                {
                    bonusAccount = new BonusAccount
                    {
                        UserId = userId,
                        TotalBalance = bonusToAdd,
                        MonthlyAccrued = bonusToAdd,
                        MonthlySpent = 0,
                        AccrualRate = 0.10m,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    _db.BonusAccounts.Add(bonusAccount);
                }
                else
                {
                    bonusAccount.TotalBalance += bonusToAdd;
                    bonusAccount.MonthlyAccrued += bonusToAdd;
                    bonusAccount.UpdatedAt = DateTime.UtcNow;
                }
            }

            await _db.SaveChangesAsync();

            _usMan.ClearCart(userId);

            if (pmEnum == OrderPaymentMethod.PayNow)
            {
                return RedirectToAction("Payment", new { orderId = order.Id });
            }

            TempData["Ok"] = $"Замовлення успішно оформлено! Використано бонусів: {bonusToUse}. Нараховано бонусів: {bonusToAdd}.";
            return RedirectToAction("Orders", "Profile");
        }

        private decimal GetPromoDiscount(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return 0;

            code = code.Trim();

            var promo = _db.Promotions
                .FirstOrDefault(x => x.Number.ToLower() == code.ToLower());

            return promo?.DiscountAmount ?? 0;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ValidatePromo(string code)
        {
            var discount = GetPromoDiscount(code ?? "");
            if (discount > 0)
                return Json(new { valid = true, discount, message = $"Промокод застосовано: -{discount:N0}₴" });
            return Json(new { valid = false, discount = 0, message = "Промокод недійсний" });
        }

        [HttpGet]
        public IActionResult Payment(int orderId)
        {
            if (!IsAuthenticated) return RequireLogin();
            SetLayoutData();
            var order = _usMan.GetOrderById(orderId);
            if (order == null || order.UserId != CurrentUser!.Id) return NotFound();
            ViewBag.PayerEmail = CurrentUser?.Email ?? "";
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessPayment(int orderId, string pm_pan, string pm_holder, string pm_thru, string pm_sec, bool saveCard = false)
        {
            if (!IsAuthenticated) return RequireLogin();

            var order = _usMan.GetOrderById(orderId);
            if (order == null || order.UserId != CurrentUser!.Id) return NotFound();

            // In real app: integrate with payment gateway here
            // For now: simulate success
            TempData["Ok"] = $"Оплата замовлення №{order.Number} успішно прийнята!";
            return RedirectToAction("Orders", "Profile");
        }
        [HttpGet]
        public async Task<IActionResult> CalculateBonusDiscount(int bonusToUse)
        {
            if (!IsAuthenticated)
            {
                return Json(new
                {
                    ok = false,
                    message = "Користувач не авторизований."
                });
            }

            var userId = CurrentUser!.Id;

            var bonusAccount = await _db.BonusAccounts
                .FirstOrDefaultAsync(x => x.UserId == userId);

            var availableBonus = bonusAccount?.TotalBalance ?? 0;

            if (bonusToUse < 0)
                bonusToUse = 0;

            if (bonusToUse > availableBonus)
                bonusToUse = availableBonus;

            decimal discount = bonusToUse / 10m;

            return Json(new
            {
                ok = true,
                bonusUsed = bonusToUse,
                discount,
                discountText = $"{discount:0.##}₴"
            });
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public List<Item> GetAllItems() => _itMan.GetAll();

        [HttpGet]
        public Item? GetItemById(int id) => _itMan.GetById(id);

        [HttpGet]
        public List<Item> GetItemsByCategory(int categoryId) => _itMan.GetByCategory(categoryId);

        [HttpGet]
        public List<Item> GetItemsByBrand(int brandId) => _itMan.GetByBrand(brandId);

        [HttpGet]
        public List<Item> SearchItems(string query) => _itMan.Search(query);

        [HttpGet]
        public List<Item> FilterItems(int? categoryId, int? brandId, decimal? minPrice, decimal? maxPrice, bool? isAvailable)
            => _itMan.Filter(categoryId, brandId, minPrice, maxPrice, isAvailable);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateItem(Item item)
        {
            if (!IsAdmin) return RequireAdmin();

            SetLayoutData();

            var ok = _itMan.Create(item);
            TempData[ok ? "Ok" : "Error"] = ok ? "Товар создан." : "Не удалось создать товар.";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateItem(int id, Item item)
        {
            if (!IsAdmin) return RequireAdmin();

            SetLayoutData();

            var updated = _itMan.Update(id, item);
            TempData[updated != null ? "Ok" : "Error"] = updated != null ? "Товар обновлён." : "Не удалось обновить товар.";
            return RedirectToAction(nameof(ItemDetails), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItem(int id)
        {
            if (!IsAdmin) return RequireAdmin();

            var ok = _itMan.Delete(id);
            TempData[ok ? "Ok" : "Error"] = ok ? "Товар удалён." : "Не удалось удалить товар.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReview(int itemId, Review review)
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(ItemDetails), new { id = itemId }));

            review.UserId = CurrentUser!.Id;
            review.CreatedAt = DateTime.UtcNow;

            var ok = _itMan.AddReview(itemId, review);
            TempData[ok ? "Ok" : "Error"] = ok ? "Отзыв добавлен." : "Не удалось добавить отзыв.";
            return RedirectToAction(nameof(ItemDetails), new { id = itemId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveReview(int reviewId)
        {
            if (!IsAdmin) return RequireAdmin();

            var ok = _itMan.RemoveReview(reviewId);
            TempData[ok ? "Ok" : "Error"] = ok ? "Отзыв удалён." : "Не удалось удалить отзыв.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCharacteristic(int itemId, ItemCharacteristic characteristic)
        {
            if (!IsAdmin) return RequireAdmin();

            var ok = _itMan.AddCharacteristic(itemId, characteristic);
            TempData[ok ? "Ok" : "Error"] = ok ? "Характеристика добавлена." : "Не удалось добавить характеристику.";
            return RedirectToAction(nameof(ItemDetails), new { id = itemId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveCharacteristic(int characteristicId)
        {
            if (!IsAdmin) return RequireAdmin();

            var ok = _itMan.RemoveCharacteristic(characteristicId);
            TempData[ok ? "Ok" : "Error"] = ok ? "Характеристика удалена." : "Не удалось удалить характеристику.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUsluga(int itemId, Usluga usluga)
        {
            if (!IsAdmin) return RequireAdmin();

            var ok = _itMan.AddUsluga(itemId, usluga);
            TempData[ok ? "Ok" : "Error"] = ok ? "Услуга добавлена." : "Не удалось добавить услугу.";
            return RedirectToAction(nameof(ItemDetails), new { id = itemId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveUsluga(int uslugaId)
        {
            if (!IsAdmin) return RequireAdmin();

            var ok = _itMan.RemoveUsluga(uslugaId);
            TempData[ok ? "Ok" : "Error"] = ok ? "Услуга удалена." : "Не удалось удалить услугу.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDeliveryVariant(int itemId, Delivery delivery)
        {
            if (!IsAdmin) return RequireAdmin();

            var ok = _itMan.AddDeliveryVariant(itemId, delivery);
            TempData[ok ? "Ok" : "Error"] = ok ? "Доставка добавлена." : "Не удалось добавить доставку.";
            return RedirectToAction(nameof(ItemDetails), new { id = itemId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveDeliveryVariant(int deliveryId)
        {
            if (!IsAdmin) return RequireAdmin();

            var ok = _itMan.RemoveDeliveryVariant(deliveryId);
            TempData[ok ? "Ok" : "Error"] = ok ? "Вариант доставки удалён." : "Не удалось удалить доставку.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddCategoriesTest()
        {
            _db.Categories.AddRange(new List<Category>
            {
                new Category { Name = "Телефоны", IconUrl = "/images/cat_phone.png" },
                new Category { Name = "Ноутбуки", IconUrl = "/images/cat_laptop.png" },
                new Category { Name = "Планшеты", IconUrl = "/images/cat_tablet.png" },
                new Category { Name = "Аксессуары", IconUrl = "/images/cat_accessory.png" },
            });
            _db.SaveChanges();
            return Content("OK");
        }
    }

    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            session.SetString(key, json);
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            var json = session.GetString(key);
            if (string.IsNullOrWhiteSpace(json)) return default;
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}