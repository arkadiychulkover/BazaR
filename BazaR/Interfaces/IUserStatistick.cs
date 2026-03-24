using BazaR.Data;
using BazaR.Models;

namespace BazaR.Interfaces
{
    public interface IUserStatistick
    {
        public int GetUsersCountForMonthAsync();
        public int GetUsersCountForWeekAsync();
        public int GetUsersCountForDayAsync();
        public Task<Dictionary<Category, int>> GetPopularCategoryAsync();
        public Task<CategoryStatistik> AddUserCategoryVisit(User us, int cat);
        public Task<UserUseStatistick> AddUser(User us);
    }
}
