using System;

namespace ScheduleInDDD.Models.Schedule
{
  public class DeleteScheduleRequest : BaseRequest
  {
    public const string Route = "api/schedules/{Id}";
    public Guid Id { get; set; }
  }
}
