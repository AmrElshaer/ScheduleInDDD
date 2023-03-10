using System.Threading;
using System.Threading.Tasks;
using ScheduleInDDD.Core.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ScheduleInDDD.Api.Hubs
{
  public class AppointmentConfirmedHandler : INotificationHandler<AppointmentConfirmedEvent>
  {
    private readonly IHubContext<ScheduleHub> _hubContext;

    public AppointmentConfirmedHandler(IHubContext<ScheduleHub> hubContext)
    {
      _hubContext = hubContext;
    }

    public Task Handle(AppointmentConfirmedEvent args, CancellationToken cancellationToken)
    {
      return _hubContext.Clients.All.SendAsync("ReceiveMessage", args.AppointmentUpdated.Title + " was confirmed", cancellationToken);
    }
  }
}
