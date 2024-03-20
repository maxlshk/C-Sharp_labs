using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingInCSharp.Lab3.Tools.Exceptions
{
    internal class UnoccuredBirthdayException : InvalidBirthdayException
    {
        public UnoccuredBirthdayException(string message) : base(message)
        {

        }
    }
}
