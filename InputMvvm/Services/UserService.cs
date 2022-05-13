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

            if (string.IsNullOrEmpty(username))
                throw new Exception("Username is required");
            
            if (string.IsNullOrEmpty(catname))
                throw new Exception("Catname is required");

            if (string.IsNullOrEmpty(dogname))
                throw new Exception("Dogname is required");

            var user = new User
            {
                Username = username,
                Catname = catname,
                Dogname = dogname
            };

            try
            {
                await db.InsertAsync(user);
            }
            catch (Exception ex)
            {
                var Message = string.Format("Failed to add {0}. Error: {1}", username, ex.Message);
            }
            
        }

        public async Task RemoveUser(int id)
        {
            await Init();

            await db.DeleteAsync<User>(id);
        }

        public async Task<int> GetUserCount(string username)
        {
            await Init();

            var user = from u in db.Table<User>()
                       where u.Username.Equals(username)
                       select u;

            try
            {
                return await user.CountAsync();
            }
            catch (Exception ex)
            {
                var Message = string.Format("Failed to add {0}. Error: {1}", username, ex.Message);
            }

            return 0;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            await Init();

            var users = await db.Table<User>().ToListAsync();
            return users;
        }
    }
}
