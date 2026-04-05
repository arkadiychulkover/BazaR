using BazaR.Models;

namespace BazaR.Interfaces
{
    public interface IMailingGeneratorService
    {
        List<(string Title, string Text)> GenerateMessages(MailingSetting settings, User user);
    }
}