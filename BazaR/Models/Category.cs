// Models/Category.cs
namespace BazaR.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? IconUrl { get; set; } // Иконка категории (Bootstrap Icons class)
        public string? ImgUrl { get; set; } // Иконка категории (Bootstrap Icons class)

        public int? ParentCategoryId { get; set; } // Для поддержки подкатегорий
        public Category? ParentCategory { get; set; }

        public List<Category> SubCategories { get; set; } = new(); // Подкатегории

        public List<CategoryFilter> Filters { get; set; } = new();

        public List<CategoryBrand> CategoryBrands { get; set; } = new();

        public int DisplayOrder { get; set; } = 0; // Для сортировки
    }
}