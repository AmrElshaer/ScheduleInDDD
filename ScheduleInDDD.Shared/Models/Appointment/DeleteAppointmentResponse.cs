using System;
using ScheduleInDDD.Models.Schedule;

namespace ScheduleInDDD.Models.Appointment
{
  public class DeleteAppointmentResponse : BaseResponse
  {

    public ScheduleDto Schedule { get; set; }

    public DeleteAppointmentResponse(Guid correlationId) : base(correlationId)
    {
    }

    public DeleteAppointmentResponse()
    {
    }

  }
}
