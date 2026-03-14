using System;

namespace BazaR.Models
{
    public class Wallet
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public decimal Balance { get; set; } = 0;

        public decimal MonthlySpent { get; set; } = 0;

        public DateTime? LastReplenishment { get; set; }

        public decimal LastReplenishmentAmount { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
