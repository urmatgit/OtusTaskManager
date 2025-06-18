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
    internal class UserLogoutEventHandler(ILogger<UserLogoutEventHandler> logger,IBrokerPublisher<PublishMassage<User>> brokerPublisher) : INotificationHandler<UserLogoutEvent>
    {
        public async Task Handle(UserLogoutEvent notification, CancellationToken cancellationToken)
        {

            logger.LogInformation("handling user logout domain event..");
            PublishMassage<User> publishMassage = new DataAccess.Entities.PublishMassage<User>(notification.user, notification.RaisedOn, MessageAction.Logout);
            brokerPublisher.Publish(publishMassage);
            logger.LogInformation("finished handling user logout domain event..");
        }
    }
}
