// Repositories/ItemRepository.cs
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

        public List<Item> GetAll()
        {
            return _context.Items
                .Include(i => i.Category)
                .Include(i => i.Brand)
                .Include(i => i.Colors)
                .Include(i => i.Reviews)
                .ToList();
        }

        public Item? GetById(int id)
        {
            return _context.Items
                .Include(i => i.Category)
                .Include(i => i.Brand)
                .Include(i => i.Colors)
                .Include(i => i.Reviews)
                    .ThenInclude(r => r.User)
                .Include(i => i.Characteristics)
                .Include(i => i.Uslugi)
                .Include(i => i.DeliveryVariants)
                .FirstOrDefault(i => i.Id == id);
        }

        public List<Item> GetByCategory(int categoryId)
        {
            return _context.Items
                .Where(i => i.CategoryId == categoryId)
                .Include(i => i.Category)
                .Include(i => i.Brand)
                .Include(i => i.Colors)
                .ToList();
        }

        public List<Item> GetByBrand(int brandId)
        {
            return _context.Items
                .Where(i => i.BrandId == brandId)
                .Include(i => i.Category)
                .Include(i => i.Brand)
                .Include(i => i.Colors)
                .ToList();
        }

        public List<Item> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<Item>();

            return _context.Items
                .Where(i => i.Name.Contains(query) ||
                           (i.Desc != null && i.Desc.Contains(query)))
                .Include(i => i.Category)
                .Include(i => i.Brand)
                .Include(i => i.Colors)
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
                .Include(i => i.Category)
                .Include(i => i.Brand)
                .Include(i => i.Colors)
                .ToList();
        }

        // Новые методы для категорий
        public List<Category> GetAllCategories()
        {
            return _context.Categories
                .Include(c => c.SubCategories)
                .Include(c => c.CategoryBrands)
                    .ThenInclude(cb => cb.Brand)
                .OrderBy(c => c.DisplayOrder)
                .ToList();
        }

        public List<Category> GetMainCategories()
        {
            return _context.Categories
                .Where(c => c.ParentCategoryId == null)
                .Include(c => c.SubCategories)
                .Include(c => c.CategoryBrands)
                    .ThenInclude(cb => cb.Brand)
                .OrderBy(c => c.DisplayOrder)
                .ToList();
        }

        public List<Category> GetSubCategories(int parentCategoryId) => _context.Categories
            .Where(c => c.ParentCategoryId == parentCategoryId)
            .Include(c => c.SubCategories)
            .OrderBy(c => c.DisplayOrder)
            .ToList();



        public Category? GetCategoryById(int id)
        {
            return _context.Categories
                .Include(c => c.SubCategories)
                .Include(c => c.ParentCategory)
                .Include(c => c.CategoryBrands)
                    .ThenInclude(cb => cb.Brand)
                .FirstOrDefault(c => c.Id == id);
        }

        public Category? GetCategoryWithSubCategories(int id)
        {
            return _context.Categories
                .Include(c => c.SubCategories)
                    .ThenInclude(sc => sc.SubCategories)
                .Include(c => c.CategoryBrands)
                    .ThenInclude(cb => cb.Brand)
                .FirstOrDefault(c => c.Id == id);
        }

        public List<Category> GetCategoryPath(int categoryId)
        {
            var path = new List<Category>();
            var category = GetCategoryById(categoryId);
            while (category != null)
            {
                path.Insert(0, category);
                category = category.ParentCategory != null ? GetCategoryById(category.ParentCategory.Id) : null;
            }
            return path;
        }

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
                var existingItem = GetById(id);
                if (existingItem == null) return null;

                existingItem.Name = item.Name;
                existingItem.Price = item.Price;
                existingItem.Desc = item.Desc;
                existingItem.IsAvailable = item.IsAvailable;
                existingItem.BrandId = item.BrandId;
                existingItem.CategoryId = item.CategoryId;
                existingItem.Garantia = item.Garantia;

                _context.Items.Update(existingItem);
                Save();
                return existingItem;
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
                var item = GetById(id);
                if (item == null) return false;

                _context.Items.Remove(item);
                return Save();
            }
            catch
            {
                return false;
            }
        }

        public bool AddReview(int itemId, Review review)
        {
            try
            {
                var item = GetById(itemId);
                if (item == null) return false;

                item.Reviews.Add(review);
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

        public bool AddCharacteristic(int itemId, ItemCharacteristic characteristic)
        {
            try
            {
                var item = GetById(itemId);
                if (item == null) return false;

                item.Characteristics.Add(characteristic);
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

        public bool AddUsluga(int itemId, Usluga usluga)
        {
            try
            {
                var item = GetById(itemId);
                if (item == null) return false;

                item.Uslugi.Add(usluga);
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

        public bool AddDeliveryVariant(int itemId, Delivery delivery)
        {
            try
            {
                var item = GetById(itemId);
                if (item == null) return false;

                item.DeliveryVariants.Add(delivery);
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