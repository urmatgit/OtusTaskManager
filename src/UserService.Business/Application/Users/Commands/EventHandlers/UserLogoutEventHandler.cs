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
    internal class UserLogoutEventHandler(ILogger<UserLogoutEventHandler> logger) : INotificationHandler<UserLogoutEvent>
    {
        public async Task Handle(UserLogoutEvent notification, CancellationToken cancellationToken)
        {

            logger.LogInformation("handling user logout domain event..");
            await Task.FromResult(notification);
            logger.LogInformation("finished handling user logout domain event..");
        }
    }
}
