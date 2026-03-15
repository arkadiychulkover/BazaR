namespace BazaR.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<CategoryFilter> Filters { get; set; } = new();

        public List<CategoryBrand> CategoryBrands { get; set; } = new();
    }
}