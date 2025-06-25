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
using UserService.DataAccess.Enums;

namespace UserService.Business.Application.Projects.Commands.EventHandlers
{
    internal class UserCreatedEventHandler( IBrokerPublisher<PublishMassage<User>> brokerPublisher, ILogger<UserCreatedEventHandler> logger) : INotificationHandler<UserCreatedEvent>
    {
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {

            logger.LogInformation("handling user created domain event..");
            PublishMassage<User> publishMassage = new DataAccess.Entities.PublishMassage<User>(notification.user, notification.RaisedOn, MessageAction.Created);
            await Task.Run(() => {
                brokerPublisher?.Publish(publishMassage);
            });
            logger.LogInformation("finished handling user created domain event..");
        }
    }
}
