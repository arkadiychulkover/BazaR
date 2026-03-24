namespace BazaR.Models
{
    namespace BazaR.Models
    {
        public class SearchFilters
        {
            public int Id { get; set; }
            public string? Query { get; set; }
            public List<int>? CategoryIds { get; set; }
            public int Page { get; set; } = 1;
            public string Sort { get; set; } = "default";
            public decimal? MinPrice { get; set; }
            public decimal? MaxPrice { get; set; }
            public List<int>? BrandIds { get; set; }
        }
    }
}
