namespace BazaR.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }

        public Order LastOrderInfo { get; set; }
        public int OrderId { get; set; }

        public List<Tovar> TovarsOnSale { get; set; }
        public List<Tovar> TovarsInKorzina { get; set; }
        public List<Tovar> TovarsInWishList { get; set; }
        public List<Tovar> TovarsInDelivery { get; set; }
    }
}
