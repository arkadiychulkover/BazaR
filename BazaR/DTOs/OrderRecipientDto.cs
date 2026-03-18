namespace BazaR.DTOs
{
    public class OrderRecipientDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string? Phone { get; set; }
    }
}
