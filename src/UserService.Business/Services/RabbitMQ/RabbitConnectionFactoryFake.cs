using RabbitMQ.Client;
using RabbitMQ.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Business.Services.RabbitMQ
{
    public class RabbitConnectionFactoryFake(string connectionString) : IRabbitConnectionFactory
    {
        public string ConnectionString => throw new NotImplementedException();

        public IConnection CreateConnection()
        {
            return null;
        }
    }
}
