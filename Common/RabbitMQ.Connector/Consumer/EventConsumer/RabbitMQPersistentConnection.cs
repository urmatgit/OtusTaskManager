using System.Collections.Concurrent;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Connector;

namespace RabbitMq.Connector.Consumer.EventConsumer
{
    public class RabbitMqPersistentConnection : IRabbitMqPersistentConnection
    {
        private readonly ConcurrentDictionary<string, IConnection> _connections = new();
        private readonly IRabbitConnectionFactory _rabbitConnectionFactory;
        private readonly ILogger<RabbitMqPersistentConnection> _logger;

        public RabbitMqPersistentConnection(IRabbitConnectionFactory rabbitConnectionFactory,
            ILogger<RabbitMqPersistentConnection> logger)
        {
            _rabbitConnectionFactory = rabbitConnectionFactory;
            _logger = logger;
        }

        /// <summary>
        /// Создание соединений
        /// </summary>
        /// <returns></returns>
        public IConnection Connect(string connectionName)
        {
            try
            {
                var rabbitConnection = _rabbitConnectionFactory.CreateConnection();
                var connection = _connections.GetOrAdd(connectionName, rabbitConnection);
                _logger.LogInformation("RabbitMq connection '{connectionUrl}' successfully created",
                    _rabbitConnectionFactory.ConnectionString);
                return connection;
            }
            catch (SocketException e)
            {
                _logger.LogError(e, "Unable to connect RabbitMq '{connectionUrl}'",
                    _rabbitConnectionFactory.ConnectionString);
                throw;
            }
        }

        public IModel CreateModel(string connectionName)
        {
            if (!IsConnected(connectionName))
                Connect(connectionName);
            var tryResult = _connections.TryGetValue(connectionName, out var connection);
            if (!tryResult)
                throw new InvalidOperationException($"Connection with name '{connectionName}' not found!");
            var channel = connection!.CreateModel();
            return channel;
        }

        public bool IsConnected(string connectionName)
        {
            var connection = _connections.GetValueOrDefault(connectionName);
            return connection is { IsOpen: true };
        }
    }
}