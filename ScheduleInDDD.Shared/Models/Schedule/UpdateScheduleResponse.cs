using System;

namespace ScheduleInDDD.Models.Schedule
{
  public class UpdateScheduleResponse : BaseResponse
  {
    public ScheduleDto Schedule { get; set; } = new ScheduleDto();

    public UpdateScheduleResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateScheduleResponse()
    {
    }
  }
}
