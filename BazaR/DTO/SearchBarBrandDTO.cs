using BazaR.Models;

namespace BazaR.DTO
{
    public class SearchBarBrandDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SearchBarBrandDTO(Brand b) 
        {
            Id = b.Id;
            Name = b.Name;
        }
    }
}
