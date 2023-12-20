using System.Security.Principal;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.Views;

namespace WPF_LoginForm.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        //Fields
        private UserModel _currentUser = new UserModel();
        private string _errorMessage;

        private IUserRepository userRepository;

        //Properties

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }

            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

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

        //-> Commands
        public ICommand RegistrationCommand { get; private set; }

        //Constructor
        public RegistrationViewModel()
        {
            userRepository = new UserRepository();
            RegistrationCommand = new ViewModelCommand(ExecuteRegistrationCommand, CanExecuteRegistrationCommand);
        }

        private bool CanExecuteRegistrationCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }

        private void ExecuteRegistrationCommand(object obj)
        {
            userRepository.RegistrationUser(_currentUser);
            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);
            MainView mainView = new MainView();
            mainView.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
