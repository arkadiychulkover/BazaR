using BazaR.Models.BazaR.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace BazaR.Models
{
    public class VisitingModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserAction userAction { get; set; }

        public string? ActionName { get; set; }
        public string? ControllerName { get; set; }

        public int? ItemId { get; set; }
        public int? OrderId { get; set; }

        // Это поле будет храниться как JSON в БД
        public string? SearchFiltersJson { get; set; }

        [NotMapped]
        public SearchFilters? SearchFilters
        {
            get => string.IsNullOrEmpty(SearchFiltersJson) ? null : JsonSerializer.Deserialize<SearchFilters>(SearchFiltersJson);
            set => SearchFiltersJson = value == null ? null : JsonSerializer.Serialize(value);
        }
    }
}