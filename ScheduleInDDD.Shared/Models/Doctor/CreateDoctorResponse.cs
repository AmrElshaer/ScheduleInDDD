using System;

namespace ScheduleInDDD.Models.Doctor
{
  public class CreateDoctorResponse : BaseResponse
  {
    public DoctorDto Doctor { get; set; } = new DoctorDto();

    public CreateDoctorResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateDoctorResponse()
    {
    }
  }
}