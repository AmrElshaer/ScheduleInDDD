using System;

namespace ScheduleInDDD.Models.Appointment
{
  public class UpdateAppointmentResponse : BaseResponse
  {
    public AppointmentDto Appointment { get; set; } = new AppointmentDto();

    public UpdateAppointmentResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateAppointmentResponse()
    {
    }
  }
}
