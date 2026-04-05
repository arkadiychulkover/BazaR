using BazaR.Data;
using BazaR.Filters;
using BazaR.Interfaces;
using BazaR.Models;
using BazaR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace BazaR.Controllers
{
    [ServiceFilter(typeof(BlockResourseFilter))]
    [ServiceFilter(typeof(LoggerActionFilter))]
    [ServiceFilter(typeof(OnlineResourceFilter))]
    public class SiteController : Controller
    {
        #region Private Fields

        private readonly IUserDb _usMan;
        private readonly ILogDb _log;
        private readonly IItemRepository _itMan;
        private readonly AppDbContext _db;
        private readonly UserManager<User> _userManager;
        private const int PageSize = 12;
        private readonly IMemoryCache _cache;

        #endregion

        #region Constructor

        public SiteController(
            IUserDb usMan,
            IItemRepository itMan,
            AppDbContext db,
            UserManager<User> userManager,
            ILogDb log,
            IMemoryCache cache)
        {
            _usMan = usMan;
            _itMan = itMan;
            _db = db;
            _userManager = userManager;
            _log = log;
            _cache = cache;
        }

        #endregion

        #region Private Helpers

        private async Task SetLayoutDataAsync()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(User);
                ViewBag.User = user;

                if (user != null)
                {
                    var userId = user.Id;
                    ViewBag.CartCount = _usMan.GetCartItems(userId)?.Count() ?? 0;
                    ViewBag.WishlistCount = _usMan.GetWishList(userId)?.Count() ?? 0;
                }
                else
                {
                    ViewBag.CartCount = 0;
                    ViewBag.WishlistCount = 0;
                }
            }
            else
            {
                ViewBag.User = null;
                ViewBag.CartCount = 0;
                ViewBag.WishlistCount = 0;
            }

            ViewBag.Query = HttpContext.Request.Query["query"].ToString();
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

        private decimal GetPromoDiscount(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return 0;

            code = code.Trim();

            var promo = _db.Promotions.FirstOrDefault(x => x.Number.ToLower() == code.ToLower());

            return promo?.DiscountAmount ?? 0;
        }

        #endregion

        #region Public Pages

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await SetLayoutDataAsync();

            var categories = _db.Categories.ToList();

            var featuredItems = _db.Items
                .Where(i => i.IsAvailable)
                .OrderByDescending(i => i.Price)
                .Take(5)
                .ToList();

            var trendingItems = _db.Items
                .Where(i => i.IsAvailable)
                .OrderByDescending(i => i.Reviews.Count)
                .Take(5)
                .ToList();

            var recommendedItems = _db.Items
                .Where(i => i.IsAvailable)
                .OrderByDescending(i => i.Id)
                .Take(5)
                .ToList();

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
        public async Task<IActionResult> Browse(
    string? query,
    List<int>? categoryIds,
    int page = 1,
    string sort = "default",
    decimal? minPrice = null,
    decimal? maxPrice = null,
    List<int>? brandIds = null,
    bool? readyToSend = null,
    List<int>? sellerTypes = null,
    bool? noPercentCredit = null,
    List<int>? countries = null)
        {
            await SetLayoutDataAsync();

            if (page < 1)
                page = 1;

            var allCategories = _db.Categories
                .Include(c => c.CategoryBrands)
                    .ThenInclude(cb => cb.Brand)
                .Include(c => c.Filters)
                .ToList();

            ViewBag.AllCategories = allCategories;
            ViewBag.MainCategories = allCategories
                .Where(c => c.ParentCategoryId == null)
                .OrderBy(c => c.DisplayOrder)
                .ToList();

            IQueryable<Item> itemsQuery = _db.Items
                .Include(i => i.Brand)
                .Include(i => i.Category)
                .Include(i => i.Reviews)
                .Include(i => i.Characteristics)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                itemsQuery = itemsQuery.Where(i =>
                    i.Name.Contains(query) ||
                    (i.Desc != null && i.Desc.Contains(query)));
            }

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

                currentCategory = allCategories.FirstOrDefault(c => c.Id == categoryIds[0]);
                var allBrands = new List<Brand>();

                if (currentCategory != null)
                {
                    ViewBag.CurrentCategory = currentCategory;
                    ViewBag.CategoryPath = GetCategoryPath(categoryIds[0], allCategories);
                    ViewBag.SubCategories = allCategories
                        .Where(c => c.ParentCategoryId == categoryIds[0])
                        .OrderBy(c => c.DisplayOrder)
                        .ToList();

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
                                .ToList());
                    }

                    ViewBag.CategoryBrands = allBrands
                        .GroupBy(b => b.Id)
                        .Select(g => g.First())
                        .OrderBy(b => b.Name)
                        .ToList();
                }
            }

            if (minPrice.HasValue)
                itemsQuery = itemsQuery.Where(i => i.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                itemsQuery = itemsQuery.Where(i => i.Price <= maxPrice.Value);

            if (brandIds != null && brandIds.Any())
                itemsQuery = itemsQuery.Where(i => brandIds.Contains(i.BrandId));

            if (readyToSend == true)
                itemsQuery = itemsQuery.Where(i => i.IsReadyToSend);

            if (sellerTypes != null && sellerTypes.Any())
                itemsQuery = itemsQuery.Where(i => sellerTypes.Contains((int)i.SellerType));

            if (noPercentCredit == true)
                itemsQuery = itemsQuery.Where(i => i.IsNoPercentCredit);

            if (countries != null && countries.Any())
                itemsQuery = itemsQuery.Where(i => countries.Contains((int)i.Country));

            var selectedFilters = new Dictionary<string, List<string>>();

            foreach (var key in Request.Query.Keys.Where(k => k.StartsWith("filter_")))
            {
                var filterKey = key.Substring("filter_".Length);
                var values = Request.Query[key]
                    .ToString()
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                selectedFilters[filterKey] = values;

                itemsQuery = itemsQuery.Where(i => i.Characteristics.Any(c =>
                    c.Key == filterKey && values.Contains(c.Value)));
            }

            ViewBag.SelectedFilters = selectedFilters;

            ViewBag.ReadyToSend = readyToSend;
            ViewBag.SellerTypes = sellerTypes ?? new List<int>();
            ViewBag.NoPercentCredit = noPercentCredit;
            ViewBag.Countries = countries ?? new List<int>();

            var filterOptions = new Dictionary<string, List<string>>();

            if (currentCategory != null && currentCategory.Filters != null && currentCategory.Filters.Any())
            {
                IQueryable<Item> itemsForOptionsQuery = _db.Items
                    .Include(i => i.Characteristics)
                    .AsQueryable();

                if (allCategoryIds.Any())
                    itemsForOptionsQuery = itemsForOptionsQuery.Where(i => allCategoryIds.Contains(i.CategoryId));

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
                IQueryable<Item> itemsForOptionsQuery = _db.Items
                    .Include(i => i.Characteristics)
                    .AsQueryable();

                if (allCategoryIds.Any())
                    itemsForOptionsQuery = itemsForOptionsQuery.Where(i => allCategoryIds.Contains(i.CategoryId));

                var itemsInCategory = itemsForOptionsQuery.ToList();

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
                        filterOptions[key] = values;
                }
            }

            ViewBag.FilterOptions = filterOptions;

            var cacheKey = $"browse:{query}:{string.Join("-", categoryIds ?? new())}:{page}:{sort}:{minPrice}:{maxPrice}:{string.Join("-", brandIds ?? new())}:{readyToSend}:{string.Join("-", sellerTypes ?? new())}:{noPercentCredit}:{string.Join("-", countries ?? new())}:{Request.QueryString}";

            if (!_cache.TryGetValue(cacheKey, out List<Item> items))
            {
                items = itemsQuery.ToList();

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
                    "ready_to_send" => items.OrderByDescending(i => i.IsReadyToSend).ThenBy(i => i.Id).ToList(),
                    "seller_type" => items.OrderBy(i => i.SellerType).ThenBy(i => i.Name).ToList(),
                    "no_percent_credit" => items.OrderByDescending(i => i.IsNoPercentCredit).ThenBy(i => i.Id).ToList(),
                    "country" => items.OrderBy(i => i.Country.ToString()).ThenBy(i => i.Name).ToList(),
                    _ => items.OrderBy(i => i.Id).ToList()
                };

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                _cache.Set(cacheKey, items, cacheOptions);
            }

            var total = items.Count;
            var totalPages = (int)Math.Ceiling(total / (double)PageSize);
            var paged = items.Skip((page - 1) * PageSize).Take(PageSize).ToList();

            ViewBag.Query = query ?? string.Empty;
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

            var categories = _db.Categories
                .Where(c => c.Name.ToLower().Contains(query))
                .Select(c => new { Type = "Category", Id = c.Id, Name = c.Name })
                .ToList();

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

        [HttpGet]
        public IActionResult CategoryPage(int category)
        {
            var categoryIds = GetSubCategoryIds(category);
            var categories = new List<Category>();

            ViewBag.CategotyName = _itMan.GetCategoryById(category).Name;

            foreach (var cat in categoryIds)
            {
                categories.Add(_itMan.GetCategoryById(cat));
            }

            return View(categories.Take(12).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> ItemDetails(int id)
        {
            await SetLayoutDataAsync();

            var item = _itMan.GetById(id);
            if (item == null)
                return NotFound();

            var images = new List<string>();

            if (!string.IsNullOrWhiteSpace(item.ImageUrl))
            {
                string? rightUrl = null;

                if (!item.ImageUrl.StartsWith("/images"))
                {
                    var wwwrootIndex = item.ImageUrl.IndexOf("wwwroot", StringComparison.OrdinalIgnoreCase);
                    if (wwwrootIndex >= 0)
                    {
                        rightUrl = item.ImageUrl
                            .Substring(wwwrootIndex + "wwwroot".Length)
                            .Replace("\\", "/");
                    }
                }

                images.Add(rightUrl ?? item.ImageUrl);
            }

            if (!images.Any() && item.Colors != null)
            {
                images.AddRange(item.Colors
                    .Select(c => c.Color)
                    .Where(c => !string.IsNullOrWhiteSpace(c)));
            }

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

                var extra = sameCategoryItems
                    .Where(i => !extraIds.Contains(i.Id))
                    .Take(needed);

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
                .GroupBy(i => i!.Id)
                .Select(g => g.First()!)
                .Take(1)
                .ToList();

            var demoBundleItems = !bundleItems.Any()
                ? sameCategoryItems.Take(1).ToList()
                : new List<Item>();

            var isAuthenticated = User.Identity?.IsAuthenticated == true;

            var vm = new ItemDetailsViewModel
            {
                Item = item,
                Images = images,
                Category = item.Category,
                Seller = item.User,
                RelatedItems = relatedItems,
                RecommendedItems = recommendedItems,
                BundleItems = bundleItems,
                DemoBundleItems = demoBundleItems,
                IsAuthenticated = isAuthenticated
            };

            if (isAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    var userId = user.Id;
                    vm.IsInCart = _usMan.GetCartItems(userId).Any(i => i.Id == id);
                    vm.IsInWishlist = _usMan.GetWishList(userId).Any(i => i.Id == id);
                }
            }

            return View(vm);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Cart()
        {
            await SetLayoutDataAsync();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var userId = user.Id;
            var cartItems = _usMan.GetCartItemsWithQuantity(userId);
            var items = cartItems.Select(ci => ci.Item).ToList();

            ViewBag.CartItems = cartItems.ToDictionary(ci => ci.ItemId, ci => ci.Quantity);
            ViewBag.TotalAmount = cartItems.Sum(ci => ci.Item.Price * ci.Quantity);
            ViewBag.TotalQuantity = cartItems.Sum(ci => ci.Quantity);

            return View(items);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Wishlist()
        {
            return RedirectToAction("Index", "Wishlist");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            await SetLayoutDataAsync();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var userId = user.Id;
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
        public IActionResult Privacy()
        {
            return View();
        }

        #endregion

        #region Cart Actions

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddToCart(int itemId, int quantity = 1)
        {
            if (quantity < 1)
                quantity = 1;

            var item = _itMan.GetById(itemId);
            if (item == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new
                    {
                        success = false,
                        requireLogin = true,
                        message = "Требуется авторизация"
                    });
                }

                return Challenge();
            }

            var userId = user.Id;

            for (var i = 0; i < quantity; i++)
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
        [Authorize]
        public async Task<IActionResult> RemoveFromCart(int itemId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var userId = user.Id;

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
        [Authorize]
        public async Task<IActionResult> UpdateCartQuantity(int itemId, int quantity)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var userId = user.Id;

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
        [Authorize]
        public async Task<IActionResult> ClearCart()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var userId = user.Id;

            _usMan.ClearCart(userId);

            TempData["Ok"] = "Корзина очищена.";
            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCartJson()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new
                {
                    items = Array.Empty<object>(),
                    total = 0,
                    count = 0
                });
            }

            var userId = user.Id;
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

        #endregion

        #region Wishlist Actions

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddToWishlist(int id)
        {
            var item = _itMan.GetById(id);
            if (item == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new
                    {
                        success = false,
                        requireLogin = true,
                        message = "Требуется авторизация"
                    });
                }

                return Challenge();
            }

            var userId = user.Id;

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
        [Authorize]
        public async Task<IActionResult> RemoveFromWishlist(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var userId = user.Id;

            _usMan.RemoveFromWishList(userId, id);

            var wishlistCount = _usMan.GetWishList(userId).Count();

            TempData["Ok"] = "Удалено из избранного.";

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true, wishlistCount });
            }

            return RedirectToAction(nameof(Wishlist));
        }

        #endregion

        #region Order Actions

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CreateOrder(
            string address,
            string paymentMethod,
            string deliveryMethod,
            string? promoCode,
            string? lastName,
            string? firstName,
            string? patronymic,
            string? phone,
            string? recipientLastName,
            string? recipientFirstName,
            string? recipientPatronymic,
            string? recipientPhone,
            string? bazarPickupPointId,
            string? postPickupPointId,
            bool saveContacts = false,
            int bonusToUse = 0)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var userId = user.Id;
            var cartItems = _usMan.GetCartItemsWithQuantity(userId);

            if (!cartItems.Any())
            {
                TempData["Error"] = "Кошик порожній.";
                return RedirectToAction(nameof(Cart));
            }

            if (string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(patronymic) ||
                string.IsNullOrWhiteSpace(phone))
            {
                TempData["Error"] = "Заповніть усі обов'язкові поля в блоці «Ваші контактні дані» (включно з по батькові).";
                return RedirectToAction(nameof(Checkout));
            }

            if (string.IsNullOrWhiteSpace(recipientLastName) ||
                string.IsNullOrWhiteSpace(recipientFirstName) ||
                string.IsNullOrWhiteSpace(recipientPhone))
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
                user.LastName = lastName;
                user.FirstName = firstName;
                user.Patronymic = patronymic;
                user.PhoneNumber = phone;

                if (!string.IsNullOrWhiteSpace(firstName) || !string.IsNullOrWhiteSpace(lastName))
                    user.Name = $"{firstName} {lastName}".Trim();

                await _userManager.UpdateAsync(user);
            }

            decimal discount = 0;

            if (!string.IsNullOrWhiteSpace(promoCode))
                discount = GetPromoDiscount(promoCode);

            var rawTotal = cartItems.Sum(ci => ci.Item.Price * ci.Quantity);

            var bonusAccount = await _db.BonusAccounts
                .FirstOrDefaultAsync(x => x.UserId == userId);

            var availableBonus = bonusAccount?.TotalBalance ?? 0;

            if (bonusToUse < 0)
                bonusToUse = 0;

            if (bonusToUse > availableBonus)
                bonusToUse = availableBonus;

            decimal bonusDiscount = bonusToUse / 10m;
            decimal finalTotal = rawTotal - discount - bonusDiscount;

            if (finalTotal < 0)
                finalTotal = 0;

            var addr = (address ?? string.Empty).Trim();
            var rp = (recipientPatronymic ?? string.Empty).Trim();
            var recipMiddle = string.IsNullOrEmpty(rp) ? string.Empty : " " + rp;
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

            var bonusToAdd = (int)Math.Floor(finalTotal * 0.10m);

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

            _cache.Remove($"UserStatistic_{userId}");
            _cache.Remove($"Orders_{userId}");

            if (pmEnum == OrderPaymentMethod.PayNow)
                return RedirectToAction(nameof(Payment), new { orderId = order.Id });

            TempData["Ok"] = $"Замовлення успішно оформлено! Використано бонусів: {bonusToUse}. Нараховано бонусів: {bonusToAdd}.";
            return RedirectToAction("Orders", "Profile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ValidatePromo(string code)
        {
            var discount = GetPromoDiscount(code ?? string.Empty);

            if (discount > 0)
            {
                return Json(new
                {
                    valid = true,
                    discount,
                    message = $"Промокод застосовано: -{discount:N0}₴"
                });
            }

            return Json(new
            {
                valid = false,
                discount = 0,
                message = "Промокод недійсний"
            });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Payment(int orderId)
        {
            await SetLayoutDataAsync();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var order = _usMan.GetOrderById(orderId);
            if (order == null || order.UserId != user.Id)
                return NotFound();

            ViewBag.PayerEmail = user.Email ?? string.Empty;
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ProcessPayment(
            int orderId,
            string pm_pan,
            string pm_holder,
            string pm_thru,
            string pm_sec,
            bool saveCard = false)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var order = _usMan.GetOrderById(orderId);
            if (order == null || order.UserId != user.Id)
                return NotFound();

            TempData["Ok"] = $"Оплата замовлення №{order.Number} успішно прийнята!";
            return RedirectToAction("Orders", "Profile");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CalculateBonusDiscount(int bonusToUse)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Json(new
                {
                    ok = false,
                    message = "Користувач не авторизований."
                });
            }

            var userId = user.Id;

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

        #endregion

        #region Admin Actions

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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CreateItemUser()
        {
            User us = await _userManager.GetUserAsync(User);

            ViewBag.Categories = _db.Categories.ToList();
            ViewBag.Brands = _db.Brands.ToList();
            ViewBag.UserItems = _db.Items.Where(i => i.UserId == us.Id).ToList();

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateItemUser(
            Item model,
            List<ItemCharacteristic> Characteristics,
            List<ItemColor> Colors,
            List<Usluga> SelectedUslugs,
            IFormFile imageFile,
            string ComplectName,
            List<int> SelectedItemIds
        )
        {
            if (model == null || imageFile == null)
                return RedirectToAction(nameof(CreateItemUser));

            User us = await _userManager.GetUserAsync(User);

            string filename = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/items", filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            model.Characteristics = null;
            model.Colors = null;
            model.Uslugi = null;
            model.UserId = us.Id;
            model.ImageUrl = "/images/items/" + filename;

            _db.Items.Add(model);
            await _db.SaveChangesAsync();

            if (Characteristics != null && Characteristics.Any())
            {
                foreach (var charac in Characteristics.Where(c => !string.IsNullOrEmpty(c.Key)))
                {
                    _db.ItemCharacteristics.Add(new ItemCharacteristic
                    {
                        Key = charac.Key,
                        Value = charac.Value,
                        ItemId = model.Id
                    });
                }
            }

            if (Colors != null && Colors.Any())
            {
                foreach (var color in Colors.Where(c => !string.IsNullOrEmpty(c.Color)))
                {
                    _db.ItemColors.Add(new ItemColor
                    {
                        Color = color.Color,
                        ItemId = model.Id
                    });
                }
            }

            if (SelectedUslugs != null && SelectedUslugs.Any())
            {
                foreach (var usl in SelectedUslugs.Where(u => !string.IsNullOrEmpty(u.Name)))
                {
                    _db.Uslugi.Add(new Usluga
                    {
                        Name = usl.Name,
                        Price = usl.Price,
                        Description = usl.Description ?? $"Услуга для товара {model.Name}",
                        ItemId = model.Id
                    });
                }
            }

            if (!string.IsNullOrEmpty(ComplectName) && SelectedItemIds != null && SelectedItemIds.Any())
            {
                var complect = new Complect
                {
                    Name = ComplectName
                };

                _db.Complects.Add(complect);
                await _db.SaveChangesAsync();

                foreach (var itemId in SelectedItemIds)
                {
                    _db.ComplectItems.Add(new ComplectItem
                    {
                        ComplectId = complect.Id,
                        ItemId = itemId
                    });
                }

                _db.ComplectItems.Add(new ComplectItem
                {
                    ComplectId = complect.Id,
                    ItemId = model.Id
                });
            }

            await _db.SaveChangesAsync();
            _cache.Remove("browse:*");
            _cache.Remove($"UserStatistic_{us.Id}");
            _cache.Remove($"Items_{us.Id}");
            return RedirectToAction(nameof(ItemDetails), new { id = model.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateItem(Item item)
        {
            await SetLayoutDataAsync();

            var ok = _itMan.Create(item);
            TempData[ok ? "Ok" : "Error"] = ok ? "Товар создан." : "Не удалось создать товар.";
            _cache.Remove("browse:*");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateItem(int id, Item item)
        {
            await SetLayoutDataAsync();

            var updated = _itMan.Update(id, item);
            TempData[updated != null ? "Ok" : "Error"] = updated != null ? "Товар обновлён." : "Не удалось обновить товар.";
            _cache.Remove("browse:*");
            _cache.Remove($"Item_{id}");
            return RedirectToAction(nameof(ItemDetails), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteItem(int id)
        {
            var userId = _db.Items.FirstOrDefault(i => i.Id == id)?.UserId;
            var ok = _itMan.Delete(id);
            TempData[ok ? "Ok" : "Error"] = ok ? "Товар удалён." : "Не удалось удалить товар.";
            _cache.Remove("browse:*");
            _cache.Remove($"UserStatistic_{userId}");
            _cache.Remove($"Item_{id}");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddReview(int itemId, Review review)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            review.UserId = user.Id;
            review.CreatedAt = DateTime.UtcNow;

            var ok = _itMan.AddReview(itemId, review);
            TempData[ok ? "Ok" : "Error"] = ok ? "Отзыв добавлен." : "Не удалось добавить отзыв.";
            _cache.Remove($"Item_{itemId}");
            _cache.Remove($"UserStatistic_{user.Id}");
            return RedirectToAction(nameof(ItemDetails), new { id = itemId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveReview(int reviewId)
        {
            var review = _db.Reviews.FirstOrDefault(r => r.Id == reviewId);
            var itemId = review?.ItemId;
            var userId = review?.UserId;
            var ok = _itMan.RemoveReview(reviewId);
            TempData[ok ? "Ok" : "Error"] = ok ? "Отзыв удалён." : "Не удалось удалить отзыв.";
            _cache.Remove($"Item_{itemId}");
            _cache.Remove($"UserStatistic_{userId}");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCharacteristic(int itemId, ItemCharacteristic characteristic)
        {
            var ok = _itMan.AddCharacteristic(itemId, characteristic);
            TempData[ok ? "Ok" : "Error"] = ok ? "Характеристика добавлена." : "Не удалось добавить характеристику.";
            _cache.Remove($"Item_{itemId}");
            return RedirectToAction(nameof(ItemDetails), new { id = itemId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveCharacteristic(int characteristicId)
        {
            var charac = _db.ItemCharacteristics.FirstOrDefault(c => c.Id == characteristicId);
            var itemId = charac?.ItemId;
            var ok = _itMan.RemoveCharacteristic(characteristicId);
            TempData[ok ? "Ok" : "Error"] = ok ? "Характеристика удалена." : "Не удалось удалить характеристику.";
            _cache.Remove($"Item_{itemId}");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult AddUsluga(int itemId, Usluga usluga)
        {
            var ok = _itMan.AddUsluga(itemId, usluga);
            TempData[ok ? "Ok" : "Error"] = ok ? "Услуга добавлена." : "Не удалось добавить услугу.";
            _cache.Remove($"Item_{itemId}");
            return RedirectToAction(nameof(ItemDetails), new { id = itemId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveUsluga(int uslugaId)
        {
            var usluga = _db.Uslugi.FirstOrDefault(u => u.Id == uslugaId);
            var itemId = usluga?.ItemId;
            var ok = _itMan.RemoveUsluga(uslugaId);
            TempData[ok ? "Ok" : "Error"] = ok ? "Услуга удалена." : "Не удалось удалить услугу.";
            _cache.Remove($"Item_{itemId}");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult AddDeliveryVariant(int itemId, Delivery delivery)
        {
            var ok = _itMan.AddDeliveryVariant(itemId, delivery);
            TempData[ok ? "Ok" : "Error"] = ok ? "Доставка добавлена." : "Не удалось добавить доставку.";
            _cache.Remove($"Item_{itemId}");
            return RedirectToAction(nameof(ItemDetails), new { id = itemId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveDeliveryVariant(int deliveryId)
        {
            var delivery = _db.Deliveries.FirstOrDefault(d => d.Id == deliveryId);
            var itemId = delivery?.ItemId;
            var ok = _itMan.RemoveDeliveryVariant(deliveryId);
            TempData[ok ? "Ok" : "Error"] = ok ? "Вариант доставки удалён." : "Не удалось удалить доставку.";
            _cache.Remove($"Item_{itemId}");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult AddCategoriesTest()
        {
            _db.Categories.AddRange(new List<Category>
            {
                new Category { Name = "Телефоны", IconUrl = "/images/cat_phone.png" },
                new Category { Name = "Ноутбуки", IconUrl = "/images/cat_laptop.png" },
                new Category { Name = "Планшеты", IconUrl = "/images/cat_tablet.png" },
                new Category { Name = "Аксессуары", IconUrl = "/images/cat_accessory.png" }
            });

            _db.SaveChanges();
            _cache.Remove("browse:*");
            return Content("OK");
        }

        #endregion
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

            if (string.IsNullOrWhiteSpace(json))
                return default;

            return JsonSerializer.Deserialize<T>(json);
        }
    }
}