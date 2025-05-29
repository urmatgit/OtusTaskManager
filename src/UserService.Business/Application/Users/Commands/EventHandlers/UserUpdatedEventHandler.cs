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
    public class UserUpdatedEventHandler(ILogger<UserUpdatedEventHandler> logger) : INotificationHandler<UserUpdatedEvent>
    {
        public async Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("handling user updated domain event..");
            await Task.FromResult(notification);
            logger.LogInformation("finished handling user updated domain event..");
        }
    }
}
