using KMA.ProgrammingInCSharp.Lab3.Models;
using KMA.ProgrammingInCSharp.Lab3.Navigation;
using System.ComponentModel;

namespace KMA.ProgrammingInCSharp.Lab3.ViewModels
{
    class MainWindowViewModel : BaseNavigatableViewModel<MainNavigationTypes>, INotifyPropertyChanged
    {
        private Person _currentPerson;
        public Person CurrentPerson
        {
            get { return _currentPerson; }
            set
            {
                _currentPerson = value;
                OnPropertyChanged("CurrentPerson");
            }
        }
        public MainWindowViewModel()
        {
            Navigate(MainNavigationTypes.DataInput);
        }
        
        protected override INavigatable<MainNavigationTypes> CreateViewModel(MainNavigationTypes type)
        {
            switch (type)
            {
                case MainNavigationTypes.DataInput:
                    return new DataInputViewModel(this, ()=>Navigate(MainNavigationTypes.UserCard));
                case MainNavigationTypes.UserCard:
                    return new UserCardViewModel(this, ()=>Navigate(MainNavigationTypes.DataInput));
                default:
                    return null;
            }
        }
    }
}
