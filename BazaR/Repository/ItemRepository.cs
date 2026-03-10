using BazaR.Data;
using BazaR.Interfaces;
using BazaR.Models;
using Microsoft.EntityFrameworkCore;

namespace BazaR.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        // ========== Основные методы для товаров ==========

        public List<Item> GetAll()
        {
            return _context.Items
                .Include(i => i.Brand)
                .Include(i => i.Category)
                .Include(i => i.Colors)
                .Include(i => i.Reviews)
                .Include(i => i.Characteristics)
                .Include(i => i.Uslugi)
                .Include(i => i.DeliveryVariants)
                .Include(i => i.User)
                .ToList();
        }

        public Item? GetById(int id)
        {
            return _context.Items
                .Include(i => i.Brand)
                .Include(i => i.Category)
                .Include(i => i.Colors)
                .Include(i => i.Reviews)
                    .ThenInclude(r => r.User)
                .Include(i => i.Characteristics)
                .Include(i => i.Uslugi)
                .Include(i => i.DeliveryVariants)
                .Include(i => i.User)
                .FirstOrDefault(i => i.Id == id);
        }

        public List<Item> GetByCategory(int categoryId)
        {
            return _context.Items
                .Where(i => i.CategoryId == categoryId)
                .Include(i => i.Brand)
                .Include(i => i.Reviews)
                .ToList();
        }

        public List<Item> GetByBrand(int brandId)
        {
            return _context.Items
                .Where(i => i.BrandId == brandId)
                .Include(i => i.Category)
                .Include(i => i.Reviews)
                .ToList();
        }

        public List<Item> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<Item>();

            return _context.Items
                .Where(i => i.Name.Contains(query) ||
                           (i.Desc != null && i.Desc.Contains(query)))
                .Include(i => i.Brand)
                .Include(i => i.Category)
                .Include(i => i.Reviews)
                .ToList();
        }

        public List<Item> Filter(int? categoryId, int? brandId, decimal? minPrice, decimal? maxPrice, bool? isAvailable)
        {
            var query = _context.Items.AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(i => i.CategoryId == categoryId.Value);

            if (brandId.HasValue)
                query = query.Where(i => i.BrandId == brandId.Value);

            if (minPrice.HasValue)
                query = query.Where(i => i.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(i => i.Price <= maxPrice.Value);

            if (isAvailable.HasValue)
                query = query.Where(i => i.IsAvailable == isAvailable.Value);

            return query
                .Include(i => i.Brand)
                .Include(i => i.Category)
                .Include(i => i.Reviews)
                .ToList();
        }

        // ========== Методы для категорий ==========

        public List<Category> GetAllCategories()
        {
            return _context.Categories
                .OrderBy(c => c.DisplayOrder)
                .ToList();
        }

        public List<Category> GetAllCategoriesWithFilters()
        {
            return _context.Categories
                .Include(c => c.CategoryBrands)
                    .ThenInclude(cb => cb.Brand)
                .Include(c => c.Filters)
                .Include(c => c.SubCategories)
                .OrderBy(c => c.DisplayOrder)
                .ToList();
        }

        public List<Category> GetMainCategories()
        {
            return _context.Categories
                .Where(c => c.ParentCategoryId == null)
                .OrderBy(c => c.DisplayOrder)
                .ToList();
        }

        public List<Category> GetSubCategories(int parentCategoryId)
        {
            return _context.Categories
                .Where(c => c.ParentCategoryId == parentCategoryId)
                .OrderBy(c => c.DisplayOrder)
                .ToList();
        }

        public Category? GetCategoryById(int id)
        {
            return _context.Categories
                .Include(c => c.CategoryBrands)
                    .ThenInclude(cb => cb.Brand)
                .Include(c => c.Filters)
                .Include(c => c.SubCategories)
                .FirstOrDefault(c => c.Id == id);
        }

        public Category? GetCategoryWithSubCategories(int id)
        {
            return _context.Categories
                .Include(c => c.SubCategories)
                    .ThenInclude(sc => sc.SubCategories)
                .Include(c => c.CategoryBrands)
                    .ThenInclude(cb => cb.Brand)
                .Include(c => c.Filters)
                .FirstOrDefault(c => c.Id == id);
        }

        public List<Category> GetCategoryPath(int categoryId)
        {
            var path = new List<Category>();
            var current = _context.Categories.Find(categoryId);

            while (current != null)
            {
                path.Insert(0, current);
                current = current.ParentCategoryId.HasValue
                    ? _context.Categories.Find(current.ParentCategoryId.Value)
                    : null;
            }

            return path;
        }

        // ========== Методы для брендов ==========

        public List<Brand> GetAllBrands()
        {
            return _context.Brands
                .OrderBy(b => b.Name)
                .ToList();
        }

        public Brand? GetBrandById(int id)
        {
            return _context.Brands
                .Include(b => b.CategoryBrands)
                    .ThenInclude(cb => cb.Category)
                .Include(b => b.Items)
                .FirstOrDefault(b => b.Id == id);
        }

        public List<Brand> GetBrandsByCategory(int categoryId)
        {
            return _context.CategoryBrands
                .Where(cb => cb.CategoryId == categoryId)
                .Include(cb => cb.Brand)
                .Select(cb => cb.Brand)
                .OrderBy(b => b.Name)
                .ToList();
        }

        // ========== Методы для фильтрации ==========

        public Dictionary<string, List<string>> GetFilterValues(int categoryId, string filterKey)
        {
            var items = _context.Items
                .Where(i => i.CategoryId == categoryId)
                .Include(i => i.Characteristics)
                .ToList();

            var values = new Dictionary<string, List<string>>();

            foreach (var item in items)
            {
                var chars = item.Characteristics
                    .Where(c => c.Key == filterKey)
                    .Select(c => c.Value)
                    .Distinct()
                    .ToList();

                if (chars.Any())
                {
                    values[item.Id.ToString()] = chars;
                }
            }

            return values;
        }

        public List<Item> FilterWithDynamicFilters(int? categoryId, Dictionary<string, string> dynamicFilters)
        {
            var query = _context.Items
                .Include(i => i.Brand)
                .Include(i => i.Category)
                .Include(i => i.Characteristics)
                .Include(i => i.Reviews)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(i => i.CategoryId == categoryId.Value);
            }

            foreach (var filter in dynamicFilters)
            {
                var key = filter.Key.Replace("filter_", "");
                var value = filter.Value;

                query = query.Where(i => i.Characteristics
                    .Any(c => c.Key == key && c.Value.Contains(value)));
            }

            return query.ToList();
        }

        // ========== CRUD операции ==========

        public bool Create(Item item)
        {
            try
            {
                _context.Items.Add(item);
                return Save();
            }
            catch
            {
                return false;
            }
        }

        public Item? Update(int id, Item item)
        {
            try
            {
                var existing = _context.Items.Find(id);
                if (existing == null) return null;

                existing.Name = item.Name;
                existing.Desc = item.Desc;
                existing.Price = item.Price;
                existing.Garantia = item.Garantia;
                existing.IsAvailable = item.IsAvailable;
                existing.ImageUrl = item.ImageUrl;
                existing.BrandId = item.BrandId;
                existing.CategoryId = item.CategoryId;

                _context.Items.Update(existing);
                Save();
                return existing;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var item = _context.Items.Find(id);
                if (item == null) return false;

                _context.Items.Remove(item);
                return Save();
            }
            catch
            {
                return false;
            }
        }

        // ========== Отзывы ==========

        public bool AddReview(int itemId, Review review)
        {
            try
            {
                review.ItemId = itemId;
                _context.Reviews.Add(review);
                return Save();
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveReview(int reviewId)
        {
            try
            {
                var review = _context.Reviews.Find(reviewId);
                if (review == null) return false;

                _context.Reviews.Remove(review);
                return Save();
            }
            catch
            {
                return false;
            }
        }

        // ========== Характеристики ==========

        public bool AddCharacteristic(int itemId, ItemCharacteristic characteristic)
        {
            try
            {
                characteristic.ItemId = itemId;
                _context.ItemCharacteristics.Add(characteristic);
                return Save();
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveCharacteristic(int characteristicId)
        {
            try
            {
                var characteristic = _context.ItemCharacteristics.Find(characteristicId);
                if (characteristic == null) return false;

                _context.ItemCharacteristics.Remove(characteristic);
                return Save();
            }
            catch
            {
                return false;
            }
        }

        // ========== Услуги ==========

        public bool AddUsluga(int itemId, Usluga usluga)
        {
            try
            {
                usluga.ItemId = itemId;
                _context.Uslugi.Add(usluga);
                return Save();
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveUsluga(int uslugaId)
        {
            try
            {
                var usluga = _context.Uslugi.Find(uslugaId);
                if (usluga == null) return false;

                _context.Uslugi.Remove(usluga);
                return Save();
            }
            catch
            {
                return false;
            }
        }

        // ========== Доставка ==========

        public bool AddDeliveryVariant(int itemId, Delivery delivery)
        {
            try
            {
                delivery.ItemId = itemId;
                _context.Deliveries.Add(delivery);
                return Save();
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveDeliveryVariant(int deliveryId)
        {
            try
            {
                var delivery = _context.Deliveries.Find(deliveryId);
                if (delivery == null) return false;

                _context.Deliveries.Remove(delivery);
                return Save();
            }
            catch
            {
                return false;
            }
        }

        // ========== Сохранение ==========

        public bool Save()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}