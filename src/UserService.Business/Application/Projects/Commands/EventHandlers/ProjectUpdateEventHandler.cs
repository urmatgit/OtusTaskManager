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
    public class ProjectUpdatedEventHandler(ILogger<ProjectUpdatedEventHandler> logger) : INotificationHandler<ProjectUpdatedEvent>
    {
        public async Task Handle(ProjectUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("handling project updated domain event..");
            await Task.FromResult(notification);
            logger.LogInformation("finished handling project updated domain event..");
        }
    }
}
