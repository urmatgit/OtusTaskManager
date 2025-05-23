using RabbitMq.Connector.Publisher.EventPublisher;

namespace RabbitMq.Connector.Subscription
{
    public interface ISubscriptionHandler
    {
        Task Handle(MqPersistentModel model);
    }
}