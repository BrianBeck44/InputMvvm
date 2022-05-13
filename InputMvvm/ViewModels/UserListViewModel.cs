using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using InputMvvm.Models;
using InputMvvm.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace InputMvvm.ViewModels
{
    public class UserListViewModel : BaseViewModel
    {
        public ObservableRangeCollection<User> Users { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<User> RemoveCommand { get; }

        IUserService userService;

        public UserListViewModel()
        {
            Users = new ObservableRangeCollection<User>();

            RefreshCommand = new AsyncCommand(Refresh);
            RemoveCommand = new AsyncCommand<User>(Remove);

            userService = DependencyService.Get<IUserService>();
        }

        async Task Remove(User user)
        {
            await userService.RemoveUser(user.Id);
            await Refresh();
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Users.Clear();

            var users = userService.GetUsers();

            Users.AddRange((System.Collections.Generic.IEnumerable<User>)users);

            IsBusy = false;
        }
    }
}
