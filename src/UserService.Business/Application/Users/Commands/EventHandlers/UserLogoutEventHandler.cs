using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMq.Connector.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Entities.Events;

namespace UserService.Business.Application.Projects.Commands.EventHandlers
{
    internal class UserLogoutEventHandler(ILogger<UserLogoutEventHandler> logger,IBrokerPublisher<User> brokerPublisher) : INotificationHandler<UserLogoutEvent>
    {
        public async Task Handle(UserLogoutEvent notification, CancellationToken cancellationToken)
        {

            logger.LogInformation("handling user logout domain event..");
             brokerPublisher.Publish(notification.user);
            await Task.FromResult(notification);
            logger.LogInformation("finished handling user logout domain event..");
        }
    }
}
