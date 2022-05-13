using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using InputMvvm.Models;
using InputMvvm.Services;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly:Dependency(typeof(UserService))]
namespace InputMvvm.Services
{
    public class UserService : IUserService
    {
        SQLiteAsyncConnection db;
        async Task Init()
        {
            if (db != null)
            {
                return;
            }

            // Get path to database
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "InputMVVMData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<User>();
        }

        public async Task AddUser(string username, string catname, string dogname)
        {
            await Init();
            var user = new User
            {
                Username = username,
                Catname = catname,
                Dogname = dogname
            };

            await db.InsertAsync(user);
        }

        public async Task RemoveUser(int id)
        {
            await Init();

            await db.DeleteAsync<User>(id);
        }

        //public async Task GetUser(string username)
        //{
        //    await Init();

        //    var query = db.Table<User>().Where(u => u.Username.Equals(username));

        //    await query.FirstOrDefaultAsync();
        //}

        public async Task<User> GetUser(string username)
        {
            var user = from u in db.Table<User>()
                        where u.Username.Equals(username)
                        select u;

            return await user.FirstOrDefaultAsync();
            
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            await Init();

            var users = await db.Table<User>().ToListAsync();
            return users;
        }
    }
}
