using System.Text;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Connector;

namespace RabbitMq.Connector.Consumer;

/// <summary>
/// Класс для чтение сообщенией из очереди RabbitMq, использовать в HostedService
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseRabbitMqConsumer<T> : IDisposable, IBrokerConsumer<T> where T : class
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queue;
    private readonly ILogger<BaseRabbitMqConsumer<T>> _logger;
    private bool _disposedValue;
    private readonly SemaphoreSlim _semaphore;

    protected BaseRabbitMqConsumer(IRabbitConnectionFactory rabbitConnectionFactory, string queue, int maxParallelsHandler,
        ILogger<BaseRabbitMqConsumer<T>> logger)
    {
        ArgumentNullException.ThrowIfNull(queue);

        _queue = queue;
        _logger = logger;

        _connection = rabbitConnectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _semaphore = new SemaphoreSlim(maxParallelsHandler, maxParallelsHandler);
        _logger.LogInformation(
            "RabbitMq connection '{connectionString}' created. MaxParallelsHandler: '{maxParallelsHandler}'",
            rabbitConnectionFactory.ConnectionString, maxParallelsHandler);
    }

    public abstract Task HandleMessage(T message);

    public Task StopConsume()
    {
        _channel.Close();
        _connection.Close();
        _logger.LogInformation("Consume stopped. RabbitMq queue '{queue}'", _queue);
        return Task.CompletedTask;
    }

    public Task StartConsume()
    {
        _channel.QueueDeclare(queue: _queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += OnConsumerOnReceived;
        _ = _channel.BasicConsume(_queue, false, consumer);
        _logger.LogInformation("Consume started. RabbitMq queue '{queue}'", _queue);

        return Task.CompletedTask;
    }

    private async void OnConsumerOnReceived(object? _, BasicDeliverEventArgs ea)
    {
        await _semaphore.WaitAsync();
        _logger.LogDebug("Semaphore count {count}", _semaphore.CurrentCount);
        string body = Encoding.UTF8.GetString(ea.Body.ToArray());
        try
        {
            _logger.LogInformation("Received new message with RoutingKey: '{key}'. Message: '{body}'", ea.RoutingKey,
                body);
            if (string.IsNullOrWhiteSpace(body))
            {
                _logger.LogError($"Received message is empty");
                _channel.BasicAck(ea.DeliveryTag, false);
                return;
            }

            var obj = JsonConvert.DeserializeObject<T>(body, JsonSerializerHelper.GetTypeNameHandlingNoneSettings());
            if (obj == null)
            {
                _logger.LogWarning("Can not deserialize object to '{type}' from message '{body}'", typeof(T).Name,
                    body);
                _channel.BasicAck(ea.DeliveryTag, false);
                return;
            }

            await HandleMessage(obj);
            _channel.BasicAck(ea.DeliveryTag, false);
        }
        catch (JsonReaderException ex)
        {
            _logger.LogError(ex, "Error when deserialize object to '{type}' from message '{body}'", typeof(T).Name,
                body);
            _channel.BasicAck(ea.DeliveryTag, false);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _channel.Dispose();
                _connection.Dispose();
                _semaphore.Dispose();
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