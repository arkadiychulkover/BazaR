namespace BazaR.Models
{
    public enum OrderStatus
    {
        New = 0,          // Новий
        Processing = 1,   // В обробці
        Shipped = 2,      // Відправлено
        Delivered = 3,    // Доставлено
        Cancelled = 4     // Скасовано
    }

    public enum OrderPaymentMethod
    {
        PayNow = 0,       // Оплатити зараз
        PayOnDelivery = 1,// Оплата при отриманні
        PayByCard = 2     // Карткою у відділенні
    }

    public enum OrderPaymentStatus
    {
        Pending = 0,      // Очікує оплати
        Paid = 1,         // Оплачено
        Failed = 2,       // Помилка оплати
        Refunded = 3      // Повернено
    }

    public enum OrderDeliveryMethod
    {
        SelfPickup = 0,        // Самовивіз BAZA-R
        NovaPoshta = 1,        // Нова Пошта
        UkrPoshta = 2,         // Укрпошта
        CourierNovaPoshta = 3  // Кур'єр Нова Пошта
    }

    public enum Cities
    {
        Kyiv = 1,
        Kharkiv,
        Odesa,
        Dnipro,
        Lviv,
        Zaporizhzhia,
        Mykolaiv,
        Vinnytsia,
        Kherson,
        Poltava,
        Chernihiv,
        Cherkasy,
        Zhytomyr,
        Sumy,
        Rivne,
        Ternopil,
        Lutsk,
        Uzhhorod,
        Chernivtsi,
        IvanoFrankivsk,
        Kropyvnytskyi,
        Khmelnytskyi
    }

    public enum SellerType
    {
        bazar = 1,
        otherSeller = 2,
        otherSellerOnSklad = 3
    }

    public enum ProductionCountry
    {
        Ukraine = 1,
        USA = 2,
        Germany = 3,
        France = 4,
        Poland = 5,
        Italy = 6,
        Spain = 7,
        Canada = 8,
        Japan = 9,
        China = 10
    }

    public enum PaymentType
    {
        Cash,
        Card,
        Online,
        PostPayment
    }

    public enum FilterValueType
    {
        String,
        Number,
        Boolean,
        Range
    }

    public enum UserAction
    {
        VisitingPage = 1,
        AddToCart = 2,
        AddToWishList = 3,
        MakeOrder = 4,
        Browse = 5,
    }
}