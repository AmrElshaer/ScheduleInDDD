
using ScheduleInDDD.Core.ScheduleAggregate;
using ScheduleInDDD.Models.Appointment;

namespace ScheduleInDDD.Core.Interfaces
{
  public interface IAppointmentDTORepository
  {
    AppointmentDto GetFromAppointment(Appointment appointment);
  }
}
