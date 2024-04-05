using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using KMA.ProgrammingInCSharp.Lab4.Tools.Exceptions;

namespace KMA.ProgrammingInCSharp.Lab4.Models
{
    class Person
    {
        #region Constructors
        [JsonConstructor]
        public Person(string name, string surname, string email, DateTime birthDay)
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

        private string _email;
        private int _age;


        private readonly string[] _chineseZodiacs =
        { "Monkey", "Rooster", "Dog",
          "Pig", "Rat", "Ox",
          "Tiger", "Rabbit", "Dragon",
          "Snake", "Horse", "Sheep"
        };
        #endregion

        #region Properties

        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDay { get; set; }

        public string Email
        {
            get => _email;
            set
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
            get => _age;
            set
            {
                _age = value switch
                {
                    > 130 => throw new TooOldBirthdayException("Your birth date has to be less than 130 years ago."),
                    < 0 => throw new UnoccuredBirthdayException("Your birth date has to be in the past."),
                    _ => value
                };
            }
        }

        public bool IsAdult { get; set; }
        public bool IsBirthday { get; set; }
        public string SunSign { get; set; }
        public string ChineseSign { get; set; }
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
            return _chineseZodiacs[year % 12];
        }
        private string SunZodiac()
        {
            var day = BirthDay.Day;
            var month = BirthDay.Month;
            switch (month)
            {
                case 1:
                    return day < 20 ? "Capricorn" : "Aquarius";
                case 2:
                    return day < 19 ? "Aquarius" : "Pisces";
                case 3:
                    return day < 21 ? "Pisces" : "Aries";
                case 4:
                    return day < 20 ? "Aries" : "Taurus";
                case 5:
                    return day < 21 ? "Taurus" : "Gemini";
                case 6:
                    return day < 21 ? "Gemini" : "Cancer";
                case 7:
                    return day < 23 ? "Cancer" : "Leo";
                case 8:
                    return day < 23 ? "Leo" : "Virgo";
                case 9:
                    return day < 23 ? "Virgo" : "Libra";
                case 10:
                    return day < 23 ? "Libra" : "Scorpio";
                case 11:
                    return day < 22 ? "Scorpio" : "Sagittarius";
                case 12:
                    return day < 22 ? "Sagittarius" : "Capricorn";
                default: return "Error";
            }
        }
        private bool ValidEmail(string email)
        {
            const string emailPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
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
