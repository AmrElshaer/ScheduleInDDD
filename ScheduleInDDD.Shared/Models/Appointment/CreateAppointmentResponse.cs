using System;

namespace ScheduleInDDD.Models.Appointment
{
  public class CreateAppointmentResponse : BaseResponse
  {
    public AppointmentDto Appointment { get; set; } = new AppointmentDto();

    public CreateAppointmentResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateAppointmentResponse()
    {
    }
  }
}