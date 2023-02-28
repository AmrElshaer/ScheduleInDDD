using System;

namespace ScheduleInDDD.Models.Room
{
  public class GetByIdRoomResponse : BaseResponse
  {
    public RoomDto Room { get; set; } = new RoomDto();

    public GetByIdRoomResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdRoomResponse()
    {
    }
  }
}
