using System;

namespace BazaR.Models
{
    public class LookedCard
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public bool IsLooked { get; set; } = false;

        public DateTime LookedAt { get; set; } = DateTime.UtcNow;

        public int ViewCount { get; set; } = 1;
    }
}
