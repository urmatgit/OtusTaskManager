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
    internal class UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger, IBrokerPublisher<User> brokerPublisher) : INotificationHandler<UserCreatedEvent>
    {
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {

            logger.LogInformation("handling user created domain event..");
            brokerPublisher.Publish(notification.user);
            await Task.FromResult(notification);
            logger.LogInformation("finished handling user created domain event..");
        }
    }
}
