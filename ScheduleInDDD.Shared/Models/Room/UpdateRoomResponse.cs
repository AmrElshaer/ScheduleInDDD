using System;

namespace ScheduleInDDD.Models.Room
{
  public class UpdateRoomResponse : BaseResponse
  {
    public RoomDto Room { get; set; } = new RoomDto();

    public UpdateRoomResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateRoomResponse()
    {
    }
  }
}