// ActiveUsersService.cs
using BazaR.Models;

namespace BazaR.Services
{
    public class ActiveUsersService
    {
        private readonly Dictionary<int, DateTime> users = new();

        public Dictionary<int, DateTime> GetUsers() => users;

        public void PingUser(int id)
        {
            users[id] = DateTime.UtcNow;
        }

        public int GetOnlineUsersCount()
        {
            var threshold = DateTime.UtcNow.AddMinutes(-5);
            return users.Count(u => u.Value > threshold);
        }
    }
}