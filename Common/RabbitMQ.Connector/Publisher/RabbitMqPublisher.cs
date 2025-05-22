using System.Text;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using RabbitMQ.Client;
using RabbitMQ.Connector;

namespace RabbitMq.Connector.Publisher;

/// <summary>
/// Класс для записи сообщений в брокер RabbitMq, регистрировать как Singleton
/// </summary>
/// <typeparam name="T"></typeparam>
public class RabbitMqPublisher<T> : IDisposable, IBrokerPublisher<T> where T : class
{
    private bool _disposedValue;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queue;
    private readonly ILogger<RabbitMqPublisher<T>> _logger;

    public RabbitMqPublisher(ILogger<RabbitMqPublisher<T>> logger, IRabbitConnectionFactory connectionFactory,
        string queue, string? exchange = null, string? exchangeType = null)
    {
        _queue = queue ?? throw new ArgumentNullException(nameof(queue));
        _logger = logger;
        exchange ??= string.Empty;

        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();

        _logger.LogInformation("RabbitMq connection '{connectionUrl}' created", connectionFactory.ConnectionString);

        _channel.QueueDeclare(queue: _queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        if (!string.IsNullOrEmpty(exchange))
        {
            exchangeType ??= ExchangeType.Direct;
            _channel.ExchangeDeclare(exchange, exchangeType);
            _channel.QueueBind(_queue, exchange, _queue, null);
        }
    }

    public void Publish(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        string message = JsonConvert.SerializeObject(entity, JsonSerializerHelper.GetTypeNameHandlingNoneSettings());
        byte[] body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: string.Empty,
            routingKey: _queue,
            basicProperties: null,
            body: body);

        _logger.LogDebug("Message publish to RabbitMq queue '{_queue}'.Message: '{body}'", _queue, body);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _channel.Close();
                _connection.Close();
                _channel.Dispose();
                _connection.Dispose();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}