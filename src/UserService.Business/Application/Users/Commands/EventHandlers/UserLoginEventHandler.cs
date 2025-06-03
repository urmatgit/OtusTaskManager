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
    internal class UserLoginEventHandler(ILogger<UserLoginEventHandler> logger) : INotificationHandler<UserLoginEvent>
    {
        public async Task Handle(UserLoginEvent notification, CancellationToken cancellationToken)
        {

            logger.LogInformation("handling user login domain event..");
            await Task.FromResult(notification);
            logger.LogInformation("finished handling user login domain event..");
        }
    }
}
