using Hangfire.Business.Rabbit;

namespace Hangfire.Business.Abstract
{
    public interface IRabbitMessageHandler
    {
        Task HandleMessage(RegisterMessage registerMessage);
    }
}