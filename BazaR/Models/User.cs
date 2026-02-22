namespace BazaR.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsAdmin { get; set; }
        public List<Order> Orders { get; set; } = new();

        public List<Review> Reviews { get; set; } = new();

        public List<Item> TovarsOnSell { get; set; } = new();
        public List<Item> TovarsInKorzina { get; set; } = new();
        public List<Item> TovarsInWishList { get; set; } = new();
        public List<Item> TovarsInDelivery { get; set; } = new();
    }
}
