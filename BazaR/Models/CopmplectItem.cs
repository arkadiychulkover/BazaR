namespace BazaR.Models
{
    public class ComplectItem
    {
        public int ComplectId { get; set; }
        public Complect Complect { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}