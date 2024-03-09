using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using KMA.ProgrammingInCSharp.Lab2.Models;
using KMA.ProgrammingInCSharp.Lab2.Navigation;
using KMA.ProgrammingInCSharp.Lab2.Tools;

namespace KMA.ProgrammingInCSharp.Lab2.ViewModels
{
    class SignInViewModel : INavigatable<MainNavigationTypes>, INotifyPropertyChanged
    {
        #region Fields
        private Person _person;
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _date;

        //private string _information = "";
        private bool _enableButton = true;

        public event PropertyChangedEventHandler? PropertyChanged;

        private RelayCommand<object> _signInCommand;
        private RelayCommand<object> _cancelCommand;
        private Action _gotoMain;
        #endregion


        public SignInViewModel(Action gotoMain)
        {
            _gotoMain = gotoMain;
        }



        #region Properties
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }
        public Person Person
        {
            get { return _person; }
            set
            {
                _person = value;
                OnPropertyChanged("Person");
            }
        }
        //public string Information
        //{
        //    get { return _information; }
        //    set
        //    {
        //        _information = value;
        //        OnPropertyChanged("Information");
        //    }
        //}
        public bool ProceedEnabled
        {
            get { return _enableButton; }
            set
            {
                _enableButton = value;
                OnPropertyChanged("ProceedEnabled");
            }
        }

        public RelayCommand<object> SignInCommand
        {
            get { return _signInCommand ??= new RelayCommand<object>(InfomationProceedCommand, _ => CanExecute()); }
        }

        public RelayCommand<object> CancelCommand
        {
            get { return _cancelCommand ??= new RelayCommand<object>(_ => Environment.Exit(0)); }
        }

        public MainNavigationTypes ViewType
        {
            get { return MainNavigationTypes.SignIn; }
        }

        #endregion

        private async void InfomationProceedCommand(object obj)
        {
            //Information = "";
            await Task.Run(() =>
            {
                Thread.Sleep(500);
                Person = new Person(Name, Surname, Email, Date);
                if (Person.ValidBirthday())
                {
                    //Information = Person.ToString();
                    Person.CurrentPerson = Person;
                    _gotoMain.Invoke();
                }
                else
                {
                    MessageBox.Show("You have entered an incorrect date of birth, please try again");
                }
                Thread.Sleep(500);
            });
        }
        private bool CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Name)
                   && !string.IsNullOrWhiteSpace(Surname)
                   && !string.IsNullOrWhiteSpace(Email)
                   && Date != DateTime.MinValue;
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    //class SignInViewModel : INavigatable<MainNavigationTypes>
    //{
    //    #region Fields
    //    private UserCandidate _user = new UserCandidate();
    //    private RelayCommand<object> _signInCommand;
    //    private RelayCommand<object> _cancelCommand;
    //    private Action _gotoMain;

    //    public SignInViewModel(Action gotoMain)
    //    {
    //        _gotoMain = gotoMain;
    //    }

    //    #endregion

    //    #region Properties
    //    public DateTime Birthday
    //    {
    //        get
    //        {
    //            return _user.Birthday;
    //        }
    //        set
    //        {
    //            _user.Birthday = value;
    //        }
    //    }

    //    public RelayCommand<object> SignInCommand
    //    {
    //        get
    //        {
    //            return _signInCommand ??= new RelayCommand<object>(_ => SignIn());
    //        }
    //    }

    //    public RelayCommand<object> CancelCommand
    //    {
    //        get
    //        {
    //            return _cancelCommand ??= new RelayCommand<object>(_ => Environment.Exit(0));
    //        }
    //    }

    //    public MainNavigationTypes ViewType
    //    {
    //        get
    //        {
    //            return MainNavigationTypes.SignIn;
    //        }
    //    }
    //    #endregion

    //    private void SignIn()
    //    {
    //        if (!IsLegal())
    //            MessageBox.Show("Incorrect age!");
    //        else
    //        {
    //            User user = new User(_user);
    //            User.CurrentUser = user;
    //            MessageBox.Show($"Birthday {user.BirthDay} submitted!");
    //            _gotoMain.Invoke();
    //        }
    //    }

    //    private bool IsLegal()
    //    {
    //        return _user.Age > 0 && _user.Age < 135;
    //    }
    //}
}
