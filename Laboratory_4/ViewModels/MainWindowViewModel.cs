using System.ComponentModel;
using KMA.ProgrammingInCSharp.Lab4.Models;
using KMA.ProgrammingInCSharp.Lab4.Navigation;

namespace KMA.ProgrammingInCSharp.Lab4.ViewModels
{
    class MainWindowViewModel : BaseNavigatableViewModel<MainNavigationTypes>, INotifyPropertyChanged
    {
        #region Fields

        private Person? _currentPerson;
        public event EventHandler CurrentPersonChanged;

        #endregion

        #region Properties

        public Person? CurrentPerson
        {
            get { return _currentPerson; }
            set
            {
                _currentPerson = value;
                OnPropertyChanged("CurrentPerson");
                CurrentPersonChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion
        
        public MainWindowViewModel()
        {
            Navigate(MainNavigationTypes.UserCard);
        }
        
        protected override INavigatable<MainNavigationTypes> CreateViewModel(MainNavigationTypes type)
        {
            switch (type)
            {
                case MainNavigationTypes.DataInput:
                    return new DataInputViewModel(this, ()=>Navigate(MainNavigationTypes.UserCard));
                case MainNavigationTypes.UserCard:
                    return new UsersTableViewModel(this, ()=>Navigate(MainNavigationTypes.DataInput));
                default:
                    return null;
            }
        }
    }
}
