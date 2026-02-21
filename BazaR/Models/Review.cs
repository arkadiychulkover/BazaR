namespace BazaR.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int Rating { get; set; }

        public string coment { get; set; }

        public DateTime date { get; set; }

        public bool IsAproved { get; set; }

        public int UserId { get; set; }
        public int TovarId { get; set; }

        public Tovar Tovar { get; set; }
        public User User { get; set; }
    }
}
