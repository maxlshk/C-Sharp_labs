namespace KMA.ProgrammingInCSharp.Lab1.Models
{
    class User
    {
        public User(UserCandidate _user)
        {
            BirthDay = _user.Birthday;
            Age = _user.Age;    
        }

        #region StaticFields
        private static User? _currentUser;
        public static event EventHandler? CurrentUserChanged;
        #endregion

        #region Properties
        public static User? CurrentUser
        {
            get => _currentUser;
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                    OnCurrentUserChanged();
                }
            }
        }

        public DateTime BirthDay { get; }

        public int Age { get; }
        #endregion

        #region Methods
        public string WesternZodiacSign
        {
            get
            {
                switch (BirthDay.Month)
                {
                    case 1 when BirthDay.Day <= 19:
                    case 12 when BirthDay.Day >= 22:
                        return ZodiacSigns.Capricorn.ToString();
                    case 1:
                    case 2 when BirthDay.Day <= 18:
                        return ZodiacSigns.Aquarius.ToString();
                    case 2:
                    case 3 when BirthDay.Day <= 20:
                        return ZodiacSigns.Pisces.ToString();
                    case 3:
                    case 4 when BirthDay.Day <= 19:
                        return ZodiacSigns.Aries.ToString();
                    case 4:
                    case 5 when BirthDay.Day <= 20:
                        return ZodiacSigns.Taurus.ToString();
                    case 5:
                    case 6 when BirthDay.Day <= 20:
                        return ZodiacSigns.Gemini.ToString();
                    case 6:
                    case 7 when BirthDay.Day <= 22:
                        return ZodiacSigns.Cancer.ToString();
                    case 7:
                    case 8 when BirthDay.Day <= 22:
                        return ZodiacSigns.Leo.ToString();
                    case 8:
                    case 9 when BirthDay.Day <= 22:
                        return ZodiacSigns.Virgo.ToString();
                    case 9:
                    case 10 when BirthDay.Day <= 22:
                        return ZodiacSigns.Libra.ToString();
                    case 10:
                    case 11 when BirthDay.Day <= 21:
                        return ZodiacSigns.Scorpio.ToString();
                    case 11:
                    case 12 when BirthDay.Day <= 21:
                        return ZodiacSigns.Sagittarius.ToString();
                    default:
                        return "Unknown";
                }
            }
        }

        public string ChineseZodiacSign => ((ChineseZodiacSigns)((BirthDay.Year - 4) % 12)).ToString();

        public bool IsBirthday => BirthDay.Day == DateTime.Today.Day && BirthDay.Month == DateTime.Today.Month;

        private static void OnCurrentUserChanged()
        {
            CurrentUserChanged?.Invoke(null, EventArgs.Empty);
        }
        #endregion
    }

    public enum ZodiacSigns
    {
        Aries, Taurus, Gemini, Cancer,
        Leo, Virgo, Libra, Scorpio,
        Sagittarius, Capricorn, Aquarius, Pisces
    }

    public enum ChineseZodiacSigns
    {
        Rat, Ox, Tiger, Rabbit,
        Dragon, Snake, Horse, Goat,
        Monkey, Rooster, Dog, Pig
    }
}
