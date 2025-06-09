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
    public class ProjectCreatedEventHandler(ILogger<ProjectCreatedEventHandler> logger, IBrokerPublisher<Project> brokerPublisher) : INotificationHandler<ProjectCreatedEvent>
    {
        
        public async Task Handle(ProjectCreatedEvent notification,  CancellationToken cancellationToken)
        {
            logger.LogInformation("handling project created domain event..");
             brokerPublisher.Publish(notification.Project);
            //await Task.FromResult(notification);
            logger.LogInformation("finished handling project created domain event..");
        }
    }
}
