﻿using System.ComponentModel;
using KMA.ProgrammingInCSharp.Lab4.Models;
using KMA.ProgrammingInCSharp.Lab4.Navigation;

namespace KMA.ProgrammingInCSharp.Lab4.ViewModels
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