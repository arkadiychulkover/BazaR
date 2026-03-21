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

        public async Task<UserUseStatistick> AddUser(User us)
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

            return stat;
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

        public async Task<Dictionary<Category, int>> GetPopularCategoryAsync()
        {
            var grouped = await _con.CategoryStatistiks
                .GroupBy(v => v.CategoryId)
                .Select(g => new
                {
                    CategoryId = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToListAsync();

            var categoryIds = grouped.Select(x => x.CategoryId).ToList();

            var categories = await _con.Categories
                .Where(c => categoryIds.Contains(c.Id))
                .ToListAsync();

            return grouped.ToDictionary(
                x => categories.First(c => c.Id == x.CategoryId),
                x => x.Count
            );
        }

        public async Task<CategoryStatistik> AddUserCategoryVisit(User us, int cat)
        {
            var visit = new CategoryStatistik
            {
                UserId = us.Id,
                CategoryId = cat,
                VisitedAt = DateTime.Now
            };

            await _con.CategoryStatistiks.AddAsync(visit);
            await _con.SaveChangesAsync();
            return visit;
        }
    }
}