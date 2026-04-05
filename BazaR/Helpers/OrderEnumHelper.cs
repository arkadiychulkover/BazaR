namespace BazaR.Models
{
    public static class OrderEnumHelper
    {
        public static string DisplayStatus(this OrderStatus s) => s switch
        {
            OrderStatus.New        => "Новий",
            OrderStatus.Processing => "В обробці",
            OrderStatus.Shipped    => "Відправлено",
            OrderStatus.Delivered  => "Доставлено",
            OrderStatus.Cancelled  => "Скасовано",
            _                      => s.ToString()
        };

        public static string DisplayPaymentMethod(this OrderPaymentMethod m) => m switch
        {
            OrderPaymentMethod.PayNow        => "Оплатити зараз",
            OrderPaymentMethod.PayOnDelivery => "Оплата при отриманні",
            OrderPaymentMethod.PayByCard     => "Карткою у відділенні",
            _                                => m.ToString()
        };

        public static string DisplayPaymentStatus(this OrderPaymentStatus s) => s switch
        {
            OrderPaymentStatus.Pending  => "Очікує оплати",
            OrderPaymentStatus.Paid     => "Оплачено",
            OrderPaymentStatus.Failed   => "Помилка оплати",
            OrderPaymentStatus.Refunded => "Повернено",
            _                           => s.ToString()
        };

        public static string DisplayDeliveryMethod(this OrderDeliveryMethod d) => d switch
        {
            OrderDeliveryMethod.SelfPickup         => "Самовивіз BAZA-R",
            OrderDeliveryMethod.NovaPoshta         => "Нова Пошта",
            OrderDeliveryMethod.UkrPoshta          => "Укрпошта",
            OrderDeliveryMethod.CourierNovaPoshta  => "Кур'єр Нова Пошта",
            _                                      => d.ToString()
        };
    }
}
