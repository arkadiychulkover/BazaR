namespace BazaR.Models
{
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<CategoryBrand> CategoryBrands { get; set; } = new();
    }
}
