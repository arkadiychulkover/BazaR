using BazaR.Models;
using System.ComponentModel.DataAnnotations;

namespace BazaR.ViewModels
{
    public class SendMessageViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Введите тему")]
        [MaxLength(100, ErrorMessage = "Не более 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите текст сообщения")]
        [MaxLength(2000, ErrorMessage = "Не более 2000 символов")]
        public string Content { get; set; }
    }

    public class AdminMessageViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<Message> Messages { get; set; } = new();
        public SendMessageViewModel NewMessage { get; set; } = new();
    }
}
