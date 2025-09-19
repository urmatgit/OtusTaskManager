using RabbitMQ.Client;

namespace RabbitMQ.Connector
{
    public class RabbitConnectionFactory(string connectionString) : IRabbitConnectionFactory
    {
        private readonly string _connectionString =
            connectionString ?? throw new ArgumentNullException(nameof(connectionString));

        public IConnection CreateConnection()
        {
            
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_connectionString),
                AutomaticRecoveryEnabled = true,
                RequestedConnectionTimeout = TimeSpan.FromSeconds(60*5),
#if DEBUG
                // Отключение Heartbeat при дебаге
                RequestedHeartbeat = TimeSpan.Zero
#endif
            };
            try
            {
                var connection = factory.CreateConnection();
                return connection;
            }catch (Exception er)
            {
                throw er;
            }
            return null;
        }

        public string ConnectionString => _connectionString;
    }
}