using System;

namespace BazaR.Models
{
    public class MailingSetting
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public bool NewsAndUpdates { get; set; } = true;

        public bool SpecialOffers { get; set; } = true;

        public bool PersonalRecommendations { get; set; } = true;

        public bool ProductAlerts { get; set; } = false;

        public bool WeeklyDigest { get; set; } = false;

        public string PreferredFrequency { get; set; } = "weekly"; // daily, weekly, monthly

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public DateTime LastMailingSentAt { get; set;} = DateTime.UtcNow;
    }
}
