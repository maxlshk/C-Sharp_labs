using System;
using System.Windows;
using System.ComponentModel;
using KMA.ProgrammingInCSharp2022.Practice3LoginControlMVVM.Models;
using KMA.ProgrammingInCSharp2022.Practice3LoginControlMVVM.Tools;

namespace KMA.ProgrammingInCSharp2022.Practice3LoginControlMVVM.ViewModels
{
    class SignInViewModel : INotifyPropertyChanged
    {
        #region Fields
        private UserCandidate _user = new UserCandidate();
        private RelayCommand<object> _submitCommand;
        private RelayCommand<object> _cancelCommand;

        public int Age => _user.Age;

        public string ZodiacSign => _user.ZodiacSign.ToString();

        public string ChineseZodiacSign => _user.ChineseZodiacSign.ToString();

        #endregion

        #region Properties
        public DateTime Birthday
        {
            get => _user.Birthday;
            set
            {
                if (_user.Birthday != value && value != DateTime.MinValue)
                {
                    _user.Birthday = value;
                }
            }
        }

        //private Visibility _textBlocksVisibility = Visibility.Hidden;

        //public Visibility TextBlocksVisibility
        //{
        //    get => _textBlocksVisibility;
        //    set
        //    {
        //        if (_textBlocksVisibility != value)
        //        {
        //            _textBlocksVisibility = value;
        //            OnPropertyChanged(nameof(TextBlocksVisibility));
        //        }
        //    }
        //}


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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void Submit()
        {
            if (IsLegal())
            {
                //TextBlocksVisibility = Visibility.Visible;
                OnPropertyChanged(nameof(Age));
                OnPropertyChanged(nameof(ZodiacSign));
                OnPropertyChanged(nameof(ChineseZodiacSign));
                if (IsBirthday())
                {
                    MessageBox.Show($"Happy birthday, one year closer to retirement 🥳");
                }
                else
                {
                    MessageBox.Show($"Submit successful for date {_user.Birthday}");
                }
            }
            else
            {
                MessageBox.Show("Invalid age");
            }
        }

        private bool CanExecute(object obj)
        {
            return _user.Birthday > DateTime.MinValue;
        }

        private bool IsBirthday()
        {
            return _user.Birthday.Day == DateTime.Now.Day && _user.Birthday.Month == DateTime.Now.Month;
        }

        private bool IsLegal()
        {
            return _user.Age > 0 && _user.Age < 135;
        }
    }
}
