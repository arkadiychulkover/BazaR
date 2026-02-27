using BazaR.Data;
using BazaR.Interfaces;
using BazaR.Models;
using Microsoft.EntityFrameworkCore;

namespace BazaR.Repository
{
    public class UserRepository : IUserDb
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public User UpdateUser(User user)
        {
            var existing = _context.Users
                .Include(u => u.CartItems)
                .Include(u => u.WishlistItems)
                .Include(u => u.Orders)
                .Include(u => u.Reviews)
                .Include(u => u.SellingItems)
                .FirstOrDefault(u => u.Id == user.Id);

            if (existing == null) return null;

            existing.Email = user.Email;
            existing.PasswordHash = user.PasswordHash;
            existing.Name = user.Name;
            existing.IsAdmin = user.IsAdmin;

            _context.SaveChanges();
            return existing;
        }

        public User Delete(int id)
        {
            try
            {
                var user = _context.Users
                    .Include(u => u.CartItems)
                    .Include(u => u.WishlistItems)
                    .FirstOrDefault(u => u.Id == id);

                if (user == null) return null;

                _context.CartItems.RemoveRange(user.CartItems);
                _context.WishlistItems.RemoveRange(user.WishlistItems);

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
                .Include(u => u.Reviews);
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
                    .ThenInclude(i => i.Reviews)
                .Include(u => u.CartItems)
                    .ThenInclude(ci => ci.Item)
                    .ThenInclude(i => i.Colors)
                .Include(u => u.WishlistItems)
                    .ThenInclude(wi => wi.Item)
                    .ThenInclude(i => i.Reviews)
                .Include(u => u.WishlistItems)
                    .ThenInclude(wi => wi.Item)
                    .ThenInclude(i => i.Colors)
                .FirstOrDefault(u => u.Id == id);
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public bool AddAdminRights(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return false;

            user.IsAdmin = true;
            _context.SaveChanges();
            return true;
        }

        public bool RemoveAdminRights(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return false;

            user.IsAdmin = false;
            _context.SaveChanges();
            return true;
        }

        public bool ChangePassword(int userId, string newPassword)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return false;

            user.PasswordHash = newPassword;
            _context.SaveChanges();
            return true;
        }

        public bool AddToCart(int userId, int itemId)
        {
            try
            {
                var user = _context.Users.Include(u => u.CartItems).FirstOrDefault(u => u.Id == userId);
                var item = _context.Items.FirstOrDefault(i => i.Id == itemId);

                if (user == null || item == null) return false;

                var existing = user.CartItems.FirstOrDefault(ci => ci.ItemId == itemId);
                if (existing != null)
                {
                    existing.Quantity++;
                }
                else
                {
                    user.CartItems.Add(new CartItem { UserId = userId, ItemId = itemId, Quantity = 1 });
                }

                _context.SaveChanges();
                return true;
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
                var user = _context.Users.Include(u => u.CartItems).FirstOrDefault(u => u.Id == userId);
                if (user == null) return false;

                var cartItem = user.CartItems.FirstOrDefault(ci => ci.ItemId == itemId);
                if (cartItem == null) return false;

                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else
                {
                    user.CartItems.Remove(cartItem);
                }

                _context.SaveChanges();
                return true;
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
                var user = _context.Users.Include(u => u.CartItems).FirstOrDefault(u => u.Id == userId);
                if (user == null) return false;

                user.CartItems.Clear();
                _context.SaveChanges();
                return true;
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
                .Select(ci => ci.Item)
                .Include(i => i.Reviews)
                .Include(i => i.Characteristics)
                .Include(i => i.Colors)
                .Include(i => i.Uslugi)
                .Include(i => i.DeliveryVariants);
        }

        public List<CartItem> GetCartItemsWithQuantity(int userId)
        {
            return _context.CartItems
                .Where(ci => ci.UserId == userId)
                .Include(ci => ci.Item)
                    .ThenInclude(i => i.Reviews)
                .Include(ci => ci.Item)
                    .ThenInclude(i => i.Colors)
                .ToList();
        }

        public bool AddToWishList(int userId, int itemId)
        {
            try
            {
                var user = _context.Users.Include(u => u.WishlistItems).FirstOrDefault(u => u.Id == userId);
                var item = _context.Items.FirstOrDefault(i => i.Id == itemId);

                if (user == null || item == null) return false;

                var exists = user.WishlistItems.Any(wi => wi.ItemId == itemId);
                if (!exists)
                {
                    user.WishlistItems.Add(new WishlistItem { UserId = userId, ItemId = itemId });
                    _context.SaveChanges();
                }
                return true;
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
                var user = _context.Users.Include(u => u.WishlistItems).FirstOrDefault(u => u.Id == userId);
                if (user == null) return false;

                var wishItem = user.WishlistItems.FirstOrDefault(wi => wi.ItemId == itemId);
                if (wishItem == null) return false;

                user.WishlistItems.Remove(wishItem);
                _context.SaveChanges();
                return true;
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
                .Select(wi => wi.Item)
                .Include(i => i.Reviews)
                .Include(i => i.Characteristics)
                .Include(i => i.Colors)
                .Include(i => i.Uslugi)
                .Include(i => i.DeliveryVariants);
        }

        public bool AddItemToSell(int userId, Item item)
        {
            try
            {
                var user = _context.Users.Include(u => u.SellingItems).FirstOrDefault(u => u.Id == userId);
                if (user == null) return false;

                item.UserId = userId;
                user.SellingItems.Add(item);
                _context.Items.Add(item);
                _context.SaveChanges();
                return true;
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
                var user = _context.Users.Include(u => u.SellingItems).FirstOrDefault(u => u.Id == userId);
                if (user == null) return false;

                var item = user.SellingItems.FirstOrDefault(i => i.Id == itemId);
                if (item == null) return false;

                user.SellingItems.Remove(item);
                _context.Items.Remove(item);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<Item> GetUserSellingItems(int userId)
        {
            return _context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.SellingItems)
                .Include(i => i.Reviews)
                .Include(i => i.Characteristics)
                .Include(i => i.Colors)
                .Include(i => i.Uslugi)
                .Include(i => i.DeliveryVariants);
        }

        public bool CreateOrder(int userId, Order order)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null) return false;

                order.UserId = userId;
                order.Number = GenerateOrderNumber();
                order.CreatedAt = DateTime.UtcNow;
                order.PaymentStatus = "Pending";

                _context.Orders.Add(order);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string GenerateOrderNumber()
        {
            return $"ORD-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }

        public IQueryable<Order> GetUserOrders(int userId)
        {
            return _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item)
                    .ThenInclude(i => i.Reviews)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item)
                    .ThenInclude(i => i.Colors)
                .Include(o => o.City);
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item)
                    .ThenInclude(i => i.Reviews)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item)
                    .ThenInclude(i => i.Colors)
                .Include(o => o.City)
                .Include(o => o.User)
                .FirstOrDefault(o => o.Id == orderId);
        }

        public bool CancelOrder(int orderId)
        {
            try
            {
                var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
                if (order == null) return false;

                order.Status = "Cancelled";
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<Item> GetItemsInDelivery(int userId)
        {
            return Enumerable.Empty<Item>().AsQueryable();
        }
    }
}