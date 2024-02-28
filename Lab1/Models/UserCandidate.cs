namespace KMA.ProgrammingInCSharp2022.Practice3LoginControlMVVM.Models
{
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

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - _birthday.Year;
                if (_birthday > today.AddYears(-age)) age--;
                return age;
            }
        }

        public ZodiacSigns ZodiacSign => GetWesternZodiacSign(_birthday);

        private ZodiacSigns GetWesternZodiacSign(DateTime birthday)
        {
            var zodiacSignRanges = new[]
            {
                (Start: new DateTime(1, 3, 21), End: new DateTime(1, 4, 19), Sign: ZodiacSigns.Aries),
                (Start: new DateTime(1, 4, 20), End: new DateTime(1, 5, 20), Sign: ZodiacSigns.Taurus),
                (Start: new DateTime(1, 5, 21), End: new DateTime(1, 6, 20), Sign: ZodiacSigns.Gemini),
                (Start: new DateTime(1, 6, 21), End: new DateTime(1, 7, 22), Sign: ZodiacSigns.Cancer),
                (Start: new DateTime(1, 7, 23), End: new DateTime(1, 8, 22), Sign: ZodiacSigns.Leo),
                (Start: new DateTime(1, 8, 23), End: new DateTime(1, 9, 22), Sign: ZodiacSigns.Virgo),
                (Start: new DateTime(1, 9, 23), End: new DateTime(1, 10, 22), Sign: ZodiacSigns.Libra),
                (Start: new DateTime(1, 10, 23), End: new DateTime(1, 11, 21), Sign: ZodiacSigns.Scorpio),
                (Start: new DateTime(1, 11, 22), End: new DateTime(1, 12, 21), Sign: ZodiacSigns.Sagittarius),
                (Start: new DateTime(1, 12, 22), End: new DateTime(1, 1, 19), Sign: ZodiacSigns.Capricorn),
                (Start: new DateTime(1, 1, 20), End: new DateTime(1, 2, 18), Sign: ZodiacSigns.Aquarius),
                (Start: new DateTime(1, 2, 19), End: new DateTime(1, 3, 20), Sign: ZodiacSigns.Pisces),
            };

            foreach (var range in zodiacSignRanges)
            {
                var start = new DateTime(birthday.Year, range.Start.Month, range.Start.Day);
                var end = range.End.Month < range.Start.Month ? new DateTime(birthday.Year + 1, range.End.Month, range.End.Day) : new DateTime(birthday.Year, range.End.Month, range.End.Day);

                if (birthday >= start && birthday <= end)
                {
                    return range.Sign;
                }
            }

            throw new InvalidOperationException("Failed to determine the Zodiac sign.");
        }

        public ChineseZodiacSigns ChineseZodiacSign
        {
            get
            {
                return (ChineseZodiacSigns)(_birthday.Year - 4) % 12;
            }
        }
        
        #endregion
    }
}
