using System.Numerics;

namespace BazaR.Models
{
    public class Item
    {
        public int Id { get; set; }

        public int Price { get; set; }
        public int Garantia { get; set; }

        public bool IsAvailable { get; set; }

        public string Name { get; set; }
        public string Desc { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public List<string>? Colors { get; set; }

        public List<Review> Reviews { get; set; } = new();

        public List<ComplectItem> ComplectItems { get; set; } = new();

        public List<ItemCharacteristic> Characteristics { get; set; } = new();

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Usluga> Uslugi { get; set; } = new();

        public List<Delivery> DeliveryVariants { get; set; } = new();

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
