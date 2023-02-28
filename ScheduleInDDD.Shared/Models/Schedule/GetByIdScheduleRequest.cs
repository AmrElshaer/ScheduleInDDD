using System;

namespace ScheduleInDDD.Models.Schedule
{
  public class GetByIdScheduleRequest : BaseRequest
  {
    public const string Route = "api/schedules/{scheduleId}";
    public Guid ScheduleId { get; set; }
  }
}
