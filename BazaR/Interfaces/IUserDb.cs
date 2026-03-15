using BazaR.Models;

namespace BazaR.Interfaces
{
    public interface IUserDb
    {
        bool AddUser(User user);
        User UpdateUser(User user);
        User Delete(int id);

        IQueryable<User> GetAllUsers();
        User GetUser(int id);
        User GetByEmail(string email);

        bool AddAdminRights(int id);
        bool RemoveAdminRights(int id);

        bool ChangePassword(int userId, string newPassword);

        // Cart operations
        bool AddToCart(int userId, int itemId);
        bool RemoveFromCart(int userId, int itemId);
        bool ClearCart(int userId);
        IQueryable<Item> GetCartItems(int userId);
        List<CartItem> GetCartItemsWithQuantity(int userId); // Изменено на List<CartItem>

        // Wishlist operations
        bool AddToWishList(int userId, int itemId);
        bool RemoveFromWishList(int userId, int itemId);
        IQueryable<Item> GetWishList(int userId);

        // Selling items operations
        bool AddItemToSell(int userId, Item item);
        bool RemoveItemFromSell(int userId, int itemId);
        IQueryable<Item> GetUserSellingItems(int userId);

        // Order operations
        bool CreateOrder(int userId, Order order);
        IQueryable<Order> GetUserOrders(int userId);
        Order GetOrderById(int orderId);
        bool CancelOrder(int orderId);

        IQueryable<Item> GetItemsInDelivery(int userId);
    }
}