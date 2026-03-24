namespace BazaR.Models
{
    public class UserUseStatistick
    {
        public int Id { get; set; }
        public DateTime LastSeen { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public UserUseStatistick() 
        { }

        public UserUseStatistick(User us) 
        {
            User = us;
            UserId = us.Id;
            LastSeen = DateTime.Now;
        }
    }
}
