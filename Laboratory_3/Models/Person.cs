using System.Text;
using System.Text.RegularExpressions;
using KMA.ProgrammingInCSharp.Lab3.Tools.Exceptions;

namespace KMA.ProgrammingInCSharp.Lab3.Models
{
    class Person
    {
        #region Constructors
        internal Person(string name, string surname, string email, DateTime birthDay)
        {
            Name = name;
            Surname = surname;
            Email = email;
            BirthDay = birthDay;
            Age = CalculateAge();

            ChineseSign = ChineseZodiac();
            SunSign = SunZodiac();
            IsBirthday = IsTodayBirthDay();
            IsAdult = Age >= 18;
        }
        Person(string firstName, string surName, string email)
            : this(firstName, surName, email, DateTime.Today) { }
        Person(string firstName, string surName, DateTime birthDay)
            : this(firstName, surName, "test@gmail.com", birthDay) { }
        #endregion

        #region Fields

        private static Person _CurrentPerson;
        private string _name;
        private string _surname;
        private DateTime _birthDay;
        private string _email;
        private int _age;

        private readonly bool _isAdult;
        private readonly bool _isBirthday;
        private readonly string _sunSign;
        private readonly string _chineseSign;

        private readonly string[] chineseZodiacs =
        { "Monkey", "Rooster", "Dog",
          "Pig", "Rat", "Ox",
          "Tiger", "Rabbit", "Dragon",
          "Snake", "Horse", "Sheep"
        };
        #endregion

        #region Properties

        public static Person CurrentPerson
        {
            get
            {
                if (_CurrentPerson == null)
                {
                    throw new Exception("Person is not initialized");
                }
                return _CurrentPerson;
            }
            set
            {
                _CurrentPerson = value;
            }
        }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime BirthDay { get; private set; }

        public string Email
        {
            get
            {
                return _email;
            }
            private set
            {
                if (ValidEmail(value))
                {
                    _email = value;
                }
                else
                {
                    throw new InvalidEmailException("Not Valid Email");
                }
            }
        }

        public int Age
        {
            get
            {
                return _age;
            }
            private set
            {
                if (value > 130)
                {
                    throw new InvalidBirthdayException("Your birth date has to be less than 130 years ago.");
                }

                if (value < 0)
                {
                    throw new InvalidBirthdayException("Your birth date has to be in the past.");
                }

                _age = value;
            }
        }
        public bool IsAdult { get; private set; }
        public bool IsBirthday { get; private set; }
        public string SunSign { get; private set; }
        public string ChineseSign { get; private set; }
        #endregion

        #region Methods
        private int CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDay.Year;
            if (BirthDay.Date > today.AddYears(-age)) age--;
            return age;
        }
        private bool IsTodayBirthDay()
        {
            var today = DateTime.Today;
            return BirthDay.Month == today.Month && BirthDay.Day == today.Day;
        }
        private string ChineseZodiac()
        {
            var year = BirthDay.Year;
            return chineseZodiacs[year % 12];
        }
        private string SunZodiac()
        {
            var day = BirthDay.Day;
            var month = BirthDay.Month;
            switch (month)
            {
                case 1:
                    if (day < 20) return "Capricorn";
                    return "Aquarius";
                case 2:
                    if (day < 19) return "Aquarius";
                    return "Pisces";
                case 3:
                    if (day < 21) return "Pisces";
                    return "Aries";
                case 4:
                    if (day < 20) return "Aries";
                    return "Taurus";
                case 5:
                    if (day < 21) return "Taurus";
                    return "Gemini";
                case 6:
                    if (day < 21) return "Gemini";
                    return "Cancer";
                case 7:
                    if (day < 23) return "Cancer";
                    return "Leo";
                case 8:
                    if (day < 23) return "Leo";
                    return "Virgo";
                case 9:
                    if (day < 23) return "Virgo";
                    return "Libra";
                case 10:
                    if (day < 23) return "Libra";
                    return "Scorpio";
                case 11:
                    if (day < 22) return "Scorpio";
                    return "Sagittarius";
                case 12:
                    if (day < 22) return "Sagittarius";
                    return "Capricorn";
                default: return "Error";
            }
        }
        private bool ValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
            return Regex.IsMatch(email, emailPattern);
        }

        public override string ToString()
        {
            StringBuilder builder = new();
            builder.AppendLine($"Name: {Name}");
            builder.AppendLine($"Surname: {Surname}");
            builder.AppendLine($"E-mail: {Email}");
            builder.AppendLine($"Birthday date: {BirthDay.Day}.{BirthDay.Month}.{BirthDay.Year}");
            builder.AppendLine($"Sun sign: {SunSign}");
            builder.AppendLine($"Chinese sign: {ChineseSign}");
            builder.AppendLine($"Is adult: {IsAdult}");
            builder.AppendLine($"Age: {Age}");
            if (IsBirthday)
            {
                builder.AppendLine("HAPPY BIRTHDAY!🔥");
            }
            
            return builder.ToString();
        }
        #endregion
    }
}
