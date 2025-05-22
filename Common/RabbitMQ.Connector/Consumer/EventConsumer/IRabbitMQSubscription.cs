namespace RabbitMq.Connector.Consumer.EventConsumer
{
    public interface IRabbitMqSubscription
    {
        void Subscribe<T>(HandlerOption handlerOption);
        void RegisterConsumer(HandlerOption handlerOption);
    }
}