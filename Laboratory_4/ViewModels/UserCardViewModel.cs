using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using KMA.ProgrammingInCSharp.Lab4.Models;
using KMA.ProgrammingInCSharp.Lab4.Navigation;
using KMA.ProgrammingInCSharp.Lab4.Repository;
using KMA.ProgrammingInCSharp.Lab4.Tools;

namespace KMA.ProgrammingInCSharp.Lab4.ViewModels
{
     class UserCardViewModel : INotifyPropertyChanged, INavigatable<MainNavigationTypes>
    {
        #region Fields
        private ObservableCollection<Person> _persons;
        private Person _selectedPerson;
        public event PropertyChangedEventHandler? PropertyChanged;
    
        private RelayCommand<object> _addPersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _deletePersonCommand;
        private RelayCommand<object> _savePersonsCommand;
        
        private RelayCommand<object> _sortName;
        private RelayCommand<object> _sortSurname;
        private RelayCommand<object> _sortEmail;
        private RelayCommand<object> _sortBirthDay;
    
        private RelayCommand<object> _sortIsAdult;
        private RelayCommand<object> _sortIsBirthday;
        private RelayCommand<object> _sortSun;
        private RelayCommand<object> _sortChinese;
        
        private readonly MainWindowViewModel _mainWindowViewModel;
        private RelayCommand<object> _gotoSignInCommand;
        private Action _gotoSignIn;
        #endregion
    
        #region Properties
        public Person SelectedPerson
        {
            get 
            { 
                return _selectedPerson; 
            }
            set
            {
                _selectedPerson = value;
            }
        }
        public ObservableCollection<Person> Persons
        {
            get 
            { 
                return _persons; 
            }
            private set
            {
                _persons = value;
                OnPropertyChanged("Persons");
            }
        }
        public RelayCommand<object> AddCommand
        {
            get { return _addPersonCommand ??= new RelayCommand<object>(AddPersonCommandProcess, _ => true); }
        }
        public RelayCommand<object> EditCommand
        {
            get { return _editPersonCommand ??= new RelayCommand<object>(EditPersonCommandProcess, _ => CanExecuteCommand()); }
        }
        public RelayCommand<object> DeleteCommand
        {
            get { return _deletePersonCommand ??= new RelayCommand<object>(DeletePersonCommandProcess, _ => CanExecuteCommand()); }
        }
        public RelayCommand<object> SortNameCommand
        {
            get { return _sortName ??= new RelayCommand<object>(SortNameProcess, _ => true); }
        }
        public RelayCommand<object> SortSurnameCommand
        {
            get { return _sortSurname ??= new RelayCommand<object>(SortSurnameProcess, _ => true); }
        }
        public RelayCommand<object> SortEmailCommand
        {
            get { return _sortEmail ??= new RelayCommand<object>(SortEmailProcess, _ => true); }
        }
        public RelayCommand<object> SortBirthDayCommand
        {
            get { return _sortBirthDay ??= new RelayCommand<object>(SortBirthDayProcess, _ => true); }
        }
        public RelayCommand<object> SortIsAdultCommand
        {
            get { return _sortIsAdult ??= new RelayCommand<object>(SortIsAdultProcess, _ => true); }
        }
        public RelayCommand<object> SortIsBirthdayCommand
        {
            get { return _sortIsBirthday ??= new RelayCommand<object>(SortIsBirthDayProcess, _ => true); }
        }
        public RelayCommand<object> SortChineseSignCommand
        {
            get { return _sortChinese ??= new RelayCommand<object>(SortChineseSignProcess, _ => true); }
        }
        public RelayCommand<object> SortSunSignCommand
        {
            get { return _sortSun ??= new RelayCommand<object>(SortSunSignProcess, _ => true); }
        }
        #endregion
    
        private void AddPersonCommandProcess(object obj)
        {
            _mainWindowViewModel.CurrentPerson = null;
            _gotoSignIn.Invoke();
        }
    
        private void EditPersonCommandProcess(object obj)
        {   
            _mainWindowViewModel.CurrentPerson = SelectedPerson;
            _gotoSignIn.Invoke();
        }
    
