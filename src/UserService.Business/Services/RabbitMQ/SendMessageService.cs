using Microsoft.Extensions.Logging;
using RabbitMq.Connector.Publisher;
using RabbitMQ.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Business.Services.RabbitMQ
{
    public class SendMessageService<T> : RabbitMqPublisher<T> where T : class
    {
        public SendMessageService(ILogger<RabbitMqPublisher<T>> logger, IRabbitConnectionFactory connectionFactory) : base(logger, connectionFactory, "message-queue", "message-events", null)
        {
        }
    }
}
