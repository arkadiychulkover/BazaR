using BazaR.Models;
namespace BazaR.ViewModels
{
    public class PromotionsViewModel
    {
        public AccountProfileViewModel Profile { get; set; }
        public List<Promotion> Promotions { get; set; } = new();
    }
}