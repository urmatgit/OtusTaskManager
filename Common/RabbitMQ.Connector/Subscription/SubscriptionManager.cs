using RabbitMq.Connector.Consumer.EventConsumer;

namespace RabbitMq.Connector.Subscription
{
    public class SubscriptionManager : ISubscriptionManager
    {
        private readonly Dictionary<string, HandlerOption> _subscriptionHandlers = new();

        public bool IsHandlersRegistered => _subscriptionHandlers.Keys.Count > 0;

        public void RegisterHandler(string typeName, HandlerOption handlerOption)
        {
            ArgumentNullException.ThrowIfNull(typeName);
            ArgumentNullException.ThrowIfNull(handlerOption);

            _subscriptionHandlers.TryAdd(typeName, handlerOption);
        }

        public HandlerOption GetSubscriptionHandler(string key)
        {
            ArgumentNullException.ThrowIfNull(key);
            
            if (_subscriptionHandlers.TryGetValue(key, out var handlerOption))
                return handlerOption;

            throw new InvalidOperationException($"Handler does not exist '{key}'");
        }
    }
}