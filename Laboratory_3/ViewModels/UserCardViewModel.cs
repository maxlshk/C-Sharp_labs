using System.ComponentModel;
using System.Runtime.CompilerServices;
using KMA.ProgrammingInCSharp.Lab3.Models;
using KMA.ProgrammingInCSharp.Lab3.Navigation;
using KMA.ProgrammingInCSharp.Lab3.Tools;

namespace KMA.ProgrammingInCSharp.Lab3.ViewModels
{
    class UserCardViewModel : INotifyPropertyChanged, INavigatable<MainNavigationTypes>
    {
        #region Fields
        private Person _person;

        private readonly MainWindowViewModel _mainWindowViewModel;

        public event PropertyChangedEventHandler? PropertyChanged;

        private RelayCommand<object> _gotoSignInCommand;
        private Action _gotoSignIn;
        #endregion

        #region Properties
        public string Information
        {
            get { return Person.ToString(); }
        }

        public Person Person
        {
            get { return _mainWindowViewModel.CurrentPerson; }
            set
            {
                _person = value;
                OnPropertyChanged("Person");
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

        public MainNavigationTypes ViewType
        {
            get { return MainNavigationTypes.UserCard; }
        }
        #endregion

        public UserCardViewModel(MainWindowViewModel mainWindowViewModel, Action gotoSignIn)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _gotoSignIn = gotoSignIn;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
