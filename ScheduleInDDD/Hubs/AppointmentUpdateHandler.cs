﻿using System.Threading;
using System.Threading.Tasks;
using ScheduleInDDD.Core.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ScheduleInDDD.Api.Hubs
{
  public class AppointmentUpdateHandler : INotificationHandler<AppointmentUpdatedEvent>
  {
    private readonly IHubContext<ScheduleHub> _hubContext;

    public AppointmentUpdateHandler(IHubContext<ScheduleHub> hubContext)
    {
      _hubContext = hubContext;
    }

    public Task Handle(AppointmentUpdatedEvent notification, CancellationToken cancellationToken)
    {
      return _hubContext.Clients.All.SendAsync("ReceiveMessage", notification.AppointmentUpdated.Title + " was updated", cancellationToken);
    }
  }
}
