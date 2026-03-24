using BazaR.Data;
using BazaR.Interfaces;
using BazaR.Models;
using BazaR.Models.BazaR.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace BazaR.Repository
{
    public class LogDbRepository : ILogDb
    {
        private readonly AppDbContext _con;
        public LogDbRepository(AppDbContext con)
        {
            _con = con;
        }

        public void Log(string message)
        {
            Console.WriteLine($"\n[LOG] {message} ]\n");
        }

        public async Task LogPageVisitAsync(int userId, UserAction action,
            string? controller = null,
            string? actionName = null,
            int? itemId = null,
            int? orderId = null,
            SearchFilters? filters = null)
        {
            var visit = new VisitingModel
            {
                UserId = userId,
                userAction = action,
                ControllerName = controller,
                ActionName = actionName,
                ItemId = itemId,
                OrderId = orderId,
                SearchFilters = filters
            };

            _con.VisitingModels.Add(visit);
            await _con.SaveChangesAsync();
        }
    }
}
