using Microsoft.Extensions.Logging;
using RabbitMq.Connector.Publisher;
using RabbitMQ.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities;

namespace UserService.Business.Services.RabbitMQ
{
    public class ProjectRabbitMqPublisher : RabbitMqPublisher<Project>
    {
        public ProjectRabbitMqPublisher(ILogger<RabbitMqPublisher<Project>> logger, IRabbitConnectionFactory connectionFactory,  string? exchangeType = null) 
            : base(logger, connectionFactory, "project-queue", "project-events", exchangeType)
        {
        }
    }
}
