﻿namespace ScheduleInDDD.Models.Doctor
{
  public class UpdateDoctorRequest : BaseRequest
  {
    public int DoctorId { get; set; }
    public string Name { get; set; }
  }
}
