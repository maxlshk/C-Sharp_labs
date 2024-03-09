using System.ComponentModel;
using System.Runtime.CompilerServices;
using KMA.ProgrammingInCSharp.Lab2.Models;
using KMA.ProgrammingInCSharp.Lab2.Navigation;
using KMA.ProgrammingInCSharp.Lab2.Tools;

namespace KMA.ProgrammingInCSharp.Lab2.ViewModels
{
    class MainViewModel : INotifyPropertyChanged, INavigatable<MainNavigationTypes>
    {
        #region Fields
        private string _information = "";
        private Person _person;

        public event PropertyChangedEventHandler? PropertyChanged;

        private RelayCommand<object> _gotoSignInCommand;
        private Action _gotoSignIn;
        #endregion

        #region Properties
        public string Information
        {
            get { return Person.ToString(); }
            set
            {
                _information = value;
                OnPropertyChanged("Information");
            }
        }

        public Person Person
        {
            get { return Person.CurrentPerson; }
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
            get { return MainNavigationTypes.Main; }
        }
        #endregion


        public MainViewModel(Action gotoSignIn)
        {
            _gotoSignIn = gotoSignIn;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    //{
    //    public event PropertyChangedEventHandler? PropertyChanged;

    //    protected virtual void OnPropertyChanged(string? propertyName = null)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }
    //    public MainViewModel(Action gotoSignIn)
    //    {
    //        _gotoSignIn = gotoSignIn;
    //        User.CurrentUserChanged += User_CurrentUserChanged;
    //    }

    //    private void User_CurrentUserChanged(object? sender, EventArgs e)
    //    {
    //        _currUser = User.CurrentUser;
    //        OnPropertyChanged(nameof(CurrUser));
    //        OnPropertyChanged(nameof(BirthdayMessage));
    //    }

    //    public void Cleanup()
    //    {
    //        User.CurrentUserChanged -= User_CurrentUserChanged;
    //    }

    //    private User? _currUser = User.CurrentUser;
    //    private string? _birthdayMessage;
    //    private RelayCommand<object> _gotoSignInCommand;
    //    private Action _gotoSignIn;

    //    public User? CurrUser => _currUser;

    //    public string? BirthdayMessage
    //    {
    //        get
    //        {
    //            if (_currUser.IsBirthday)
    //            {
    //                return "Happy Birthday! :D";
    //            }
    //            return _birthdayMessage;
    //        }
    //    }


    //    public RelayCommand<object> GotoSignIn
    //    {
    //        get
    //        {
    //            return _gotoSignInCommand ??= new RelayCommand<object>(_ => GotoSignUp());
    //        }
    //    }

    //    private void GotoSignUp()
    //    {
    //        _gotoSignIn.Invoke();
    //    }

    //    public MainNavigationTypes ViewType => MainNavigationTypes.Main;
    //}
}
