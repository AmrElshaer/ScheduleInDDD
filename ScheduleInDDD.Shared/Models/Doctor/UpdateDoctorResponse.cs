using System;

namespace ScheduleInDDD.Models.Doctor
{
  public class UpdateDoctorResponse : BaseResponse
  {
    public DoctorDto Doctor { get; set; } = new DoctorDto();

    public UpdateDoctorResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateDoctorResponse()
    {
    }
  }
}