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
     class UsersTableViewModel : INotifyPropertyChanged, INavigatable<MainNavigationTypes>
    {
        #region Fields
        private ObservableCollection<Person> _persons;
        private Person _selectedPerson;
        
        private string _selectedFilterProperty;
        private string _filterValue = string.Empty;
        public event PropertyChangedEventHandler? PropertyChanged;
    
        private RelayCommand<object> _addPersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _deletePersonCommand;
        
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
        public string SelectedFilterProperty
        {
            get => _selectedFilterProperty;
            set
            {
                _selectedFilterProperty = value;
                OnPropertyChanged(nameof(SelectedFilterProperty));
            }
        }
        public string FilterValue
        {
            get => _filterValue;
            set
            {
                _filterValue = value;
                OnPropertyChanged(nameof(FilterValue));
            }
        }
        #endregion

        #region RelayCommands
        public RelayCommand<object> FilterCommand
        {
            get;
            set;
        }
        public RelayCommand<object> GotoSignIn
        {
            get
            {
                return _gotoSignInCommand ??= new RelayCommand<object>(_ => _gotoSignIn.Invoke());
            }
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
        #endregion

        #region Processes

        private void AddPersonCommandProcess(object obj)
        {
            _mainWindowViewModel.CurrentPerson = null;
            _gotoSignIn.Invoke();
            ResetFiltersAndFetchAllPersons();
        }
    
        private void EditPersonCommandProcess(object obj)
        {   
            _mainWindowViewModel.CurrentPerson = SelectedPerson;
            _gotoSignIn.Invoke();
            ResetFiltersAndFetchAllPersons();
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
                    ApplyFilter();
                }
            });
        }
    
        #region SortingMethods
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

        #endregion
        
        #region Constructors
        public UsersTableViewModel(MainWindowViewModel mainWindowViewModel, Action gotoSignIn)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _gotoSignIn = gotoSignIn;
    
            var personsList = StorageManager.Storage.PersonsList;
            _persons = new ObservableCollection<Person>(personsList);
            StorageManager.StorageUpdated += OnStorageUpdated;

            FilterCommand = new RelayCommand<object>(obj => ApplyFilter());
        }
        #endregion

        #region Filtering
        private void ApplyFilter()
        {
            var personsList = StorageManager.Storage.PersonsList;
            if (string.IsNullOrWhiteSpace(_selectedFilterProperty) || string.IsNullOrWhiteSpace(_filterValue))
            {
                Persons = new ObservableCollection<Person>(personsList);
                return;
            }
            var filtered = personsList.Where(p => FilterPredicate(p)).ToList();
            Persons = new ObservableCollection<Person>(filtered);
        }
        
        private bool FilterPredicate(Person person)
        {
            var propertyInfo = typeof(Person).GetProperty(_selectedFilterProperty);
            if (propertyInfo == null)
            {
                Console.WriteLine($"Property '{_selectedFilterProperty}' not found.");
                return true;
            }

            var propertyValue = propertyInfo.GetValue(person, null)?.ToString();
            var matches = !string.IsNullOrWhiteSpace(_filterValue) && propertyValue?.IndexOf(_filterValue, StringComparison.OrdinalIgnoreCase) >= 0;
    
            Console.WriteLine($"Filtering {propertyInfo.Name} for value '{_filterValue}' - Match: {matches}");
            return matches;
        }

        private void ResetFiltersAndFetchAllPersons()
        {
            var personsList = StorageManager.Storage.PersonsList; 
            Persons = new ObservableCollection<Person>(personsList);

            _selectedFilterProperty = null;
            _filterValue = string.Empty;

            OnPropertyChanged(nameof(Persons));
            OnPropertyChanged(nameof(SelectedFilterProperty));
            OnPropertyChanged(nameof(FilterValue));
        }
        #endregion

        #region Listeners
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        
        private void OnStorageUpdated(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var personsList = StorageManager.Storage.PersonsList; 
                Persons = new ObservableCollection<Person>(personsList);
            });
        }
        #endregion
        private bool CanExecuteCommand()
        {
            return _selectedPerson != null;
        }
        public MainNavigationTypes ViewType
        {
            get { return MainNavigationTypes.UserCard; }
        }
    }
}
