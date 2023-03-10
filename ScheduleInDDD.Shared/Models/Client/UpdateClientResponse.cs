using System;

namespace ScheduleInDDD.Models.Client
{
  public class UpdateClientResponse : BaseResponse
  {
    public ClientDto Client { get; set; } = new ClientDto();

    public UpdateClientResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateClientResponse()
    {
    }
  }
}
