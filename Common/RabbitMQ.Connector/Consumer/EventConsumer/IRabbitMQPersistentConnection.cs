using RabbitMQ.Client;

namespace RabbitMq.Connector.Consumer.EventConsumer
{
    public interface IRabbitMqPersistentConnection
    {
        IConnection Connect(string connectionName);
        bool IsConnected(string connectionName);
        IModel CreateModel(string connectionName);
    }
}