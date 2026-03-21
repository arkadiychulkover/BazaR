namespace BazaR.Models
{
    public class CategoryStatistik
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime VisitedAt { get; set; }
    }
}
