using Microsoft.AspNetCore.Identity;

namespace BazaR.Models
{
    public class User : IdentityUser<int>
    {
        public string? Name { get; set; }
        public string? Patronymic { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }

        public bool IsAdmin { get; set; }

        public List<CartItem> CartItems { get; set; } = new();

        public List<WishlistItem> WishlistItems { get; set; } = new();

        public List<Order> Orders { get; set; } = new();

        public List<Review> Reviews { get; set; } = new();

        public List<Item> SellingItems { get; set; } = new();
    }
}
