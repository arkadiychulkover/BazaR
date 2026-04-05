// Models/Brand.cs
namespace BazaR.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Logo { get; set; }
        public List<CategoryBrand> CategoryBrands { get; set; } = new();
        public List<Item> Items { get; set; } = new(); // Добавлено
    }
}