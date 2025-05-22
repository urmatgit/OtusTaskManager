namespace RabbitMq.Connector.Consumer;

public interface IBrokerConsumer<in T> where T : class
{
    Task StartConsume();    
    Task StopConsume();
    Task HandleMessage(T message);
}