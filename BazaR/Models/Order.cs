using System;
using System.Collections.Generic;

namespace BazaR.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string Number { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User? User { get; set; }

        public int CityId { get; set; }
        public City? City { get; set; }

        public string Address { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.New;
        public OrderPaymentMethod PaymentMethod { get; set; } = OrderPaymentMethod.PayNow;
        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public OrderDeliveryMethod DeliveryMethod { get; set; } = OrderDeliveryMethod.SelfPickup;

        public string? Ttn { get; set; }

        public decimal TotalAmount { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
