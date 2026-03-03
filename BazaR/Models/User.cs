// User.cs
namespace BazaR.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsAdmin { get; set; }

        public List<CartItem> CartItems { get; set; } = new();

        public List<WishlistItem> WishlistItems { get; set; } = new();

        public List<Order> Orders { get; set; } = new();

        public List<Review> Reviews { get; set; } = new();

        public List<Item> SellingItems { get; set; } = new();
    }
}