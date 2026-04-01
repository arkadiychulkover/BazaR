using BazaR.Data;
using BazaR.Interfaces;
using BazaR.Models;

namespace BazaR.Services
{
    public class UserMessageService : IUserMessageService
    {
        private readonly AppDbContext _db;

        public UserMessageService(AppDbContext db)
        {
            _db = db;
        }

        public async Task SendAsync(int userId, string title, string text, CancellationToken ct = default)
        {
            var message = new Message
            {
                UserId = userId,
                Name = title,
                Content = text,
                SenderId = 0,
                SenderName = "System",
                DateTime = DateTime.UtcNow,
                IsRead = false
            };

            _db.Messages.Add(message);
            await _db.SaveChangesAsync(ct);
        }
    }
}