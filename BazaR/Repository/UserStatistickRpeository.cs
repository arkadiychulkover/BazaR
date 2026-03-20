using BazaR.Data;
using BazaR.Interfaces;
using BazaR.Models;
using Microsoft.EntityFrameworkCore;

namespace BazaR.Repository
{
    public class UserStatistickRpeository : IUserStatistick
    {
        private readonly AppDbContext _con;

        public UserStatistickRpeository(AppDbContext con)
        {
            _con = con;
        }

        public async Task<User> AddUser(User us)
        {
            var stat = await _con.UserUseStatisticks
                .FirstOrDefaultAsync(x => x.UserId == us.Id);

            if (stat == null)
            {
                stat = new UserUseStatistick(us);
                await _con.UserUseStatisticks.AddAsync(stat);
            }
            else
            {
                stat.LastSeen = DateTime.Now;
            }

            await _con.SaveChangesAsync();

            return us;
        }

        public int GetUsersCountForDayAsync()
        {
            var since = DateTime.Now.AddHours(-24);

            return _con.UserUseStatisticks
                .Where(u => u.LastSeen >= since)
                .Count();
        }

        public int GetUsersCountForWeekAsync()
        {
            var since = DateTime.Now.AddDays(-7);

            return _con.UserUseStatisticks
                .Where(u => u.LastSeen >= since)
                .Count();
        }

        public int GetUsersCountForMonthAsync()
        {
            var since = DateTime.Now.AddMonths(-1);

            return _con.UserUseStatisticks
                .Where(u => u.LastSeen >= since)
                .Count();
        }
    }
}