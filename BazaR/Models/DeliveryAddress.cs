namespace BazaR.Models
{
    public class DeliveryAddress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string? Building { get; set; }
        public string? Apartment { get; set; }
        public string? PostalCode { get; set; }
        public bool IsDefault { get; set; }
    }
}
