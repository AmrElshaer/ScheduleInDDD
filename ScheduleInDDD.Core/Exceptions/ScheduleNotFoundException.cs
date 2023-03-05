using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleInDDD.Core.Exceptions
{
    public class ScheduleNotFoundException : Exception
    {
        public ScheduleNotFoundException(string message) : base(message)
        {
        }

        public ScheduleNotFoundException(Guid scheduleId) : base($"No schedule with id {scheduleId} found.")
        {
        }
    }
}
