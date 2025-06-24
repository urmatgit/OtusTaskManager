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
    public class UserRabbitMqPublisher : RabbitMqPublisher<PublishMassage<User>>
    {
        public UserRabbitMqPublisher( IRabbitConnectionFactory connectionFactory, ILogger<RabbitMqPublisher<PublishMassage<User>>> logger) 
            : base(logger, connectionFactory,  "user-queue", "user-events")
        {
        }
    }
}
