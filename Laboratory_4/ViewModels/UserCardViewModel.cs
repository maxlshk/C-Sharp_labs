using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
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
        private ObservableCollection<Person> _allPersons;
        private Person _selectedPerson;
        
        private string _selectedFilterProperty;
        private string _filterValue = string.Empty;
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
        public string SelectedFilterProperty
        {
            get => _selectedFilterProperty;
            set
            {
                _selectedFilterProperty = value;
                OnPropertyChanged(nameof(SelectedFilterProperty));
                // No direct call to ApplyFilter here since FilterValue controls when filtering happens
            }
        }

        public string FilterValue
        {
            get => _filterValue;
            set
            {
                _filterValue = value;
                OnPropertyChanged(nameof(FilterValue));
                // We'll rely on the FilterCommand being triggered by the UI to apply the filter
            }
        }


        public RelayCommand<object> FilterCommand
        {
            get;
            set;
        }


        #region sorting

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
    
            var personsList = StorageManager.Storage.PersonsList; // Assuming this is your repository call
            _allPersons = new ObservableCollection<Person>(personsList);
            _persons = new ObservableCollection<Person>(_allPersons);
            StorageManager.StorageUpdated += OnStorageUpdated;

            // Initialize FilterCommand
            FilterCommand = new RelayCommand<object>(obj => ApplyFilter());
        }
        
        private void ApplyFilter()
        {
            var filtered = _allPersons.Where(p => FilterPredicate(p)).ToList();
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



        #endregion
        
        private void OnStorageUpdated(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var personsList = StorageManager.Storage.PersonsList; 
                _allPersons = new ObservableCollection<Person>(personsList);
                ApplyFilter(); 
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
}
