namespace BazaR.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public bool IsRead { get; set; } = false;

        public int SenderId { get; set; }
        public string SenderName { get; set; }
    }
}
