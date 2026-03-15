namespace BazaR.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Name {get; set;}
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public string UserId { get; set; }
        public User? User { get; set; }
    }
}
