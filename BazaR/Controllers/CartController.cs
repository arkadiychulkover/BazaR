using BazaR.Interfaces;
using BazaR.Models;
using Microsoft.AspNetCore.Mvc;

namespace BazaR.Controllers
{
    public class CartController : Controller
    {
        private readonly IUserDb _usMan;

        public CartController(IUserDb usMan)
        {
            _usMan = usMan;
        }

        private int? CurrentUserId => HttpContext.Session.GetInt32("uid");
        private bool IsAuthenticated => CurrentUserId.HasValue;

        private IActionResult RequireLogin(string? returnUrl = null)
        {
            TempData["Error"] = "Нужно войти в аккаунт.";
            return RedirectToAction("Index", "Site", new { returnUrl });
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(Index)));

            var userId = CurrentUserId!.Value;
            var cartItems = _usMan.GetCartItemsWithQuantity(userId);
            var items = cartItems.Select(ci => ci.Item).ToList();

            var totalAmount = cartItems.Sum(ci => ci.Item.Price * ci.Quantity);
            var totalQuantity = cartItems.Sum(ci => ci.Quantity);

            ViewBag.CartItems = cartItems.ToDictionary(ci => ci.ItemId, ci => ci.Quantity);
            ViewBag.TotalAmount = totalAmount;
            ViewBag.TotalQuantity = totalQuantity;

            return View("Cart", items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int itemId, int quantity = 1)
        {
            if (!IsAuthenticated) return RequireLogin();
            if (quantity < 1) quantity = 1;
            var userId = CurrentUserId!.Value;
            for (int i = 0; i < quantity; i++)
                _usMan.AddToCart(userId, itemId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int itemId)
        {
            if (!IsAuthenticated) return RequireLogin();
            _usMan.RemoveFromCart(CurrentUserId!.Value, itemId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Clear()
        {
            if (!IsAuthenticated) return RequireLogin();
            _usMan.ClearCart(CurrentUserId!.Value);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            if (!IsAuthenticated) return RequireLogin(Url.Action(nameof(Checkout)));
            var userId = CurrentUserId!.Value;
            var cartItems = _usMan.GetCartItemsWithQuantity(userId);
            if (!cartItems.Any())
            {
                TempData["Error"] = "Корзина пустая.";
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
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
            if (!_usMan.CreateOrder(userId, order))
            {
                TempData["Error"] = "Не удалось создать заказ.";
                return RedirectToAction(nameof(Checkout));
            }
            _usMan.ClearCart(userId);
            TempData["Ok"] = "Заказ создан!";
            return RedirectToAction(nameof(Index));
        }
    }
}
