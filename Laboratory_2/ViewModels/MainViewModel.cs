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
}
