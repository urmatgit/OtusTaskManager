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
    public class UserUpdatedEventHandler(ILogger<UserUpdatedEventHandler> logger, IBrokerPublisher<User> brokerPublisher) : INotificationHandler<UserUpdatedEvent>
    {
        public async Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("handling user updated domain event..");
            brokerPublisher.Publish(notification.User);
            await Task.FromResult(notification);
            logger.LogInformation("finished handling user updated domain event..");
        }
    }
}
