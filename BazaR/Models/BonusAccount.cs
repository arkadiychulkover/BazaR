using System;

namespace BazaR.Models
{
    public class BonusAccount
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int TotalBalance { get; set; } = 0;

        public int MonthlyAccrued { get; set; } = 0;

        public int MonthlySpent { get; set; } = 0;

        public int AccrualRate { get; set; } = 1; // 1 бонус за 1 грн

        public DateTime? ExpirationDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
