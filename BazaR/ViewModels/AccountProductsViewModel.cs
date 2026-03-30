namespace BazaR.ViewModels
{
    public class AccountProductsViewModel
    {
        public AccountProfileViewModel Profile { get; set; } = new();
        public IEnumerable<ItemCardVm> Items { get; set; } = new List<ItemCardVm>();
    }
}
