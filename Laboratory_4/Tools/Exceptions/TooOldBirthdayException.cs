namespace KMA.ProgrammingInCSharp.Lab4.Tools.Exceptions
{
    class TooOldBirthdayException : InvalidBirthdayException
    {
        public TooOldBirthdayException(string message) : base(message)
        {

        }
    }
}
