using System;

namespace ScheduleInDDD.Models.Client
{
  public class DeleteClientResponse : BaseResponse
  {

    public DeleteClientResponse(Guid correlationId) : base(correlationId)
    {
    }

    public DeleteClientResponse()
    {
    }
  }
}
