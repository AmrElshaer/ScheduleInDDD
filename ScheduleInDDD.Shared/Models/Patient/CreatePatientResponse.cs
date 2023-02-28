using System;

namespace ScheduleInDDD.Models.Patient
{
  public class CreatePatientResponse : BaseResponse
  {
    public PatientDto Patient { get; set; } = new PatientDto();

    public CreatePatientResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreatePatientResponse()
    {
    }
  }
}