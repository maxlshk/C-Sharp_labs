using KMA.ProgrammingInCSharp.Lab1.Navigation;

namespace KMA.ProgrammingInCSharp.Lab1.ViewModels
{
    class MainViewModel : INavigatable<MainNavigationTypes>
    {
        public MainViewModel(Action exitEvent)
        {
        }

        public MainNavigationTypes ViewType
        {
            get
            {
                return MainNavigationTypes.Main;
            }
        }
    }
}
