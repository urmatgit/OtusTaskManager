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
    public class ProjectDeletedEventHandler(ILogger<ProjectDeletedEventHandler> _logger) : INotificationHandler<ProjectDeletedEvent>
    {
        public async Task Handle(ProjectDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("handling project created domain event..");
            await Task.FromResult(notification);
            _logger.LogInformation("finished handling project created domain event..");

        }
    }
}
