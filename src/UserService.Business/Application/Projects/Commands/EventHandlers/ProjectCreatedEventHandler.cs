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
    public class ProjectCreatedEventHandler(ILogger<ProjectCreatedEventHandler> logger) : INotificationHandler<ProjectCreatedEvent>
    {
        public async Task Handle(ProjectCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("handling project created domain event..");
            await Task.FromResult(notification);
            logger.LogInformation("finished handling project created domain event..");
        }
    }
}
