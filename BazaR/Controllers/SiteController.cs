using System.Text.Json;
using BazaR.Interfaces;
using BazaR.Models;
using Microsoft.AspNetCore.Mvc;

namespace BazaR.Controllers
{
    public class SiteController : Controller
    {
        private readonly IUserDb _usMan;
        private readonly IItemRepository _itMan;

        private const int PageSize = 12;

        public SiteController(IUserDb usMan, IItemRepository itMan)
        {
            _usMan = usMan;
            _itMan = itMan;
        }

        private int? CurrentUserId => HttpContext.Session.GetInt32("uid");

        private User? CurrentUser =>
            CurrentUserId.HasValue ? _usMan.GetUser(CurrentUserId.Value) : null;

        private bool IsAuthenticated => CurrentUserId.HasValue;

        private bool IsAdmin => CurrentUser?.IsAdmin == true;

        private Dictionary<int, int> GetCart()
        {
            return HttpContext.Session.GetObject<Dictionary<int, int>>("cart")
                   ?? new Dictionary<int, int>();
        }

        private void SaveCart(Dictionary<int, int> cart)
        {
            HttpContext.Session.SetObject("cart", cart);
        }

        private HashSet<int> GetWishlistIds()
        {
            return HttpContext.Session.GetObject<HashSet<int>>("wish")
                   ?? new HashSet<int>();
        }

        private void SaveWishlistIds(HashSet<int> ids)
        {
            HttpContext.Session.SetObject("wish", ids);
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
            var items = _itMan.GetAll()
                .Where(i => i.IsAvailable)
                .Take(24)
                .ToList();

            return View(items);
        }

        [HttpGet]
        public IActionResult Browse(string? query, List<int>? categoryIds, int page = 1)
        {
            if (page < 1) page = 1;

            var items = string.IsNullOrWhiteSpace(query)
                ? _itMan.GetAll()
                : _itMan.Search(query);

            if (categoryIds != null && categoryIds.Count > 0)
                items = items.Where(i => categoryIds.Contains(i.CategoryId)).ToList();

            var total = items.Count;
            var paged = items
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewBag.Query = query ?? "";
            ViewBag.CategoryIds = categoryIds ?? new List<int>();
            ViewBag.Page = page;
            ViewBag.PageSize = PageSize;
            ViewBag.Total = total;

            return View(paged);
        }

        [HttpGet]
        public IActionResult ItemDetails(int id)
        {
            var item = _itMan.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }


        [HttpGet]
        public IActionResult Cart()
        {
            var cart = GetCart();
            var items = cart.Keys
                .Select(id => _itMan.GetById(id))
                .Where(i => i != null)
                .ToList()!;

            ViewBag.Cart = cart;
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int itemId, int quantity = 1)
        {
            if (quantity < 1) quantity = 1;

            var item = _itMan.GetById(itemId);
            if (item == null) return NotFound();

            var cart = GetCart();
            cart.TryGetValue(itemId, out var oldQty);
            cart[itemId] = oldQty + quantity;

            SaveCart(cart);
            TempData["Ok"] = "Добавлено в корзину.";
            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int itemId)
        {
            var cart = GetCart();
            cart.Remove(itemId);
            SaveCart(cart);

            TempData["Ok"] = "Удалено из корзины.";
            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ClearCart()
        {
            SaveCart(new Dictionary<int, int>());
            TempData["Ok"] = "Корзина очищена.";
            return RedirectToAction(nameof(Cart));
        }

        // -------------------------
        // Wishlist
        // -------------------------

        [HttpGet]
        public IActionResult Wishlist()
        {
            var ids = GetWishlistIds();
            var items = ids
                .Select(id => _itMan.GetById(id))
                .Where(i => i != null)
                .ToList()!;

            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToWishlist(int id)
        {
            var item = _itMan.GetById(id);
            if (item == null) return NotFound();

            var ids = GetWishlistIds();
            ids.Add(id);
            SaveWishlistIds(ids);

            TempData["Ok"] = "Добавлено в избранное.";
            return RedirectToAction(nameof(Wishlist));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromWishlist(int id)
        {
            var ids = GetWishlistIds();
            ids.Remove(id);
            SaveWishlistIds(ids);

            TempData["Ok"] = "Удалено из избранного.";
            return RedirectToAction(nameof(Wishlist));
        }


        [HttpGet]
        public IActionResult Checkout()
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(Checkout)));

            var cart = GetCart();
            if (cart.Count == 0)
            {
                TempData["Error"] = "Корзина пустая.";
                return RedirectToAction(nameof(Cart));
            }

            var items = cart.Keys
                .Select(id => _itMan.GetById(id))
                .Where(i => i != null)
                .ToList()!;

            ViewBag.Cart = cart;
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrder(int id, string address, string paymentMethod, string deliveryMethod)
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(Checkout)));

