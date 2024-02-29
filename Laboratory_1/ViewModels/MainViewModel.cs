using KMA.ProgrammingInCSharp.Lab1.Models;
using KMA.ProgrammingInCSharp.Lab1.Navigation;
using KMA.ProgrammingInCSharp.Lab1.Tools;
using System.ComponentModel;
using System.Windows;

namespace KMA.ProgrammingInCSharp.Lab1.ViewModels
{
    class MainViewModel : INotifyPropertyChanged, INavigatable<MainNavigationTypes>
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainViewModel(Action gotoSignIn)
        {
            _gotoSignIn = gotoSignIn;
            User.CurrentUserChanged += User_CurrentUserChanged;
        }

        private void User_CurrentUserChanged(object? sender, EventArgs e)
        {
            _currUser = User.CurrentUser;
            OnPropertyChanged(nameof(CurrUser));
            OnPropertyChanged(nameof(BirthdayMessage));
        }

        public void Cleanup()
        {
            User.CurrentUserChanged -= User_CurrentUserChanged;
        }

        private User? _currUser = User.CurrentUser;
        private string? _birthdayMessage;
        private RelayCommand<object> _gotoSignInCommand;
        private Action _gotoSignIn;

        public User? CurrUser => _currUser;

        public string? BirthdayMessage
        {
            get
            {
                if (_currUser.IsBirthday)
                {
                    return "Happy Birthday! :D";
                }
                return _birthdayMessage;
            }
        }


        public RelayCommand<object> GotoSignIn
        {
            get
            {
                return _gotoSignInCommand ??= new RelayCommand<object>(_ => GotoSignUp());
            }
        }

        private void GotoSignUp()
        {
            _gotoSignIn.Invoke();
        }

        public MainNavigationTypes ViewType => MainNavigationTypes.Main;
    }
}
