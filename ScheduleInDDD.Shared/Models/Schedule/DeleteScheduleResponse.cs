using System;

namespace ScheduleInDDD.Models.Schedule
{
  public class DeleteScheduleResponse : BaseResponse
  {

    public DeleteScheduleResponse(Guid correlationId) : base(correlationId)
    {
    }

    public DeleteScheduleResponse()
    {
    }
  }
}
