using BazaR.Data;
using BazaR.Interfaces;
using BazaR.Models;
using Microsoft.EntityFrameworkCore;

namespace BazaR.Repositories
{
    public class UserRepository : IUserDb
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        // ========== Управление пользователями ==========

        public bool AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public User UpdateUser(User user)
        {
            try
            {
                var existing = _context.Users.Find(user.Id);
                if (existing == null) return null;

                existing.Name = user.Name;
                existing.Email = user.Email;
                existing.PasswordHash = user.PasswordHash;

                _context.Users.Update(existing);
                _context.SaveChanges();
                return existing;
            }
            catch
            {
                return null;
            }
        }

        public User Delete(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null) return null;

                _context.Users.Remove(user);
                _context.SaveChanges();
                return user;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<User> GetAllUsers()
        {
            return _context.Users
                .Include(u => u.Orders)
                .Include(u => u.Reviews)
                .Include(u => u.SellingItems)
                .AsQueryable();
        }

        public User GetUser(int id)
        {
            return _context.Users
                .Include(u => u.Orders)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Item)
                .Include(u => u.Reviews)
                .Include(u => u.SellingItems)
                .Include(u => u.CartItems)
                    .ThenInclude(ci => ci.Item)
                .Include(u => u.WishlistItems)
                    .ThenInclude(wi => wi.Item)
                .FirstOrDefault(u => u.Id == id);
        }

        public User GetByEmail(string email)
        {
            return _context.Users
                .FirstOrDefault(u => u.Email == email);
        }

        // ========== Права администратора ==========

        public bool AddAdminRights(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null) return false;

                user.IsAdmin = true;
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveAdminRights(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null) return false;

                user.IsAdmin = false;
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        // ========== Пароль ==========

        public bool ChangePassword(int userId, string newPassword)
        {
            try
            {
                var user = _context.Users.Find(userId);
                if (user == null) return false;

                user.PasswordHash = newPassword;
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        // ========== Корзина ==========

        public bool AddToCart(int userId, int itemId)
        {
            try
            {
                var existing = _context.CartItems
                    .FirstOrDefault(ci => ci.UserId == userId && ci.ItemId == itemId);

                if (existing != null)
                {
                    existing.Quantity++;
                }
                else
                {
                    _context.CartItems.Add(new CartItem
                    {
                        UserId = userId,
                        ItemId = itemId,
                        Quantity = 1
                    });
                }

                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveFromCart(int userId, int itemId)
        {
            try
            {
                var cartItem = _context.CartItems
                    .FirstOrDefault(ci => ci.UserId == userId && ci.ItemId == itemId);

                if (cartItem == null) return false;

                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else
                {
                    _context.CartItems.Remove(cartItem);
                }

                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool SetCartQuantity(int userId, int itemId, int quantity)
        {
            try
            {
                var cartItem = _context.CartItems
                    .FirstOrDefault(ci => ci.UserId == userId && ci.ItemId == itemId);

                if (quantity <= 0)
                {
                    if (cartItem != null)
                    {
                        _context.CartItems.Remove(cartItem);
                        return _context.SaveChanges() > 0;
                    }
                    return true;
                }

                if (cartItem != null)
                {
                    cartItem.Quantity = quantity;
                }
                else
                {
                    _context.CartItems.Add(new CartItem
                    {
                        UserId = userId,
                        ItemId = itemId,
                        Quantity = quantity
                    });
                }

                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool ClearCart(int userId)
        {
            try
            {
                var cartItems = _context.CartItems
                    .Where(ci => ci.UserId == userId)
                    .ToList();

                _context.CartItems.RemoveRange(cartItems);
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<Item> GetCartItems(int userId)
        {
            return _context.CartItems
                .Where(ci => ci.UserId == userId)
                .Include(ci => ci.Item)
                    .ThenInclude(i => i.Brand)
                .Include(ci => ci.Item)
                    .ThenInclude(i => i.Category)
                .Select(ci => ci.Item)
                .AsQueryable();
        }

        public List<CartItem> GetCartItemsWithQuantity(int userId)
        {
            return _context.CartItems
                .Where(ci => ci.UserId == userId)
                .Include(ci => ci.Item)
                    .ThenInclude(i => i.Brand)
                .Include(ci => ci.Item)
                    .ThenInclude(i => i.Category)
                .ToList();
        }

        // ========== Избранное ==========

        public bool AddToWishList(int userId, int itemId)
        {
            try
            {
                var existing = _context.WishlistItems
                    .FirstOrDefault(wi => wi.UserId == userId && wi.ItemId == itemId);

                if (existing == null)
                {
                    _context.WishlistItems.Add(new WishlistItem
                    {
                        UserId = userId,
                        ItemId = itemId
                    });
                }

                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveFromWishList(int userId, int itemId)
        {
            try
            {
                var wishlistItem = _context.WishlistItems
                    .FirstOrDefault(wi => wi.UserId == userId && wi.ItemId == itemId);

                if (wishlistItem == null) return false;

                _context.WishlistItems.Remove(wishlistItem);
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<Item> GetWishList(int userId)
        {
            return _context.WishlistItems
                .Where(wi => wi.UserId == userId)
                .Include(wi => wi.Item)
                    .ThenInclude(i => i.Brand)
                .Include(wi => wi.Item)
                    .ThenInclude(i => i.Category)
                .Select(wi => wi.Item)
                .AsQueryable();
        }

        // ========== Товары на продажу ==========

        public bool AddItemToSell(int userId, Item item)
        {
            try
            {
                item.UserId = userId;
                _context.Items.Add(item);
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveItemFromSell(int userId, int itemId)
        {
            try
            {
                var item = _context.Items
                    .FirstOrDefault(i => i.Id == itemId && i.UserId == userId);

                if (item == null) return false;

                _context.Items.Remove(item);
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<Item> GetUserSellingItems(int userId)
        {
            return _context.Items
                .Where(i => i.UserId == userId)
                .Include(i => i.Brand)
                .Include(i => i.Category)
                .Include(i => i.Reviews)
                .AsQueryable();
        }

        // ========== Заказы ==========

        public bool CreateOrder(int userId, Order order)
        {
            try
            {
                order.UserId = userId;
                order.Number = GenerateOrderNumber();
                order.CreatedAt = DateTime.UtcNow;
                order.Status = "Новий";
                order.PaymentStatus = "Очікує оплати";

                _context.Orders.Add(order);
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        private string GenerateOrderNumber()
        {
            return $"ORDER-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }

        public IQueryable<Order> GetUserOrders(int userId)
        {
            return _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item)
                .Include(o => o.City)
                .OrderByDescending(o => o.CreatedAt)
                .AsQueryable();
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item)
                        .ThenInclude(i => i.Brand)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item)
                        .ThenInclude(i => i.Category)
                .Include(o => o.User)
                .Include(o => o.City)
                .FirstOrDefault(o => o.Id == orderId);
        }

        public bool CancelOrder(int orderId)
        {
            try
            {
                var order = _context.Orders.Find(orderId);
                if (order == null) return false;

                order.Status = "Скасовано";
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        // ========== Доставка ==========

        public IQueryable<Item> GetItemsInDelivery(int userId)
        {
            return _context.Orders
                .Where(o => o.UserId == userId && o.Status == "Відправлено")
                .SelectMany(o => o.OrderItems)
                .Select(oi => oi.Item)
                .Include(i => i.Brand)
                .Include(i => i.Category)
                .AsQueryable();
        }
    }
}