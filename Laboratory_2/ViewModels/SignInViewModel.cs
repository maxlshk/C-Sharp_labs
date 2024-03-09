using System.Windows;
using KMA.ProgrammingInCSharp.Lab1.Models;
using KMA.ProgrammingInCSharp.Lab1.Navigation;
using KMA.ProgrammingInCSharp.Lab1.Tools;

namespace KMA.ProgrammingInCSharp.Lab1.ViewModels
{
    class SignInViewModel : INavigatable<MainNavigationTypes>
    {
        #region Fields
        private UserCandidate _user = new UserCandidate();
        private RelayCommand<object> _signInCommand;
        private RelayCommand<object> _cancelCommand;
        private Action _gotoMain;

        public SignInViewModel(Action gotoMain)
        {
            _gotoMain = gotoMain;
        }

        #endregion

        #region Properties
        public DateTime Birthday
        {
            get
            {
                return _user.Birthday;
            }
            set
            {
                _user.Birthday = value;
            }
        }

        public RelayCommand<object> SignInCommand
        {
            get
            {
                return _signInCommand ??= new RelayCommand<object>(_ => SignIn());
            }
        }

        public RelayCommand<object> CancelCommand
        {
            get
            {
                return _cancelCommand ??= new RelayCommand<object>(_ => Environment.Exit(0));
            }
        }

        public MainNavigationTypes ViewType
        {
            get
            {
                return MainNavigationTypes.SignIn;
            }
        }
        #endregion

        private void SignIn()
        {
            if (!IsLegal())
                MessageBox.Show("Incorrect age!");
            else
            {
                User user = new User(_user);
                User.CurrentUser = user;
                MessageBox.Show($"Birthday {user.BirthDay} submitted!");
                _gotoMain.Invoke();
            }
        }

        private bool IsLegal()
        {
            return _user.Age > 0 && _user.Age < 135;
        }
    }
}
