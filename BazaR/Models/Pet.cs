namespace BazaR.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public string? Type { get; set; }
        public string? Breed { get; set; }
    }
}
