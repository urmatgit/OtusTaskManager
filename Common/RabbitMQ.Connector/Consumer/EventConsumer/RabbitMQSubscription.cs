using System.Text;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using RabbitMq.Connector.Publisher.EventPublisher;
using RabbitMq.Connector.Subscription;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Connector;

namespace RabbitMq.Connector.Consumer.EventConsumer
{
    public class RabbitMqSubscription : IRabbitMqSubscription
    {
        private readonly ISubscriptionManager _subscriptionManager;
        private readonly IRabbitMqPersistentConnection _persistentConnection;
        private readonly ILogger<RabbitMqSubscription> _logger;
        private HandlerOption? _handlerOption;
        private IModel? _consumerChannel;
        private const string DefaultConnectionName = "Subscriber";

        private readonly ISubscriptionHandler _subscriptionHandler;

        public RabbitMqSubscription(ISubscriptionManager subscriptionManager,
            ISubscriptionHandler subscriptionHandler,
            IRabbitMqPersistentConnection persistentConnection,
            ILogger<RabbitMqSubscription> logger)
        {
            _subscriptionManager = subscriptionManager;
            _persistentConnection = persistentConnection;
            _logger = logger;
            _subscriptionHandler = subscriptionHandler;
        }

        private IModel CreateConsumerChannel()
        {
            if (_handlerOption is null)
                throw new InvalidOperationException("Before subscribe ir register consumer");

            if (!_persistentConnection.IsConnected(DefaultConnectionName))
            {
                _persistentConnection.Connect(DefaultConnectionName);
            }

            var channel = _persistentConnection.CreateModel(DefaultConnectionName);
            channel.ExchangeDeclare(exchange: _handlerOption.QueueName,
                type: "direct");
            channel.BasicQos(0, 1, false);
            channel.QueueDeclare(queue: _handlerOption.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _logger.LogInformation("Created RabbitMQ consumer channel. Queue '{queue}'", _handlerOption.QueueName);
            return channel;
        }

        private void StartBasicConsume()
        {
            if (_handlerOption is null)
                throw new InvalidOperationException("Before subscribe ir register consumer");

            if (_consumerChannel != null)
            {
                _logger.LogInformation("Starting RabbitMQ basic consume. Queue '{queue}'", _handlerOption.QueueName);

                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                consumer.Received += Consumer_Received;

                _consumerChannel.BasicConsume(
                    queue: _handlerOption.QueueName,
                    autoAck: _handlerOption.AutoAck,
                    consumer: consumer);
            }
            else
            {
                _logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
            }
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            if (_handlerOption is null)
                throw new InvalidOperationException("Before subscribe ir register consumer");

            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

            _logger.LogInformation("Consumed new message from queue '{queue}'. Message: '{message}'. ",
                _handlerOption.QueueName, message);
            try
            {
                var jObject = JObject.Parse(message);
                var model = JsonConvert.DeserializeObject<MqPersistentModel>(message,
                    JsonSerializerHelper.GetTypeNameHandlingNoneSettings());

                if (model == null)
                {
                    _logger.LogWarning("Can not deserialize object to MqPersistentModel from message '{message}'",
                        message);
                    return;
                }

                model.JObject = jObject;

                await ProcessEvent(model);
                if (!_handlerOption.AutoAck)
                {
                    _consumerChannel?.BasicAck(eventArgs.DeliveryTag, false);
                    _logger.LogInformation("Message '{message}' from queue '{queue}' handled.", message,
                        _handlerOption.QueueName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on consume message '{message}'", message);
                _consumerChannel?.BasicNack(eventArgs.DeliveryTag, false, true);
            }
        }

        private async Task ProcessEvent(MqPersistentModel model)
        {
            await _subscriptionHandler.Handle(model);
        }

        /// <summary>
        /// Регистрация подписчика и его обработчика
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handlerOption"></param>
        /// <exception cref="ArgumentException"></exception>
        public void Subscribe<T>(HandlerOption handlerOption)
        {
            ArgumentNullException.ThrowIfNull(handlerOption);

            if (!typeof(T).IsGenericType) throw new ArgumentException("Invalid argument type");

            var type = typeof(T);

            handlerOption.QueueName = string.IsNullOrWhiteSpace(handlerOption.QueueName)
                ? $"{type.Name}.{type.GetGenericArguments()[0].Name}"
                : handlerOption.QueueName;

            RegisterConsumer(handlerOption);
        }

        /// <summary>
        /// Регистрация подписчика и его обработчика
        /// </summary>
        /// <param name="handlerOption"></param>
        public void RegisterConsumer(HandlerOption handlerOption)
        {
            ArgumentNullException.ThrowIfNull(handlerOption);

            _subscriptionManager.RegisterHandler(handlerOption.QueueName, handlerOption);
            _handlerOption = handlerOption;

            if (!_persistentConnection.IsConnected(DefaultConnectionName))
            {
                _persistentConnection.Connect(DefaultConnectionName);
            }

            _consumerChannel = CreateConsumerChannel();

            StartBasicConsume();
        }
    }
}