using System;

namespace ScheduleInDDD.Models.Patient
{
  public class GetByIdPatientResponse : BaseResponse
  {
    public PatientDto Patient { get; set; } = new PatientDto();

    public GetByIdPatientResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdPatientResponse()
    {
    }
  }
}