using System;

namespace ScheduleInDDD.Models.Client
{
  public class CreateClientResponse : BaseResponse
  {
    public ClientDto Client { get; set; } = new ClientDto();

    public CreateClientResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateClientResponse()
    {
    }
  }
}
