using Hangfire.Business.Rabbit;
using Hangfire.Business.Settings;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using RabbitMq.Connector.Publisher;

using RabbitMQ.Connector;

namespace Hangfire.Business
{
    public static class ServiceExtension
    {
        public static void RegisterConfig(this IServiceCollection serviceCollection, IConfiguration configuration) 
        {
            serviceCollection.AddOptions<QueueSettings>().Bind(configuration.GetSection("Hangfire:Queues"));
        }

        public static void RegisterBroker(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            string? connectionString = configuration.GetConnectionString("RabbitMq");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new Exception("Connection string 'RabbitMq' not specified");

            serviceCollection.AddSingleton<IRabbitConnectionFactory, RabbitConnectionFactory>(_ =>
                new RabbitConnectionFactory(connectionString));
            serviceCollection.AddSingleton<IBrokerPublisher<NotifyMessage>, RabbitMqPublisher<NotifyMessage>>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<RabbitMqPublisher<NotifyMessage>>>();
                var connectionFactory = sp.GetRequiredService<IRabbitConnectionFactory>();
                var queuesSettings = sp.GetRequiredService<IOptions<QueueSettings>>().Value;

                if (string.IsNullOrEmpty(queuesSettings.NotifyQueue))
                    throw new Exception("NotifyQueue not specified in QueuesSettings");

                return new RabbitMqPublisher<NotifyMessage>(logger, connectionFactory, queuesSettings.NotifyQueue);
            });
        }

        public static void RegisterHost(this IServiceCollection serviceCollection, IConfiguration configuration) 
        {
            string? connectionString = configuration.GetConnectionString("RabbitMq");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new Exception("Connection string 'RabbitMq' not specified");

            serviceCollection.AddSingleton<IRabbitConnectionFactory, RabbitConnectionFactory>(_ =>
                new RabbitConnectionFactory(connectionString));

            serviceCollection.AddHostedService(t =>
            {
                var rabbitConnectionFactory = t.GetRequiredService<IRabbitConnectionFactory>();
                var queueSettings = t.GetRequiredService<IOptions<QueueSettings>>().Value;
                var serviceScopeFactory = t.GetRequiredService<IServiceScopeFactory>();
                var logger = t.GetRequiredService<ILogger<HangfireConsumer>>();

                if (string.IsNullOrEmpty(queueSettings.SchedulerQueue))
                    throw new Exception("SchedulerQueue not specified in QueuesSettings");

                return new HangfireConsumer(rabbitConnectionFactory, serviceScopeFactory, logger, queueSettings.SchedulerQueue, queueSettings.MaxParallelsHandler);
            });
        }
    }
}