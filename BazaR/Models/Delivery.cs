namespace BazaR.Models
{
    public class Delivery
    {
        public int Id { get; set; }

        public string DeliveryPlace { get; set; }
        public string SendingPlace { get; set; }

        public TimeSpan DeliveryTime { get; set; }
        public DateTime SendingDate { get; set; }

        public decimal Price { get; set; }

        public PaymentType PaymentType { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
