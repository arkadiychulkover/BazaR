using System;
namespace BazaR.Models
{
    public class UserPromotion
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PromotionId { get; set; }
        public Promotion Promotion { get; set; }
        public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
    }
}