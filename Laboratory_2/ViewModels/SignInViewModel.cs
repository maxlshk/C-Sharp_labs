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

        private bool _enableButton = true;
        private bool _active = true;

        public event PropertyChangedEventHandler? PropertyChanged;

        private RelayCommand<object> _signInCommand;
        private RelayCommand<object> _cancelCommand;
        private Action _gotoMain;
        #endregion

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
        public bool ProceedEnabled
        {
            get { return _enableButton; }
            set
            {
                _enableButton = value;
                OnPropertyChanged("ProceedEnabled");
            }
        }

        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                OnPropertyChanged("Active");
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

        public SignInViewModel(Action gotoMain)
        {
            _gotoMain = gotoMain;
        }

        private async void InfomationProceedCommand(object obj)
        {
            await Task.Run(() =>
            {
                Active = false;
                ProceedEnabled = false;
                Thread.Sleep(500);
                Person = new Person(Name, Surname, Email, Date);
                if (Person.ValidBirthday())
                {
                    Person.CurrentPerson = Person;
                    _gotoMain.Invoke();
                }
                else
                {
                    MessageBox.Show("Invalid birthdate!");
                }
                Thread.Sleep(500);
                Active = true;
                ProceedEnabled = true;
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
}
