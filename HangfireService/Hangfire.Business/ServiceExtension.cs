using Hangfire.Business.Rabbit;
using Hangfire.Business.Settings;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using RabbitMQ.Connector;

namespace Hangfire.Business
{
    public static class ServiceExtension
    {
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

                if (string.IsNullOrEmpty(queueSettings.NotifyQueue))
                    throw new Exception("SchedulerQueue not specified in QueuesSettings");

                return new HangfireConsumer(rabbitConnectionFactory, serviceScopeFactory, logger, queueSettings.NotifyQueue, queueSettings.MaxParallelsHandler);
            });
        }
    }
}