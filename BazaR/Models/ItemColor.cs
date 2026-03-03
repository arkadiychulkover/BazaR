// ItemColor.cs
namespace BazaR.Models
{
    public class ItemColor
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public string Color { get; set; }
    }
}