using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
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
        private string _filterValue;
        private string _sorterName;
        public event PropertyChangedEventHandler? PropertyChanged;
    
        private RelayCommand<object> _addPersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _deletePersonCommand;
        
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
        public string SortingBy
        {
            get => _sorterName;
            set
            {
                _sorterName = value;
                PerformSorting();
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
        
        #region Sorting
        
        private void PerformSorting()
        {
            switch (_sorterName)
            {
                case "Name":
                    Persons = new ObservableCollection<Person>(_persons.OrderBy(i => i.Name));
                    break;
                case "Surname":
                    Persons = new ObservableCollection<Person>(_persons.OrderBy(i => i.Surname));
                    break;
                case "E-mail":
                    Persons = new ObservableCollection<Person>(_persons.OrderBy(i => i.Email));
                    break;
                case "Birthday":
                    Persons = new ObservableCollection<Person>(_persons.OrderBy(i => i.BirthDay));
                    break;
                case "IsBirthday":
                    Persons = new ObservableCollection<Person>(_persons.OrderBy(i => i.IsBirthday));
                    break;
                case "IsAdult":
                    Persons = new ObservableCollection<Person>(_persons.OrderBy(i => i.IsAdult));
                    break;
                case "SunSign":
                    Persons = new ObservableCollection<Person>(_persons.OrderBy(i => i.SunSign));
                    break;
                case "ChineseSign":
                    Persons = new ObservableCollection<Person>(_persons.OrderBy(i => i.ChineseSign));
                    break;
                default:
                    var personsList = StorageManager.Storage.PersonsList; 
                    Persons = new ObservableCollection<Person>(personsList);
                    break;
            }
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
