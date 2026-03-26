namespace BazaR.Models
{
    public class OrderRecipient
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string? Phone { get; set; }
        public bool IsDefault { get; set; }
    }
}
