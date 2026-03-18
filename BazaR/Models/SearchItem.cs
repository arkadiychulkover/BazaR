using BazaR.Models;

public class SearchItem
{
    public int Id { get; set; }
    public string Value { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; } // Измените на User?
}