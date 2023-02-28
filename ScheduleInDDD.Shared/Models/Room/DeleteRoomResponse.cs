using System;

namespace ScheduleInDDD.Models.Room
{
  public class DeleteRoomResponse : BaseResponse
  {
    public DeleteRoomResponse(Guid correlationId) : base(correlationId)
    {
    }

    public DeleteRoomResponse()
    {
    }
  }
}