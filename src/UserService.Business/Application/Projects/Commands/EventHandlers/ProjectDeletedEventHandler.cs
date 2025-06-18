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
    public class ProjectDeletedEventHandler(ILogger<ProjectDeletedEventHandler> _logger, IBrokerPublisher<PublishMassage<Project>> brokerPublisher) : INotificationHandler<ProjectDeletedEvent>
    {
        public async Task Handle(ProjectDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("handling project created domain event..");
            PublishMassage<Project> publishMassage = new DataAccess.Entities.PublishMassage<Project>(notification.project, notification.RaisedOn, MessageAction.Deleted);
            brokerPublisher.Publish(publishMassage);
            _logger.LogInformation("finished handling project created domain event..");

        }
    }
}
