namespace BazaR.ViewModels
{
    public class AccountProductsViewModel
    {
        public AccountProfileViewModel Profile { get; set; } = new();
        public List<ItemCardVm> Items { get; set; } = new();
    }
}