namespace BazaR.ViewModels
{
    public class ItemCardVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int? OldPrice { get; set; }
        public string ImageUrl { get; set; }
        public bool InWishlist { get; set; }
    }
}