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

        public string Status { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public string DeliveryMethod { get; set; } = string.Empty;

        public string? Ttn { get; set; }

        public decimal TotalAmount { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