            var userId = CurrentUserId!.Value;

            var cart = GetCart();
            if (cart.Count == 0)
            {
                TempData["Error"] = "Корзина пустая.";
                return RedirectToAction(nameof(Cart));
            }

            var order = new Order
            {
                Status = "Created",
                Address = address,
                PaymentMethod = paymentMethod,
                DeliveryMethod = deliveryMethod,
                CreatedAt = DateTime.UtcNow,
                Items = new List<Item>()
            };

            foreach (var kv in cart)
            {
                var item = _itMan.GetById(kv.Key);
                if (item != null)
                {
                    order.Items.Add(item);
                }
            }

            var ok = _usMan.CreateOrder(userId, order);
            if (!ok)
            {
                TempData["Error"] = "Не удалось создать заказ.";
                return RedirectToAction(nameof(Checkout));
            }

            SaveCart(new Dictionary<int, int>());
            TempData["Ok"] = "Заказ создан!";
            return RedirectToAction(nameof(Orders));
        }

        [HttpGet]
        public IActionResult Orders()
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(Orders)));

            var orders = _usMan.GetUserOrders(CurrentUserId!.Value)
                .OrderByDescending(o => o.Id)
                .ToList();

            return View(orders);
        }

        [HttpGet]
        public IActionResult OrderDetails(int id)
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(OrderDetails), new { id }));

            var order = _usMan.GetOrderById(id);
            if (order == null) return NotFound();

            if (!IsAdmin && order.UserId != CurrentUserId!.Value) return RedirectToAction(nameof(AccessDenied));

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelOrder(int id)
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(Orders)));

            var order = _usMan.GetOrderById(id);
            if (order == null) return NotFound();

            if (!IsAdmin && order.UserId != CurrentUserId!.Value) return RedirectToAction(nameof(AccessDenied));

            var ok = _usMan.CancelOrder(id);
            TempData[ok ? "Ok" : "Error"] = ok ? "Заказ отменён." : "Не удалось отменить заказ.";
            return RedirectToAction(nameof(Orders));
        }

        [HttpGet]
        public IActionResult Profile()
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(Profile)));
            return View(CurrentUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("uid");
            SaveCart(new Dictionary<int, int>());
            SaveWishlistIds(new HashSet<int>());
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
            if (user == null || user.Password != password)
            {
                TempData["Error"] = "Неверный email или пароль.";
                return RedirectToAction(nameof(Index));
            }

            HttpContext.Session.SetInt32("uid", user.Id);
            TempData["Ok"] = "Успешный вход.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(string email, string firstName, string secondName, string password, string phoneNumber)
        {
            email = (email ?? "").Trim();

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(firstName))
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
                FirstName = firstName,
                SecondName = secondName,
                Password = password,
                PhoneNumber = phoneNumber,
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
            var ok = _itMan.Create(item);
            TempData[ok ? "Ok" : "Error"] = ok ? "Товар создан." : "Не удалось создать товар.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateItem(int id, Item item)
        {
            if (!IsAdmin) return RequireAdmin();
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