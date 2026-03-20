using BazaR.Data;
using BazaR.Models;

namespace BazaR.Interfaces
{
    public interface IUserStatistick
    {
        public int GetUsersCountForMonthAsync();
        public int GetUsersCountForWeekAsync();
        public int GetUsersCountForDayAsync();
        public Task<User> AddUser(User us);
    }
}
