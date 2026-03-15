using BazaR.Interfaces;
using BazaR.Models;
using BazaR.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BazaR.Controllers
{
    public class SiteController : Controller
    {
        private readonly IUserDb _usMan;
        private readonly IItemRepository _itMan;
        private readonly UserManager<User> _userManager;

        private const int PageSize = 12;

        public SiteController(IUserDb usMan, IItemRepository itMan, UserManager<User> userManager)
        {
            _usMan = usMan;
            _itMan = itMan;
            _userManager = userManager;
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

            var items = _itMan.GetAll();

            HashSet<int> wishlistIds = new();
            if (IsAuthenticated && CurrentUser != null)
            {
                var userId = CurrentUser.Id;
                wishlistIds = _usMan.GetWishList(userId)
                                    .Select(i => i.Id)
                                    .ToHashSet();
            }

            var model = items.Select(x => new ItemCardVm
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                OldPrice = null,
                ImageUrl = x.ImageUrl,
                InWishlist = wishlistIds.Contains(x.Id)
            }).ToList();

            return View(model);
        }


        [HttpGet]
        public IActionResult Browse(string? query, List<int>? categoryIds, int page = 1, string sort = "default")
        {
            SetLayoutData();
            if (page < 1) page = 1;

            var items = string.IsNullOrWhiteSpace(query)
                ? _itMan.GetAll()
                : _itMan.Search(query);

            if (categoryIds != null && categoryIds.Count > 0)
                items = items.Where(i => categoryIds.Contains(i.CategoryId)).ToList();

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

            HashSet<int> wishlistIds = new();
            if (IsAuthenticated && CurrentUser != null)
            {
                var userId = CurrentUser.Id;
                wishlistIds = _usMan.GetWishList(userId)
                                    .Select(i => i.Id)
                                    .ToHashSet();
            }

            var model = paged.Select(x => new ItemCardVm
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                OldPrice = null,
                ImageUrl = x.ImageUrl,
                InWishlist = wishlistIds.Contains(x.Id)
            }).ToList();

            ViewBag.Query = query ?? "";
            ViewBag.CategoryIds = categoryIds ?? new List<int>();
            ViewBag.Page = page;
            ViewBag.PageSize = PageSize;
            ViewBag.Total = total;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentSort = sort;

            return View(model);
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

            if (IsAuthenticated && CurrentUser != null)
            {
                var userId = CurrentUser.Id;
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
            if (!IsAuthenticated || CurrentUser == null) return RequireLogin(Url.Action(nameof(Cart)));

            SetLayoutData();

            var userId = CurrentUser.Id;
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
            if (!IsAuthenticated || CurrentUser == null)
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

            var userId = CurrentUser.Id;

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
            if (!IsAuthenticated || CurrentUser == null) return RequireLogin();

            var userId = CurrentUser.Id;

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
            if (!IsAuthenticated || CurrentUser == null) return RequireLogin();

            var userId = CurrentUser.Id;

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
            if (!IsAuthenticated || CurrentUser == null) return RequireLogin();

            var userId = CurrentUser.Id;

            _usMan.ClearCart(userId);

            TempData["Ok"] = "Корзина очищена.";
            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        public IActionResult Wishlist()
        {
            if (!IsAuthenticated || CurrentUser == null) return RequireLogin(Url.Action(nameof(Wishlist)));

            SetLayoutData();

            var userId = CurrentUser.Id;
            var items = _usMan.GetWishList(userId).ToList();

            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToWishlist(int id)
        {
            if (!IsAuthenticated || CurrentUser == null)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, requireLogin = true, message = "Требуется авторизация" });
                }
                return RequireLogin();
            }

            var item = _itMan.GetById(id);
            if (item == null) return NotFound();

            var userId = CurrentUser.Id;

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
            if (!IsAuthenticated || CurrentUser == null) return RequireLogin();

            var userId = CurrentUser.Id;

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
            if (!IsAuthenticated || CurrentUser == null) return RequireLogin(Url.Action(nameof(Checkout)));

            SetLayoutData();

            var userId = CurrentUser.Id;
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
            if (!IsAuthenticated || CurrentUser == null) return RequireLogin(Url.Action(nameof(Checkout)));

            var userId = CurrentUser.Id;

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
            if (!IsAuthenticated || CurrentUser == null) return RequireLogin(Url.Action(nameof(Orders)));

            SetLayoutData();

            var userId = CurrentUser.Id;
            var orders = _usMan.GetUserOrders(userId)
                .OrderByDescending(o => o.Id)
                .ToList();

            ViewBag.OrdersCount = orders.Count;

            return View(orders);
        }

        [HttpGet]
        public IActionResult OrderDetails(int id)
        {
            if (!IsAuthenticated || CurrentUser == null) return RequireLogin(Url.Action(nameof(OrderDetails), new { id }));

            SetLayoutData();

            var order = _usMan.GetOrderById(id);
            if (order == null) return NotFound();

            if (!IsAdmin && order.UserId != CurrentUser.Id)
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
            if (!IsAuthenticated || CurrentUser == null) return RequireLogin(Url.Action(nameof(Orders)));

            var order = _usMan.GetOrderById(id);
            if (order == null) return NotFound();

            if (!IsAdmin && order.UserId != CurrentUser.Id)
                return RedirectToAction(nameof(AccessDenied));

            var ok = _usMan.CancelOrder(id);
            TempData[ok ? "Ok" : "Error"] = ok ? "Заказ отменён." : "Не удалось отменить заказ.";
            return RedirectToAction(nameof(Orders));
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
            if (!IsAuthenticated || CurrentUser == null) return RequireLogin(Url.Action(nameof(ItemDetails), new { id = itemId }));

            review.UserId = CurrentUser.Id;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleWishlist(int id)
        {
            if (!IsAuthenticated || CurrentUser == null)
                return Json(new { success = false, requireLogin = true });

            var userId = CurrentUser.Id;

            bool isInWishlist = _usMan.GetWishList(userId).Any(i => i.Id == id);

            if (isInWishlist)
                _usMan.RemoveFromWishList(userId, id);
            else
                _usMan.AddToWishList(userId, id);

            var wishlistCount = _usMan.GetWishList(userId).Count();

            return Json(new
            {
                success = true,
                inWishlist = !isInWishlist,
                wishlistCount
            });
        }

        public IActionResult Reviews()
        {
            ViewBag.ActiveMenu = "Reviews";
            return View("_ProfileSidebar");
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
