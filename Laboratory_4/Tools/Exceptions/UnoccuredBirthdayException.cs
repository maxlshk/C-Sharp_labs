namespace KMA.ProgrammingInCSharp.Lab4.Tools.Exceptions
{
    internal class UnoccuredBirthdayException : InvalidBirthdayException
    {
        public UnoccuredBirthdayException(string message) : base(message)
        {

        }
    }
}
