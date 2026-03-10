using BazaR.Models;

namespace BazaR.Interfaces
{
    public interface IItemRepository
    {
        // Основные методы для товаров
        List<Item> GetAll();
        Item? GetById(int id);
        List<Item> GetByCategory(int categoryId);
        List<Item> GetByBrand(int brandId);
        List<Item> Search(string query);
        List<Item> Filter(int? categoryId, int? brandId, decimal? minPrice, decimal? maxPrice, bool? isAvailable);

        // Методы для категорий
        List<Category> GetAllCategories();
        List<Category> GetAllCategoriesWithFilters();
        List<Category> GetMainCategories();
        List<Category> GetSubCategories(int parentCategoryId);
        Category? GetCategoryById(int id);
        Category? GetCategoryWithSubCategories(int id);
        List<Category> GetCategoryPath(int categoryId);

        // Методы для брендов
        List<Brand> GetAllBrands();
        Brand? GetBrandById(int id);
        List<Brand> GetBrandsByCategory(int categoryId);

        // Методы для фильтрации
        Dictionary<string, List<string>> GetFilterValues(int categoryId, string filterKey);
        List<Item> FilterWithDynamicFilters(int? categoryId, Dictionary<string, string> dynamicFilters);

        // CRUD операции
        bool Create(Item item);
        Item? Update(int id, Item item);
        bool Delete(int id);

        // Отзывы
        bool AddReview(int itemId, Review review);
        bool RemoveReview(int reviewId);

        // Характеристики
        bool AddCharacteristic(int itemId, ItemCharacteristic characteristic);
        bool RemoveCharacteristic(int characteristicId);

        // Услуги
        bool AddUsluga(int itemId, Usluga usluga);
        bool RemoveUsluga(int uslugaId);

        // Доставка
        bool AddDeliveryVariant(int itemId, Delivery delivery);
        bool RemoveDeliveryVariant(int deliveryId);

        bool Save();
    }
}