using BazaR.Models;

namespace BazaR.ViewModels
{
    public class ItemDetailsViewModel
    {
        public Item Item { get; set; } = null!;
        public List<string> Images { get; set; } = new();
        public Category? Category { get; set; }
        public User? Seller { get; set; }
        public bool IsInCart { get; set; }
        public bool IsInWishlist { get; set; }
        public bool IsAuthenticated { get; set; }

        public List<Item> RelatedItems { get; set; } = new();
        public List<Item> RecommendedItems { get; set; } = new();
        public List<Item> BundleItems { get; set; } = new();
        public List<Item> DemoBundleItems { get; set; } = new();

        public double AverageRating => Item.Reviews.Any()
            ? Math.Round(Item.Reviews.Average(r => r.Rating), 1)
            : 0;

        public int ReviewCount => Item.Reviews.Count;
    }
}
