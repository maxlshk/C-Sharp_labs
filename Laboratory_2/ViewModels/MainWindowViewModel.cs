﻿using KMA.ProgrammingInCSharp.Lab2.Navigation;

namespace KMA.ProgrammingInCSharp.Lab2.ViewModels
{
    class MainWindowViewModel : BaseNavigatableViewModel<MainNavigationTypes>
    {
        public MainWindowViewModel()
        {
            Navigate(MainNavigationTypes.SignIn);
        }
        
        protected override INavigatable<MainNavigationTypes> CreateViewModel(MainNavigationTypes type)
        {
            switch (type)
            {
                case MainNavigationTypes.SignIn:
                    return new SignInViewModel(()=>Navigate(MainNavigationTypes.Main));
                case MainNavigationTypes.Main:
                    return new MainViewModel(()=>Navigate(MainNavigationTypes.SignIn));
                default:
                    return null;
            }
        }
    }
}
