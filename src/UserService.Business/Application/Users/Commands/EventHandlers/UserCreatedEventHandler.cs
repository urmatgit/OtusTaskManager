using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities.Events;

namespace UserService.Business.Application.Projects.Commands.EventHandlers
{
    internal class UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger) : INotificationHandler<UserCreatedEvent>
    {
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {

            logger.LogInformation("handling user created domain event..");
            await Task.FromResult(notification);
            logger.LogInformation("finished handling user created domain event..");
        }
    }
}
