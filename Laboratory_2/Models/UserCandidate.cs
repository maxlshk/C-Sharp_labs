namespace KMA.ProgrammingInCSharp.Lab1.Models
{
    class UserCandidate
    {
        #region Fields
        private DateTime _birthday = DateTime.Today;
        #endregion

        #region Properties
        public DateTime Birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                _birthday = value;
            }
        }

        public int Age
        {
            get
            {
                var age = DateTime.Now.Year - _birthday.Year;
                if (DateTime.Now.DayOfYear < _birthday.DayOfYear)
                    age--;
                return age;
            }
        }
        #endregion
    }
}
