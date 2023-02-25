using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleInDDD.Core.Exceptions
{
    public class DuplicateAppointmentException : ArgumentException
    {
        public DuplicateAppointmentException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
