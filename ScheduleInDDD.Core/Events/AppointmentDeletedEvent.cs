using PluralsightDdd.SharedKernel;
using ScheduleInDDD.Core.ScheduleAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleInDDD.Core.Events
{
    internal class AppointmentDeletedEvent : BaseDomainEvent
    {
        public AppointmentDeletedEvent(Appointment appointment)
        {
            AppointmentDeleted = appointment;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public Appointment AppointmentDeleted { get; private set; }
    }
}
