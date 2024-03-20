using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingInCSharp.Lab3.Tools.Exceptions
{
    class TooOldBirthdayException : InvalidBirthdayException
    {
        public TooOldBirthdayException(string message) : base(message)
        {

        }
    }
}
