using KMA.ProgrammingInCSharp.Lab3.Navigation;

namespace KMA.ProgrammingInCSharp.Lab3.ViewModels
{
    class MainWindowViewModel : BaseNavigatableViewModel<MainNavigationTypes>
    {
        public MainWindowViewModel()
        {
            Navigate(MainNavigationTypes.DataInput);
        }
        
        protected override INavigatable<MainNavigationTypes> CreateViewModel(MainNavigationTypes type)
        {
            switch (type)
            {
                case MainNavigationTypes.DataInput:
                    return new DataInputViewModel(()=>Navigate(MainNavigationTypes.UserCard));
                case MainNavigationTypes.UserCard:
                    return new UserCardViewModel(()=>Navigate(MainNavigationTypes.DataInput));
                default:
                    return null;
            }
        }
    }
}
