using BazaR.Models;

namespace BazaR.ViewModels
{
    public class AccountOrderDetailViewModel
    {
        public AccountProfileViewModel Profile { get; set; } = new();
        public Order Order { get; set; } = null!;
    }
}
