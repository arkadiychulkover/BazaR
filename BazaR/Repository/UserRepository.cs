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
                .Include(u => u.TovarsOnSell)
                .Include(u => u.TovarsInKorzina)
                .Include(u => u.TovarsInWishList)
                .Include(u => u.TovarsInDelivery)
                .Include(u => u.Orders)
                .Include(u => u.Reviews)
                .FirstOrDefault(u => u.Id == user.Id);

            if (existing == null) return null;

            existing.Email = user.Email;
            existing.FirstName = user.FirstName;
            existing.SecondName = user.SecondName;
            existing.Password = user.Password;
            existing.PhoneNumber = user.PhoneNumber;
            existing.IsAdmin = user.IsAdmin;

            _context.SaveChanges();
            return existing;
        }

        public User Delete(int id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == id);
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
                .Include(u => u.Reviews);
        }

        public User GetUser(int id)
        {
            return _context.Users
                .Include(u => u.Orders)
                .Include(u => u.Reviews)
                .Include(u => u.TovarsOnSell)
                .Include(u => u.TovarsInKorzina)
                .Include(u => u.TovarsInWishList)
                .Include(u => u.TovarsInDelivery)
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

            user.Password = newPassword;
            _context.SaveChanges();
            return true;
        }

        public bool AddToCart(int userId, int itemId)
        {
            var user = _context.Users.Include(u => u.TovarsInKorzina)
                .FirstOrDefault(u => u.Id == userId);
            var item = _context.Items.FirstOrDefault(i => i.Id == itemId);

            if (user == null || item == null) return false;

            user.TovarsInKorzina.Add(item);
            _context.SaveChanges();
            return true;
        }

        public bool RemoveFromCart(int userId, int itemId)
        {
            var user = _context.Users.Include(u => u.TovarsInKorzina)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null) return false;

            var item = user.TovarsInKorzina.FirstOrDefault(i => i.Id == itemId);
            if (item == null) return false;

            user.TovarsInKorzina.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public bool ClearCart(int userId)
        {
            var user = _context.Users.Include(u => u.TovarsInKorzina)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null) return false;

            user.TovarsInKorzina.Clear();
            _context.SaveChanges();
            return true;
        }

        public IQueryable<Item> GetCartItems(int userId)
        {
            return _context.Users
                .Include(u => u.TovarsInKorzina)
                .Where(u => u.Id == userId)
                .SelectMany(u => u.TovarsInKorzina);
        }

        public bool AddToWishList(int userId, int itemId)
        {
            var user = _context.Users.Include(u => u.TovarsInWishList)
                .FirstOrDefault(u => u.Id == userId);
            var item = _context.Items.FirstOrDefault(i => i.Id == itemId);

            if (user == null || item == null) return false;

            user.TovarsInWishList.Add(item);
            _context.SaveChanges();
            return true;
        }

        public bool RemoveFromWishList(int userId, int itemId)
        {
            var user = _context.Users.Include(u => u.TovarsInWishList)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null) return false;

            var item = user.TovarsInWishList.FirstOrDefault(i => i.Id == itemId);
            if (item == null) return false;

            user.TovarsInWishList.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public IQueryable<Item> GetWishList(int userId)
        {
            return _context.Users
                .Include(u => u.TovarsInWishList)
                .Where(u => u.Id == userId)
                .SelectMany(u => u.TovarsInWishList);
        }

        public bool AddItemToSell(int userId, Item item)
        {
            var user = _context.Users.Include(u => u.TovarsOnSell)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null) return false;

            user.TovarsOnSell.Add(item);
            _context.Items.Add(item);
            _context.SaveChanges();
            return true;
        }

        public bool RemoveItemFromSell(int userId, int itemId)
        {
            var user = _context.Users.Include(u => u.TovarsOnSell)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null) return false;

            var item = user.TovarsOnSell.FirstOrDefault(i => i.Id == itemId);
            if (item == null) return false;

            user.TovarsOnSell.Remove(item);
            _context.Items.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public IQueryable<Item> GetUserSellingItems(int userId)
        {
            return _context.Users
                .Include(u => u.TovarsOnSell)
                .Where(u => u.Id == userId)
                .SelectMany(u => u.TovarsOnSell);
        }

        public bool CreateOrder(int userId, Order order)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return false;

            order.UserId = userId;
            _context.Orders.Add(order);
            _context.SaveChanges();
            return true;
        }

        public IQueryable<Order> GetUserOrders(int userId)
        {
            return _context.Orders.Where(o => o.UserId == userId);
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == orderId);
        }

        public bool CancelOrder(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null) return false;

            order.Status = "Cancelled";
            _context.SaveChanges();
            return true;
        }

        public IQueryable<Item> GetItemsInDelivery(int userId)
        {
            return _context.Users
                .Include(u => u.TovarsInDelivery)
                .Where(u => u.Id == userId)
                .SelectMany(u => u.TovarsInDelivery);
        }
    }
}