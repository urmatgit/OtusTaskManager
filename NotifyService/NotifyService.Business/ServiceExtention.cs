using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NotifyService.Business.Settings;

using RabbitMQ.Connector;

namespace NotifyService.Business
{
    public static class ServiceExtention
    {
        public static void AddConfigurations(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddOptions<MailSettings>().Bind(configuration.GetSection("MailSettings"));
            serviceCollection.AddOptions<QueueSettings>().Bind(configuration.GetSection("Queues"));
        }

        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<INotifyHandler, NotifyHandler>();
        }

        public static void AddHostedServices(this IServiceCollection serviceCollection, IConfiguration configuration)
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
                var logger = t.GetRequiredService<ILogger<NotifyConsumer>>();

                if (string.IsNullOrEmpty(queueSettings.NotifyQueue))
                    throw new Exception("SchedulerQueue not specified in QueuesSettings");

                return new NotifyConsumer(rabbitConnectionFactory, serviceScopeFactory, logger, queueSettings.NotifyQueue, queueSettings.MaxParallelsHandler);
            });
        }
    }
}