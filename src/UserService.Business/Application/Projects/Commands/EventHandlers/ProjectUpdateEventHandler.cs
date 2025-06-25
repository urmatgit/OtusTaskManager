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
    public class ProjectUpdatedEventHandler(ILogger<ProjectUpdatedEventHandler> logger, IBrokerPublisher<PublishMassage<Project>> brokerPublisher) : INotificationHandler<ProjectUpdatedEvent>
    {
        public async Task Handle(ProjectUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("handling project updated domain event..");
            PublishMassage<Project> publishMassage = new DataAccess.Entities.PublishMassage<Project>(notification.Project, notification.RaisedOn, MessageAction.Updated);
            await Task.Run(() => {
                brokerPublisher?.Publish(publishMassage);
            });
            logger.LogInformation("finished handling project updated domain event..");
        }
    }
}
