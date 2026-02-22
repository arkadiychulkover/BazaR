namespace BazaR.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime Date { get; set; }

        public bool IsApproved { get; set; }
    }
}
