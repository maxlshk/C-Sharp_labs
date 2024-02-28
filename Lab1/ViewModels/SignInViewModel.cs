using System;
using System.Windows;
using KMA.ProgrammingInCSharp2022.Practice3LoginControlMVVM.Models;
using KMA.ProgrammingInCSharp2022.Practice3LoginControlMVVM.Tools;

namespace KMA.ProgrammingInCSharp2022.Practice3LoginControlMVVM.ViewModels
{
    class SignInViewModel
    {
        #region Fields
        private UserCandidate _user = new UserCandidate();
        private RelayCommand<object> _submitCommand;
        private RelayCommand<object> _cancelCommand;
        #endregion

        #region Properties
        public DateTime Birthday
        {
            get
            {
                return _user.Birthday;
            }
            set
            {
                _user.Birthday = value;
            }
        }


        public RelayCommand<object> SubmitCommand
        {
            get
            {
                return _submitCommand ??= new RelayCommand<object>(_ => Submit(), CanExecute);
            }
        }

        public RelayCommand<object> CancelCommand
        {
            get
            {
                return _cancelCommand ??= new RelayCommand<object>(_ => Environment.Exit(0));
            }
        }
        #endregion

        private void Submit()
        {
            MessageBox.Show($"Submit successful for date {_user.Birthday}");
        }

        //private void SignUp()
        //{
        //    MessageBox.Show($"User with name {_user.Login} was created");
        //}

        private bool CanExecute(object obj)
        {
            return _user.Birthday > DateTime.MinValue;
        }
    }
}
