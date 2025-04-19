using System.Text;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using RabbitMQ.Client;
using RabbitMQ.Connector;

namespace RabbitMq.Connector.Publisher.EventPublisher;

public class RabbitMqEventPublisher : IEventBrokerPublisher
{
    private readonly IConnection _connection;

    private readonly ILogger<RabbitMqEventPublisher> _logger;

    public RabbitMqEventPublisher(IRabbitConnectionFactory connectionFactory, ILogger<RabbitMqEventPublisher> logger)
    {
        _logger = logger;
        _connection = connectionFactory.CreateConnection();
        _logger.LogInformation("RabbitMq connection '{connectionUrl}' created", connectionFactory.ConnectionString);
    }

    public void SendMqPersistentModel(MqPersistentModel model, string exchangeName, string queueName,
        string exchangeType = "direct")
    {
        var message =
            JsonConvert.SerializeObject(model, JsonSerializerHelper.GetTypeNameHandlingNoneSettings());
        byte[] body = Encoding.UTF8.GetBytes(message);

        using var channel = _connection.CreateModel();

        channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType);
        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);
        channel.QueueBind(queueName, exchangeName, queueName, null);

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;

        channel.BasicPublish(exchangeName, queueName, properties, body);

        _logger.LogDebug(
            "Message publish to RabbitMq queue '{_queue}'.Exchange: '{exchange}'. ExchangeType: {exchangeType}. Message: '{message}'",
            queueName, exchangeName, exchangeType, message);
    }

    public void Send<TObject>(TObject obj, string exchangeName, string queueName, string exchangeType = "direct")
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentNullException.ThrowIfNull(exchangeName);
        ArgumentNullException.ThrowIfNull(queueName);

        var persistentModel = new MqPersistentModel(obj);
        SendMqPersistentModel(persistentModel, exchangeName, queueName, exchangeType);
    }
}