        private async void DeletePersonCommandProcess(object obj)
        {
            await Task.Run(() =>
            {
                var message = MessageBox.Show($"Delete {_selectedPerson.Name}?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (message == MessageBoxResult.Yes)
                {
                    StorageManager.Storage.RemovePerson(SelectedPerson);
                    SelectedPerson = null;
                    Persons = new ObservableCollection<Person>(StorageManager.Storage.PersonsList);
                }
            });
        }
    
        #region sorting
        private void SortNameProcess(object obj)
        {
            IOrderedEnumerable<Person> persons = from person in _persons orderby person.Name select person;
            Persons = new ObservableCollection<Person>(persons);
            StorageManager.Storage.PersonsList = Persons.ToList();
        }
    
        private void SortSurnameProcess(object obj)
        {
            IOrderedEnumerable<Person> persons = from person in _persons orderby person.Surname select person;
            Persons = new ObservableCollection<Person>(persons);
            StorageManager.Storage.PersonsList = Persons.ToList();
        }
    
        private void SortEmailProcess(object obj)
        {
            IOrderedEnumerable<Person> persons = from person in _persons orderby person.Email select person;
            Persons = new ObservableCollection<Person>(persons);
            StorageManager.Storage.PersonsList = Persons.ToList();
        }
    
        private void SortBirthDayProcess(object obj)
        {
            IOrderedEnumerable<Person> persons = from person in _persons orderby person.BirthDay select person;
            Persons = new ObservableCollection<Person>(persons);
            StorageManager.Storage.PersonsList = Persons.ToList();
        }
    
        private void SortIsAdultProcess(object obj)
        {
            IOrderedEnumerable<Person> persons = from person in _persons orderby person.IsAdult select person;
            Persons = new ObservableCollection<Person>(persons);
            StorageManager.Storage.PersonsList = Persons.ToList();
        }
        private void SortIsBirthDayProcess(object obj)
        {
            IOrderedEnumerable<Person> persons = from person in _persons orderby person.IsBirthday select person;
            Persons = new ObservableCollection<Person>(persons);
            StorageManager.Storage.PersonsList = Persons.ToList();
        }
        private void SortChineseSignProcess(object obj)
        {
            IOrderedEnumerable<Person> persons = from person in _persons orderby person.ChineseSign select person;
            Persons = new ObservableCollection<Person>(persons);
            StorageManager.Storage.PersonsList = Persons.ToList();
        }
        private void SortSunSignProcess(object obj)
        {
            IOrderedEnumerable<Person> persons = from person in _persons orderby person.SunSign select person;
            Persons = new ObservableCollection<Person>(persons);
            StorageManager.Storage.PersonsList = Persons.ToList();
        }
        #endregion
    
        #region Constructors
        
        public UserCardViewModel(MainWindowViewModel mainWindowViewModel, Action gotoSignIn)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _gotoSignIn = gotoSignIn;
            
            _persons = new ObservableCollection<Person>(StorageManager.Storage.PersonsList);
            StorageManager.StorageUpdated += OnStorageUpdated;
        }
        #endregion
        
        private void OnStorageUpdated(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Persons = new ObservableCollection<Person>(StorageManager.Storage.PersonsList);
            });
        }
    
        private bool CanExecuteCommand()
        {
            return _selectedPerson != null;
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
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    
    
    // class UserCardViewModel : INotifyPropertyChanged, INavigatable<MainNavigationTypes>
    // {
    //     #region Fields
    //     private Person _person;
    //
    //     private readonly MainWindowViewModel _mainWindowViewModel;
    //
    //     public event PropertyChangedEventHandler? PropertyChanged;
    //
    //     private RelayCommand<object> _gotoSignInCommand;
    //     private Action _gotoSignIn;
    //     #endregion
    //
    //     #region Properties
    //     public string Information
    //     {
    //         get { return Person.ToString(); }
    //     }
    //
    //     public Person Person
    //     {
    //         get { return _mainWindowViewModel.CurrentPerson; }
    //         set
    //         {
    //             _person = value;
    //             OnPropertyChanged("Person");
    //         }
    //     }
    //
    //     public RelayCommand<object> GotoSignIn
    //     {
    //         get
    //         {
    //             return _gotoSignInCommand ??= new RelayCommand<object>(_ => GotoSignUp());
    //         }
    //     }
    //
    //     private void GotoSignUp()
    //     {
    //         _gotoSignIn.Invoke();
    //     }
    //
    //     public MainNavigationTypes ViewType
    //     {
    //         get { return MainNavigationTypes.UserCard; }
    //     }
    //     #endregion
    //
    //     public UserCardViewModel(MainWindowViewModel mainWindowViewModel, Action gotoSignIn)
    //     {
    //         _mainWindowViewModel = mainWindowViewModel;
    //         _gotoSignIn = gotoSignIn;
    //     }
    //
    //     public void OnPropertyChanged([CallerMemberName] string prop = "")
    //     {
    //         if (PropertyChanged != null)
    //             PropertyChanged(this, new PropertyChangedEventArgs(prop));
    //     }
    // }
}
