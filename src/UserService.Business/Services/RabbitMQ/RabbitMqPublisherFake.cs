using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities;
using RabbitMq.Connector.Publisher;
using RabbitMQ.Connector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Business.Services.RabbitMQ
{
    public class RabbitMqPublisherFake<T> : IDisposable, IBrokerPublisher<T> where T : class
    {
        private readonly string _queue;
        private readonly ILogger<RabbitMqPublisher<T>> _logger;
        public RabbitMqPublisherFake(ILogger<RabbitMqPublisher<T>> logger, IRabbitConnectionFactory connectionFactory, string queue, string? exchange = null, string? exchangeType = null)
        {
            _logger = logger;
            _queue = queue;
        }
        public void Dispose()
        {
        }

        public void Publish(T entity)

        {
            ArgumentNullException.ThrowIfNull(entity, "entity");
            string s = JsonConvert.SerializeObject((object)entity, JsonSerializerHelper.GetTypeNameHandlingNoneSettings());
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            _logger.LogDebug("Message publish to RabbitMq queue '{_queue}'.Message: '{body}'", _queue, bytes);
        }
    }
}
