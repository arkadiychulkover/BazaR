using System.Text.Json;
using BazaR.Data;
using BazaR.Interfaces;
using BazaR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BazaR.Controllers
{
    public class SiteController : Controller
    {
        private readonly IUserDb _usMan;
        private readonly IItemRepository _itMan;
        private readonly AppDbContext _db;

        private const int PageSize = 12;

        public SiteController(IUserDb usMan, IItemRepository itMan, AppDbContext db)
        {
            _usMan = usMan;
            _itMan = itMan;
            _db = db;
        }

        private int? CurrentUserId => HttpContext.Session.GetInt32("uid");

        private User? CurrentUser =>
            CurrentUserId.HasValue ? _usMan.GetUser(CurrentUserId.Value) : null;

        private bool IsAuthenticated => CurrentUserId.HasValue;

        private bool IsAdmin => CurrentUser?.IsAdmin == true;

        private void SetLayoutData()
        {
            ViewBag.User = CurrentUser;

            if (IsAuthenticated)
            {
                var userId = CurrentUserId!.Value;
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

            var featuredItems = _db.Items
                .Where(i => i.IsAvailable)
                .OrderByDescending(i => i.Price)
                .Take(3)
                .ToList();

            var newItems = _db.Items
                .Where(i => i.IsAvailable)
                .OrderByDescending(i => i.Id)
                .Take(3)
                .ToList();

            var popularItems = _db.Items
                .Where(i => i.IsAvailable)
                .OrderByDescending(i => i.Reviews.Average(r => r.Rating))
                .Take(3)
                .ToList();

            ViewBag.Categories = categories;
            ViewBag.FeaturedItems = featuredItems;
            ViewBag.NewItems = newItems;
            ViewBag.PopularItems = popularItems;

            return View();
        }

        [HttpGet]
        public IActionResult Browse(string? query, List<int>? categoryIds, int page = 1, string sort = "default")
        {
            SetLayoutData();

            if (page < 1) page = 1;

            // Получаем все категории для сайдбара
            var allCategories = _itMan.GetMainCategories();
            ViewBag.AllCategories = allCategories;

            IQueryable<Item> itemsQuery = _db.Items.AsQueryable();

            // Фильтр по поисковому запросу
            if (!string.IsNullOrWhiteSpace(query))
            {
                itemsQuery = itemsQuery.Where(i => i.Name.Contains(query) ||
                                                  (i.Desc != null && i.Desc.Contains(query)));
            }

            // Фильтр по категориям (включая подкатегории)
            if (categoryIds != null && categoryIds.Any())
            {
                var allCategoryIds = new List<int>();
                foreach (var catId in categoryIds)
                {
                    allCategoryIds.Add(catId);
                    // Добавляем ID всех подкатегорий
                    var subCats = GetSubCategoryIds(catId);
                    allCategoryIds.AddRange(subCats);
                }
                allCategoryIds = allCategoryIds.Distinct().ToList();
                itemsQuery = itemsQuery.Where(i => allCategoryIds.Contains(i.CategoryId));
            }

            var items = itemsQuery.ToList();

            // Сортировка
            items = sort switch
            {
                "price_asc" => items.OrderBy(i => i.Price).ToList(),
                "price_desc" => items.OrderByDescending(i => i.Price).ToList(),
                "name_asc" => items.OrderBy(i => i.Name).ToList(),
                "name_desc" => items.OrderByDescending(i => i.Name).ToList(),
                "rating_desc" => items.OrderByDescending(i => i.Reviews?.Average(r => r.Rating) ?? 0).ToList(),
                "newest" => items.OrderByDescending(i => i.Id).ToList(),
                _ => items.OrderBy(i => i.Id).ToList()
            };

            var total = items.Count;
            var totalPages = (int)Math.Ceiling(total / (double)PageSize);
            var paged = items
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            // Если выбрана конкретная категория, получаем информацию о ней
            if (categoryIds != null && categoryIds.Count == 1)
            {
                var currentCategory = _itMan.GetCategoryById(categoryIds[0]);
                ViewBag.CurrentCategory = currentCategory;

                // Получаем путь к категории (хлебные крошки)
                ViewBag.CategoryPath = _itMan.GetCategoryPath(categoryIds[0]);

                // Получаем подкатегории для фильтра
                ViewBag.SubCategories = _itMan.GetSubCategories(categoryIds[0]);
            }

            ViewBag.Query = query ?? "";
            ViewBag.CategoryIds = categoryIds ?? new List<int>();
            ViewBag.Page = page;
            ViewBag.PageSize = PageSize;
            ViewBag.Total = total;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentSort = sort;

            return View(paged);
        }

        private List<int> GetSubCategoryIds(int categoryId)
        {
            var ids = new List<int>();
            var subCats = _itMan.GetSubCategories(categoryId);
            foreach (var subCat in subCats)
            {
                ids.Add(subCat.Id);
                ids.AddRange(GetSubCategoryIds(subCat.Id));
            }
            return ids;
        }

        [HttpGet]
        public IActionResult ItemDetails(int id)
        {
            SetLayoutData();

            var item = _itMan.GetById(id);
            if (item == null) return NotFound();

            ViewBag.Images = item.Colors?.Select(c => c.Color).ToList() ?? new List<string>();
            ViewBag.Category = item.Category;
            ViewBag.RelatedItems = _itMan.GetByCategory(item.CategoryId)
                .Where(i => i.Id != id)
                .Take(4)
                .ToList();
            ViewBag.Seller = item.User;

            if (IsAuthenticated)
            {
                var userId = CurrentUserId!.Value;
                ViewBag.IsInCart = _usMan.GetCartItems(userId).Any(i => i.Id == id);
                ViewBag.IsInWishlist = _usMan.GetWishList(userId).Any(i => i.Id == id);
            }
            else
            {
                ViewBag.IsInCart = false;
                ViewBag.IsInWishlist = false;
            }

            return View(item);
        }

        [HttpGet]
        public IActionResult Cart()
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(Cart)));

            SetLayoutData();

            var userId = CurrentUserId!.Value;
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

            var userId = CurrentUserId!.Value;

            for (int i = 0; i < quantity; i++)
            {
                _usMan.AddToCart(userId, itemId);
            }

            var cartCount = _usMan.GetCartItems(userId).Count();

            TempData["Ok"] = "Добавлено в корзину.";

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

            var userId = CurrentUserId!.Value;

            _usMan.RemoveFromCart(userId, itemId);

            var cartCount = _usMan.GetCartItems(userId).Count();

            TempData["Ok"] = "Удалено из корзины.";

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

            var userId = CurrentUserId!.Value;

            var cartItems = _usMan.GetCartItemsWithQuantity(userId);
            var currentItem = cartItems.FirstOrDefault(ci => ci.ItemId == itemId);

            if (currentItem != null)
            {
                for (int i = 0; i < currentItem.Quantity; i++)
                {
                    _usMan.RemoveFromCart(userId, itemId);
                }
            }

            for (int i = 0; i < quantity; i++)
            {
                _usMan.AddToCart(userId, itemId);
            }

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

            var userId = CurrentUserId!.Value;

            _usMan.ClearCart(userId);

            TempData["Ok"] = "Корзина очищена.";
            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        public IActionResult Wishlist()
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(Wishlist)));

            SetLayoutData();

            var userId = CurrentUserId!.Value;
            var items = _usMan.GetWishList(userId).ToList();

            return View(items);
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

            var userId = CurrentUserId!.Value;

            _usMan.AddToWishList(userId, id);

            var wishlistCount = _usMan.GetWishList(userId).Count();

            TempData["Ok"] = "Добавлено в избранное.";

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true, wishlistCount });
            }

            return RedirectToAction(nameof(Wishlist));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromWishlist(int id)
        {
            if (!IsAuthenticated) return RequireLogin();

            var userId = CurrentUserId!.Value;

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

            var userId = CurrentUserId!.Value;
            var cartItems = _usMan.GetCartItemsWithQuantity(userId);

            if (!cartItems.Any())
            {
                TempData["Error"] = "Корзина пустая.";
                return RedirectToAction(nameof(Cart));
            }

            var items = cartItems.Select(ci => ci.Item).ToList();
            ViewBag.CartItems = cartItems.ToDictionary(ci => ci.ItemId, ci => ci.Quantity);

            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrder(string address, string paymentMethod, string deliveryMethod)
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(Checkout)));

            var userId = CurrentUserId!.Value;

            var cartItems = _usMan.GetCartItemsWithQuantity(userId);
            if (!cartItems.Any())
            {
                TempData["Error"] = "Корзина пустая.";
                return RedirectToAction(nameof(Cart));
            }

            var order = new Order
            {
                Status = "Created",
                CreatedAt = DateTime.UtcNow,
                OrderItems = new List<OrderItem>(),
                TotalAmount = 0,
                Address = address,
                PaymentMethod = paymentMethod,
                DeliveryMethod = deliveryMethod,
                PaymentStatus = "Pending",
                CityId = 1 // По умолчанию Киев, нужно добавить выбор города
            };

            foreach (var cartItem in cartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ItemId = cartItem.ItemId,
                    Quantity = cartItem.Quantity,
                    PriceAtMoment = cartItem.Item.Price
                });
                order.TotalAmount += cartItem.Item.Price * cartItem.Quantity;
            }

            var ok = _usMan.CreateOrder(userId, order);
            if (!ok)
            {
                TempData["Error"] = "Не удалось создать заказ.";
                return RedirectToAction(nameof(Checkout));
            }

            _usMan.ClearCart(userId);

            TempData["Ok"] = "Заказ создан!";
            return RedirectToAction(nameof(Orders));
        }

        [HttpGet]
        public IActionResult Orders()
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(Orders)));

            SetLayoutData();

            var userId = CurrentUserId!.Value;
            var orders = _usMan.GetUserOrders(userId)
                .OrderByDescending(o => o.Id)
                .ToList();

            ViewBag.OrdersCount = orders.Count;

            return View(orders);
        }

        [HttpGet]
        public IActionResult OrderDetails(int id)
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(OrderDetails), new { id }));

            SetLayoutData();

            var order = _usMan.GetOrderById(id);
            if (order == null) return NotFound();

            if (!IsAdmin && order.UserId != CurrentUserId!.Value)
                return RedirectToAction(nameof(AccessDenied));

            ViewBag.Items = order.OrderItems;
            ViewBag.City = order.City ?? new City { Name = "Киев", Id = 1 };
            ViewBag.User = order.User;

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelOrder(int id)
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(Orders)));

            var order = _usMan.GetOrderById(id);
            if (order == null) return NotFound();

            if (!IsAdmin && order.UserId != CurrentUserId!.Value)
                return RedirectToAction(nameof(AccessDenied));

            var ok = _usMan.CancelOrder(id);
            TempData[ok ? "Ok" : "Error"] = ok ? "Заказ отменён." : "Не удалось отменить заказ.";
            return RedirectToAction(nameof(Orders));
        }

        [HttpGet]
        public IActionResult Profile()
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(Profile)));

            SetLayoutData();

            var user = CurrentUser;
            ViewBag.OrdersCount = user?.Orders?.Count ?? 0;
            ViewBag.WishlistCount = _usMan.GetWishList(CurrentUserId!.Value).Count();
            ViewBag.CartCount = _usMan.GetCartItems(CurrentUserId.Value).Count();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("uid");
            TempData["Ok"] = "Вы вышли из аккаунта.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult OpenLoginModal()
        {
            return PartialView("LoginModal");
        }

        [HttpGet]
        public IActionResult OpenRegisterModal()
        {
            return PartialView("RegisterModal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                TempData["Error"] = "Заполните email и пароль.";
                return RedirectToAction(nameof(Index));
            }

            var user = _usMan.GetByEmail(email.Trim());
            if (user == null || user.PasswordHash != password)
            {
                TempData["Error"] = "Неверный email или пароль.";
                return RedirectToAction(nameof(Index));
            }

            HttpContext.Session.SetInt32("uid", user.Id);
            TempData["Ok"] = "Успешный вход.";

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true, redirect = Url.Action(nameof(Index)) });
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(string email, string name, string password, string phoneNumber)
        {
            email = (email ?? "").Trim();

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(name))
            {
                TempData["Error"] = "Заполните обязательные поля.";
                return RedirectToAction(nameof(Index));
            }

            if (_usMan.GetByEmail(email) != null)
            {
                TempData["Error"] = "Пользователь с таким email уже существует.";
                return RedirectToAction(nameof(Index));
            }

            var user = new User
            {
                Email = email,
                Name = name,
                PasswordHash = password,
                IsAdmin = false
            };

            var ok = _usMan.AddUser(user);
            if (!ok)
            {
                TempData["Error"] = "Не удалось зарегистрироваться.";
                return RedirectToAction(nameof(Index));
            }

            var created = _usMan.GetByEmail(email);
            if (created != null)
                HttpContext.Session.SetInt32("uid", created.Id);

            TempData["Ok"] = "Регистрация успешна.";

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true, redirect = Url.Action(nameof(Index)) });
            }

            return RedirectToAction(nameof(Index));
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

            review.UserId = CurrentUserId!.Value;
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