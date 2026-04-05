using BazaR.Models;
using BazaR.Models.BazaR.Models;

namespace BazaR.Interfaces
{
    public interface ILogDb
    {
        void Log(string message);

        Task LogPageVisitAsync(int userId, UserAction action,
            string? controller = null,
            string? actionName = null,
            int? itemId = null,
            int? orderId = null,
            SearchFilters? filters = null);
    }
}