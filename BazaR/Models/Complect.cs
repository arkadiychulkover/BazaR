namespace BazaR.Models
{
    public class Complect
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ComplectItem> Items { get; set; } = new();
    }
}