namespace BazaR.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string Number { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public string Address { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string DeliveryMethod { get; set; }

        public string Ttn { get; set; }

        public decimal TotalAmount { get; set; }

        public List<Item> Items { get; set; } = new();
    }
}
