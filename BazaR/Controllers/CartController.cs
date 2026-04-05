using BazaR.Interfaces;
using BazaR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BazaR.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        #region Private Fields

        private readonly IUserDb _usMan;
        private readonly UserManager<User> _userManager;

        #endregion

        #region Constructor

        public CartController(IUserDb usMan, UserManager<User> userManager)
        {
            _usMan = usMan;
            _userManager = userManager;
        }

        #endregion

        #region Public Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var userId = user.Id;
            var cartItems = _usMan.GetCartItemsWithQuantity(userId);
            var items = cartItems.Select(ci => ci.Item).ToList();

            ViewBag.CartItems = cartItems.ToDictionary(ci => ci.ItemId, ci => ci.Quantity);
            ViewBag.TotalAmount = cartItems.Sum(ci => ci.Item.Price * ci.Quantity);
            ViewBag.TotalQuantity = cartItems.Sum(ci => ci.Quantity);

            return View("Cart", items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int itemId, int quantity = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            if (quantity < 1)
                quantity = 1;

            var userId = user.Id;

            for (int i = 0; i < quantity; i++)
            {
                _usMan.AddToCart(userId, itemId);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int itemId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            _usMan.RemoveFromCart(user.Id, itemId);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Clear()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            _usMan.ClearCart(user.Id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var userId = user.Id;
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
        public async Task<IActionResult> CreateOrder(string address, string paymentMethod, string deliveryMethod)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var userId = user.Id;
            var cartItems = _usMan.GetCartItemsWithQuantity(userId);

            if (!cartItems.Any())
            {
                TempData["Error"] = "Корзина пустая.";
                return RedirectToAction(nameof(Index));
            }

            var order = new Order
            {
                Status = OrderStatus.New,
                CreatedAt = DateTime.UtcNow,
                OrderItems = new List<OrderItem>(),
                TotalAmount = 0,
                Address = address,
                PaymentMethod = OrderPaymentMethod.PayNow,
                DeliveryMethod = OrderDeliveryMethod.SelfPickup,
                PaymentStatus = OrderPaymentStatus.Pending,
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

        #endregion
    }
}