namespace BazaR.Models
{
    public class AdditionalInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public string Key { get; set; } = string.Empty;
        public string? Value { get; set; }
    }
}
