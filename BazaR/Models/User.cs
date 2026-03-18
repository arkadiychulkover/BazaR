using Microsoft.AspNetCore.Identity;

namespace BazaR.Models
{
    public class User : IdentityUser<int>
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? Gender { get; set; }

        public bool IsAdmin { get; set; }

        public List<CartItem> CartItems { get; set; } = new();
        public List<WishlistItem> WishlistItems { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
        public List<Item> SellingItems { get; set; } = new();

        public List<OrderRecipient> OrderRecipients { get; set; } = new();
        public List<DeliveryAddress> DeliveryAddresses { get; set; } = new();
        public List<Pet> Pets { get; set; } = new();
        public List<AdditionalInfo> AdditionalInfos { get; set; } = new();
    }
}