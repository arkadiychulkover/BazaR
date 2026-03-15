namespace BazaR.ViewModels
{
    public class OrderItemVm
    {
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderVm
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = string.Empty;
        public int Total { get; set; }
        public List<OrderItemVm> Items { get; set; } = new();
    }

    public class AccountOrdersViewModel
    {
        public AccountProfileViewModel Profile { get; set; } = new();
        public List<OrderVm> Orders { get; set; } = new();
    }
}