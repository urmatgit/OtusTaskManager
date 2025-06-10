using Hangfire.Business.Abstract;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RabbitMq.Connector.Consumer;

using RabbitMQ.Connector;

namespace Hangfire.Business.Rabbit
{
    public class HangfireConsumer : BaseRabbitMqConsumer<RegisterMessage>, IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public HangfireConsumer(IRabbitConnectionFactory rabbitConnectionFactory,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<HangfireConsumer> logger,
            string queue,
            int maxParallelHandler) : base(rabbitConnectionFactory, queue, maxParallelHandler, logger) 
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async override Task HandleMessage(RegisterMessage message)
        {
            var scope = _serviceScopeFactory.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IRabbitMessageHandler>();
            await service.HandleMessage(message);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await StartConsume();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await StopConsume();
        }
    }
}