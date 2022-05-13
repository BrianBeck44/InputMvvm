using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using InputMvvm.Models;
using InputMvvm.Services;
using InputMvvm.Validations;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace InputMvvm.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        string username, catname, dogname, errorUsernameMessage, errorCatnameMessage, errorDognameMessage,
            successUsernameMessage, successCatnameMessage, successDognameMessage;
                
        bool isErrorUsername, isErrorCatname, isErrorDogname, isVisible;
                
        public string Username { get => username; set => SetProperty(ref username, value); }
        public string Catname { get => catname; set => SetProperty(ref catname, value); }
        public string Dogname { get => dogname; set => SetProperty(ref dogname, value); }

        public string SuccessUsernameMessage { get => successUsernameMessage; set => SetProperty(ref successUsernameMessage, value); }
        public string SuccessCatnameMessage { get => successCatnameMessage; set => SetProperty(ref successCatnameMessage, value); }
        public string SuccessDognameMessage { get => successDognameMessage; set => SetProperty(ref successDognameMessage, value); }

        public string ErrorUsernameMessage { get => errorUsernameMessage; set => SetProperty(ref errorUsernameMessage, value); }
        public string ErrorCatnameMessage { get => errorCatnameMessage; set => SetProperty(ref errorCatnameMessage, value); }
        public string ErrorDognameMessage { get => errorDognameMessage; set => SetProperty(ref errorDognameMessage, value); }

        public bool IsErrorUsername { get => isErrorUsername; set => SetProperty(ref isErrorUsername, value); }
        public bool IsErrorCatname { get => isErrorCatname; set => SetProperty(ref isErrorCatname, value); }
        public bool IsErrorDogname { get => isErrorDogname; set => SetProperty(ref isErrorDogname, value); }

        public bool IsVisible { get => isVisible; set => SetProperty(ref isVisible, value); }

        public AsyncCommand SaveCommand { get; }
        public AsyncCommand CancelCommand { get; }

        IUserService userService;

        public UserViewModel()
        {
            Title = "User Entry Page";
            SaveCommand = new AsyncCommand(Save);
            CancelCommand = new AsyncCommand(Cancel);

            IsErrorUsername = false;
            IsErrorCatname = false;
            IsErrorDogname = false;

            //getting the global instance of UserService through Xamarin.Forms dependency injection
            userService = DependencyService.Get<IUserService>();
        }
         
         async Task Save()
        {
            IsVisible = false;
            IsErrorUsername = false;
            IsErrorCatname = false;
            IsErrorDogname = false;


            ValidateUsername();
            ValidateCatname();
            ValidateDogname();

            // check for duplicate username
            // Task<User> user = userService.GetUser(Username);

            //if (user.AsyncState != null)
            //{
            //    // duplicate
            //    IsErrorUsername = true;
            //    ErrorUsernameMessage = "Username must be unique. Username is already being used";
            //    return;
            //}

            if (!IsErrorUsername && !IsErrorCatname && !IsErrorDogname)
            {
                // await userService.AddUser(Username, Catname, Dogname);

                SuccessUsernameMessage = "Username: " + Username;
                SuccessCatnameMessage = "Cat's name:" + Catname;
                SuccessDognameMessage = "Dog's name:" + Dogname;

                IsVisible = true;
            }            
        }

         async Task Cancel()
        {
            IsVisible = false;

            Username = string.Empty;
            Catname = string.Empty;
            Dogname = string.Empty;

            IsErrorUsername = false;
            IsErrorCatname = false;
            IsErrorDogname = false;

            return;
        }

        public void ValidateUsername()
        {
            if (string.IsNullOrEmpty(Username))
            {
                IsErrorUsername = true;
                ErrorUsernameMessage = "Username can not be empty";
                return;
            }
        }

        public void ValidateCatname()
        {
            if (string.IsNullOrEmpty(Catname))
            {
                IsErrorCatname = true;
                ErrorCatnameMessage = "Cat name can not be empty";
                return;
            }            
        }

        public void ValidateDogname()
        {
            if (string.IsNullOrEmpty(Dogname))
            {
                IsErrorDogname = true;
                ErrorDognameMessage = "Dog name can not be empty";
                return;
            }
        }
    }
}
