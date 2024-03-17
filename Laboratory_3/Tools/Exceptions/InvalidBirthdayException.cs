using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingInCSharp.Lab3.Tools.Exceptions
{
    internal class InvalidBirthdayException : Exception
    {
        public InvalidBirthdayException(string message) : base(message)
        {

        }
    }
}
