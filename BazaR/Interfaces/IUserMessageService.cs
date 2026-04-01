namespace BazaR.Interfaces
{
    public interface IUserMessageService
    {
        Task SendAsync(int userId, string title, string text, CancellationToken ct = default);
    }
}