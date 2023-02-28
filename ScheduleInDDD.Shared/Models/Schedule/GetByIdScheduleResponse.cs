using System;

namespace ScheduleInDDD.Models.Schedule
{
  public class GetByIdScheduleResponse : BaseResponse
  {
    public ScheduleDto Schedule { get; set; } = new ScheduleDto();

    public GetByIdScheduleResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdScheduleResponse()
    {
    }
  }
}
