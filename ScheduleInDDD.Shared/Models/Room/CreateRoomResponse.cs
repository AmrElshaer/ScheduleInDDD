using System;

namespace ScheduleInDDD.Models.Room
{
  public class CreateRoomResponse : BaseResponse
  {
    public RoomDto Room { get; set; } = new RoomDto();

    public CreateRoomResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateRoomResponse()
    {
    }
  }
}