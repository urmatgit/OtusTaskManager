using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RabbitMq.Connector.Consumer;

using RabbitMQ.Connector;
using RabbitMQ.Messages;

namespace NotifyService.Business
{
    public class NotifyConsumer : BaseRabbitMqConsumer<NotifyMessage>, IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public NotifyConsumer(IRabbitConnectionFactory rabbitConnectionFactory,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<NotifyConsumer> logger,
            string queue,
            int maxParallelHandler) : base(rabbitConnectionFactory, queue, maxParallelHandler, logger) 
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task HandleMessage(NotifyMessage message)
        {
            var scope = _serviceScopeFactory.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<INotifyHandler>();
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