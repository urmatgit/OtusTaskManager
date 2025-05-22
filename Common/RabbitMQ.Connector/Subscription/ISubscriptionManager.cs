using RabbitMq.Connector.Consumer.EventConsumer;

namespace RabbitMq.Connector.Subscription
{
    public interface ISubscriptionManager
    {
        HandlerOption GetSubscriptionHandler(string key);
        void RegisterHandler(string typeName, HandlerOption handlerOption);
        bool IsHandlersRegistered { get; }
    }
}