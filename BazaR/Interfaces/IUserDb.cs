using BazaR.Models;

namespace BazaR.Interfaces
{
    public interface IUserDb
    {
        public bool AddUser(User us);
        public User Delete(int Id);
        public User UpdateUser(User us);
        public bool AddAdminRights(int Id);
        public IQueryable<User> GetAllUsers();
        public User GetUser(int Id);

    }
}
