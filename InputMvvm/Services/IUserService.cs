using System.Collections.Generic;
using System.Threading.Tasks;
using InputMvvm.Models;

namespace InputMvvm.Services
{
    public interface IUserService
    {
        Task AddUser(string username, string catname, string dogname);
        Task<IEnumerable<User>> GetUsers();
        Task RemoveUser(int id);
        Task<int> GetUserCount(string username);
    }
}