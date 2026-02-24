using Microsoft.EntityFrameworkCore;
using BazaR.Data;
using BazaR.Interfaces;
using BazaR.Models;

namespace BazaR.Repositories;

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
            .Include(i => i.Reviews)
            .Include(i => i.Characteristics)
            .Include(i => i.Uslugi)
            .Include(i => i.DeliveryVariants)
            .ToList();
    }

    public Item? GetById(int id)
    {
        return _context.Items
            .Include(i => i.Reviews)
            .Include(i => i.Characteristics)
            .Include(i => i.Uslugi)
            .Include(i => i.DeliveryVariants)
            .FirstOrDefault(i => i.Id == id);
    }

    public List<Item> GetByCategory(int categoryId)
    {
        return _context.Items
            .Where(i => i.CategoryId == categoryId)
            .Include(i => i.Reviews)
            .Include(i => i.Characteristics)
            .Include(i => i.Uslugi)
            .Include(i => i.DeliveryVariants)
            .ToList();
    }

    public List<Item> GetByBrand(int brandId)
    {
        return _context.Items
            .Where(i => i.BrandId == brandId)
            .Include(i => i.Reviews)
            .Include(i => i.Characteristics)
            .Include(i => i.Uslugi)
            .Include(i => i.DeliveryVariants)
            .ToList();
    }

    public List<Item> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return GetAll();

        var lower = query.ToLowerInvariant();
        return _context.Items
            .Where(i =>
                (i.Name != null && i.Name.ToLower().Contains(lower)) ||
                (i.Desc != null && i.Desc.ToLower().Contains(lower)))
            .Include(i => i.Reviews)
            .Include(i => i.Characteristics)
            .Include(i => i.Uslugi)
            .Include(i => i.DeliveryVariants)
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
            .Include(i => i.Reviews)
            .Include(i => i.Characteristics)
            .Include(i => i.Uslugi)
            .Include(i => i.DeliveryVariants)
            .ToList();
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
        var existing = _context.Items.Find(id);
        if (existing == null)
            return null;

        existing.Name = item.Name;
        existing.Desc = item.Desc;
        existing.Price = item.Price;
        existing.IsAvailable = item.IsAvailable;
        existing.CategoryId = item.CategoryId;
        existing.BrandId = item.BrandId;

        return Save() ? existing : null;
    }

    public bool Delete(int id)
    {
        var item = _context.Items.Find(id);
        if (item == null)
            return false;

        _context.Items.Remove(item);
        return Save();
    }

    public bool AddReview(int itemId, Review review)
    {
        var item = _context.Items.Find(itemId);
        if (item == null)
            return false;

        review.ItemId = itemId;
        _context.Reviews.Add(review);
        return Save();
    }

    public bool RemoveReview(int reviewId)
    {
        var review = _context.Reviews.Find(reviewId);
        if (review == null)
            return false;

        _context.Reviews.Remove(review);
        return Save();
    }

    public bool AddCharacteristic(int itemId, ItemCharacteristic characteristic)
    {
        var item = _context.Items.Find(itemId);
        if (item == null)
            return false;

        characteristic.ItemId = itemId;
        _context.ItemCharacteristics.Add(characteristic);
        return Save();
    }

    public bool RemoveCharacteristic(int characteristicId)
    {
        var characteristic = _context.ItemCharacteristics.Find(characteristicId);
        if (characteristic == null)
            return false;

        _context.ItemCharacteristics.Remove(characteristic);
        return Save();
    }

    public bool AddUsluga(int itemId, Usluga usluga)
    {
        var item = _context.Items.Find(itemId);
        if (item == null)
            return false;

        usluga.ItemId = itemId;
        _context.Uslugas.Add(usluga);
        return Save();
    }

    public bool RemoveUsluga(int uslugaId)
    {
        var usluga = _context.Uslugas.Find(uslugaId);
        if (usluga == null)
            return false;

        _context.Uslugas.Remove(usluga);
        return Save();
    }

    public bool AddDeliveryVariant(int itemId, Delivery delivery)
    {
        var item = _context.Items.Find(itemId);
        if (item == null)
            return false;

        delivery.ItemId = itemId;
        _context.Deliveries.Add(delivery);
        return Save();
    }

    public bool RemoveDeliveryVariant(int deliveryId)
    {
        var delivery = _context.Deliveries.Find(deliveryId);
        if (delivery == null)
            return false;

        _context.Deliveries.Remove(delivery);
        return Save();
    }

    public bool Save()
    {
        try
        {
            return _context.SaveChanges() >= 0;
        }
        catch
        {
            return false;
        }
    }
}