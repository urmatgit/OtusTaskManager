using RabbitMq.Connector.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Enums;

namespace UserService.Business.Services.Mail
{
    public class SendMailThroughRabbtiMQService : IMailService
    {
        private readonly IBrokerPublisher<PublishMassage<MailRequest>> _brokerPublisher;
        public SendMailThroughRabbtiMQService(IBrokerPublisher<PublishMassage<MailRequest>> brokerPublisher)
        {
            _brokerPublisher = brokerPublisher;    
        }
        public async Task SendAsync(MailRequest request, CancellationToken ct)
        {
            var message = new PublishMassage<MailRequest>(request,DateTime.Now,MessageAction.ConfirmEmail);
            await Task.Run(() =>
            {
                _brokerPublisher?.Publish(message);
            });
            
             
        }
    }
}
