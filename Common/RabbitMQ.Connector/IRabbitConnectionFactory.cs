using RabbitMQ.Client;

namespace RabbitMQ.Connector
{
    public interface IRabbitConnectionFactory
    {
        IConnection CreateConnection();
        string ConnectionString { get; }
    }
}