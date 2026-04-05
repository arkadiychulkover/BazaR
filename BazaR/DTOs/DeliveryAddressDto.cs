namespace BazaR.DTOs
{
    public class DeliveryAddressDto
    {
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string? Building { get; set; }
        public string? Apartment { get; set; }
        public string? PostalCode { get; set; }
    }
}
