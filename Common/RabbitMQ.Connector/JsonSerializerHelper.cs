using Newtonsoft.Json;

namespace RabbitMQ.Connector
{
    public static class JsonSerializerHelper
    {
        public static JsonSerializerSettings GetTypeNameHandlingNoneSettings()
        {
            return new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };
        }
    }
}