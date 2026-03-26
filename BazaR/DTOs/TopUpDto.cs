namespace BazaR.DTOs
{
    public class TopUpDto
    {
        public decimal Amount { get; set; }
        public string Method { get; set; } = "card";
    }
}
