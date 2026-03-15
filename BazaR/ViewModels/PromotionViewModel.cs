using BazaR.Models;
namespace BazaR.ViewModels
{
    public class PromotionsViewModel
    {
        public AccountProfileViewModel Profile { get; set; }
        public List<PromotionItemViewModel> Promotions { get; set; } = new();
    }

    public class PromotionItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime? EndsAt { get; set; }
        public bool IsSubscribed { get; set; }

        public bool IsExpired => EndsAt.HasValue && EndsAt.Value < DateTime.UtcNow;
        public bool IsActive => !IsExpired;
    }
}