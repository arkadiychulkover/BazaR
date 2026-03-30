namespace BazaR;

/// <summary>
/// Універсальні URL-шляхи до статичних ассетів.
/// Фізична папка: wwwroot/images/items/
/// </summary>
public static class WebPaths
{
    public const string ImagesItems = "/images/items";
    public const string DefaultItemImage = "/images/items/default.jpg";

    /// <summary>Іконка оплати / кредиту на PDP: файли у wwwroot/images/items/ (credit1.png … credit6.png).</summary>
    public static string PaymentCreditIcon(int index) => ItemImage($"credit{index}.png");
    /// <summary>Іконка для розділу замовлень</summary>
    public const string OrdersIcon = "/images/items/check-list.png";
    public static string ItemImage(string fileName) => $"{ImagesItems}/{fileName}";
}