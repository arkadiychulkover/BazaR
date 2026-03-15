using BazaR.Models;

namespace BazaR.ViewModels
{
    public class LookedCardsViewModel
    {
        public AccountProfileViewModel Profile { get; set; }

        public List<LookedCardItemViewModel> LookedCards { get; set; } = new();
    }

    public class LookedCardItemViewModel
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public string ItemImageUrl { get; set; }

        public int Price { get; set; }

        public bool IsLooked { get; set; }

        public DateTime LookedAt { get; set; }

        public int ViewCount { get; set; }
    }
}
