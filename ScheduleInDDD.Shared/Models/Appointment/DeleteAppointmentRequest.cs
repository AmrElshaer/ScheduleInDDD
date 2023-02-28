﻿using System;

namespace ScheduleInDDD.Models.Appointment
{
  public class DeleteAppointmentRequest : BaseRequest
  {
    public const string Route = "api/schedule/{ScheduleId}/appointments/{AppointmentId}";

    public Guid ScheduleId { get; set; }
    public Guid AppointmentId { get; set; }
  }
}
