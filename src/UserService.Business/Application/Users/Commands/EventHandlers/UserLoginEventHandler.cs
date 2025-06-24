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
    internal class UserLoginEventHandler(ILogger<UserLoginEventHandler> logger, IBrokerPublisher<PublishMassage<User>> brokerPublisher) : INotificationHandler<UserLoginEvent>
    {
        public async Task Handle(UserLoginEvent notification, CancellationToken cancellationToken)
        {

            logger.LogInformation("handling user login domain event..");
            PublishMassage<User> publishMassage = new DataAccess.Entities.PublishMassage<User>(notification.user, notification.RaisedOn, MessageAction.Login);
            await Task.Run(() => {
                brokerPublisher.Publish(publishMassage);
            });
            await Task.FromResult(notification);
            logger.LogInformation("finished handling user login domain event..");
        }
    }
}
