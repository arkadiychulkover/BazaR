using System;

namespace BazaR.Models
{
    public class PremiumSubscription
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public bool IsActive { get; set; } = false;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string PlanType { get; set; } = "basic"; // basic, standard, premium

        public decimal MonthlyPrice { get; set; }

        public bool AutoRenewal { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
