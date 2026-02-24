namespace BazaR.Models
{
    public class CategoryFilter
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string Key { get; set; }

        public string DisplayName { get; set; }

        public FilterValueType ValueType { get; set; }
    }
}