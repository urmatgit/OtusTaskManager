using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Business.Application.Events
{
    //пишем лог через INotificationHandler
    public class LogEventHandler(ILogger<LogEventHandler> _logger) : INotificationHandler<EntityEvent<Guid>>
    {
        public Task Handle(EntityEvent<Guid> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{notification.message}. ({notification.baseEntity.Id})");
            return Task.CompletedTask;
        }
    }

}
