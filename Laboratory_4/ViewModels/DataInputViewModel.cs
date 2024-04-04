using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using KMA.ProgrammingInCSharp.Lab4.Models;
using KMA.ProgrammingInCSharp.Lab4.Navigation;
using KMA.ProgrammingInCSharp.Lab4.Repository;
using KMA.ProgrammingInCSharp.Lab4.Tools;
using KMA.ProgrammingInCSharp.Lab4.Tools.Exceptions;

namespace KMA.ProgrammingInCSharp.Lab4.ViewModels
{
    class DataInputViewModel : INavigatable<MainNavigationTypes>, INotifyPropertyChanged
    {
        #region Fields

        private Person? _person;
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _date;

        private bool _active = true;
        private bool _enableButton = true;

        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly MainWindowViewModel _mainWindowViewModel;

        private RelayCommand<object> _signInCommand;
        private RelayCommand<object> _cancelCommand;
        private Action _gotoMain;
        #endregion

        #region Properties
        public Person? Person
        {
            get { return _person; }
            set
            {
                _person = value;
                OnPropertyChanged("Person");
            }
        }
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
        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                OnPropertyChanged("Active");
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

        public RelayCommand<object> SignInCommand
        {
            get { return _signInCommand ??= new RelayCommand<object>(InfomationProceedCommand, _ => CanExecute()); }
        }

        public RelayCommand<object> CancelCommand
        {
            get { return _cancelCommand ??= new RelayCommand<object>(_ =>  _gotoMain.Invoke()); }
        }

        public MainNavigationTypes ViewType
        {
            get { return MainNavigationTypes.DataInput; }
        }

        #endregion

        public DataInputViewModel(MainWindowViewModel mainWindowViewModel, Action gotoMain)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _gotoMain = gotoMain;
            UpdatePersonDetails(_mainWindowViewModel.CurrentPerson);
            
            _mainWindowViewModel.CurrentPersonChanged += MainWindowViewModel_CurrentPersonChanged;
        }

        private void MainWindowViewModel_CurrentPersonChanged(object sender, EventArgs e)
        {
            UpdatePersonDetails(_mainWindowViewModel.CurrentPerson);
        }

        private void UpdatePersonDetails(Person? person)
        {
            if (person != null)
            {
                Person = person;
                Name = Person.Name;
                Surname = Person.Surname;
                Email = Person.Email;
                Date = Person.BirthDay;
            }
            else
            {
                Person = null;
                Name = string.Empty;
                Surname = string.Empty;
                Email = string.Empty;
                Date = DateTime.Today;
            }
        }

        private async void InfomationProceedCommand(object obj)
        {
            try
            {
                Active = false;
                ProceedEnabled = false;
                await Task.Run(() =>
                {
                    Thread.Sleep(500);
                    Person person = new Person(Name, Surname, Email, Date);

                    if (Person == null)
                    {
                        StorageManager.Storage.AddPerson(person);
                        StorageManager.NotifyStorageUpdated();
                    }
                    else
                    {
                        var message = MessageBox.Show($"Submit changes in {Person.Name}?", "Edit", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                        if (message == MessageBoxResult.OK)
                        {
                            StorageManager.Storage.EditPerson(Person, person);
                            StorageManager.NotifyStorageUpdated();
                        }

                    }
                    
                    _gotoMain.Invoke();
                    Thread.Sleep(500);
                });
            }
            catch (InvalidEmailException ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(ex.Message);
                Email = "";
            }
            catch (InvalidBirthdayException ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(ex.Message);
                Date = DateTime.MinValue;
            }
            finally
            {
                Active = true;
                ProceedEnabled = true;
            }
            
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
