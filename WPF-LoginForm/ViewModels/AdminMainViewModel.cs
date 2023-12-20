using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.Views;

namespace WPF_LoginForm.ViewModels
{
    internal class AdminMainViewModel:ViewModelBase
    {
        //Fields
        private UserModel _currentUser = new UserModel();
        private UserAccountModel _currentUserAccount;
        private IUserRepository userRepository;
        private List<UserModel> _users;

        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public List<UserModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        //Properties
        public string Username
        {
            get
            {
                return _currentUser.Username;
            }

            set
            {
                if (_currentUser.Username != value)
                {
                    _currentUser.Username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public string Password
        {
            get
            {
                return _currentUser.Password;
            }

            set
            {
                if (_currentUser.Password != value)
                {
                    _currentUser.Password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public string Name
        {
            get
            {
                return _currentUser.Name;
            }

            set
            {
                if (_currentUser.Name != value)
                {
                    _currentUser.Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Lastname
        {
            get
            {
                return _currentUser.LastName;
            }

            set
            {
                if (_currentUser.LastName != value)
                {
                    _currentUser.LastName = value;
                    OnPropertyChanged(nameof(Lastname));
                }
            }
        }

        public string Email
        {
            get
            {
                return _currentUser.Email;
            }

            set
            {
                if (_currentUser.Email != value)
                {
                    _currentUser.Email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public int AccessLevel
        {
            get { return _currentUser.AccessLevel; }
            set { _currentUser.AccessLevel = value; OnPropertyChanged(nameof(AccessLevel));}
        }

        public ICommand AddUserCommand { get; private set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand EditUserCommand { get; private set; }
        public ICommand RemoveUserCommand { get; private set; }

        public AdminMainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            AddUserCommand = new ViewModelCommand(ExecuteAddUserCommand, CanExecuteCommand);
            EditUserCommand = new ViewModelCommand(ExecuteEditUserCommand, CanExecuteCommand);
            RemoveUserCommand = new ViewModelCommand(ExecuteRemoveUserCommand);
            RefreshCommand = new ViewModelCommand(ExecuteRefreshCommand);
            Users = userRepository.GetByAll();
            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.AccessLevel = user.AccessLevel;
                CurrentUserAccount.DisplayName = $"Welcome {user.Name} {user.LastName}!";
                CurrentUserAccount.ProfilePicture = null;
            }
            else
            {
                CurrentUserAccount.DisplayName = "Invalid user, not logged in";
            }
        }

        private bool CanExecuteCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }

        private void ExecuteAddUserCommand(object obj)
        {
            userRepository.Add(_currentUser);
        }

        private void ExecuteEditUserCommand(object obj)
        {
            userRepository.Edit(_currentUser);
        }

        private void ExecuteRemoveUserCommand(object obj)
        {
            userRepository.Remove(_currentUser.Username, _currentUser.Name, _currentUser.LastName);
        }
        private void ExecuteRefreshCommand(object obj)
        {
            AdminMainView view = new AdminMainView();
            view.Show();
            Application.Current.Windows[0].Close();
            
        }
    }
}
