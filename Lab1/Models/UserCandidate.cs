namespace KMA.ProgrammingInCSharp2022.Practice3LoginControlMVVM.Models
{
    class UserCandidate
    {
        #region Fields
        private DateTime _birthday;
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
        #endregion
    }
}
