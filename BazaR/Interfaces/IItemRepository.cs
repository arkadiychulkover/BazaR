using BazaR.Models;

namespace BazaR.Interfaces;

public interface IItemRepository
{
    List<Item> GetAll();
    Item? GetById(int id);
    List<Item> GetByCategory(int categoryId);
    List<Item> GetByBrand(int brandId);
    List<Item> Search(string query);
    List<Item> Filter(int? categoryId, int? brandId, decimal? minPrice, decimal? maxPrice, bool? isAvailable);

    bool Create(Item item);
    Item? Update(int id, Item item);
    bool Delete(int id);

    bool AddReview(int itemId, Review review);
    bool RemoveReview(int reviewId);

    bool AddCharacteristic(int itemId, ItemCharacteristic characteristic);
    bool RemoveCharacteristic(int characteristicId);

    bool AddUsluga(int itemId, Usluga usluga);
    bool RemoveUsluga(int uslugaId);

    bool AddDeliveryVariant(int itemId, Delivery delivery);
    bool RemoveDeliveryVariant(int deliveryId);

    bool Save();
}