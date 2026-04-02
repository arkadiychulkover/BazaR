using BazaR.Models;

namespace BazaR.ViewModels
{
    public class MessageVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public bool IsRead { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
    }

    public class AccountMessagesViewModel
    {
        public AccountProfileViewModel Profile { get; set; } = new();
        public List<MessageVm> Messages { get; set; } = new();
        public int NewMessagesCount { get; set; }
    }
}