namespace BazaR.Models
{
    public class Usluga
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
