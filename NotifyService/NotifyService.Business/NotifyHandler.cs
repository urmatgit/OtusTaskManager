using MailKit.Net.Smtp;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MimeKit;

using NotifyService.Business.Settings;

using RabbitMQ.Messages;

namespace NotifyService.Business
{
    public interface INotifyHandler 
    {
        Task HandleMessage(NotifyMessage notifyMessage);
    }

    public class NotifyHandler(ILogger<NotifyHandler> logger, IOptions<MailSettings> mailOptions) : INotifyHandler
    {
        private readonly ILogger<NotifyHandler> _logger = logger;
        private readonly MailSettings _mailSettings = mailOptions.Value;

        public async Task HandleMessage(NotifyMessage notifyMessage)
        {
            if (string.IsNullOrEmpty(notifyMessage.MailTo)) 
            {
                _logger.LogWarning("Не указан адрес получателя.");
                return;
            }

            var path = "";

            var stream = File.OpenText(path);
            var mailBody = stream.ReadToEnd();
            foreach (var key in notifyMessage.MessageData.Keys)
                mailBody = mailBody.Replace(key, ToHtmlEncodingString(notifyMessage.MessageData[key]));
            stream.Close();

            var message = GetMessage(mailBody, notifyMessage.MessageType, notifyMessage.MailTo, notifyMessage.DisplayName);
            if (message != null) 
            {
                var client = GetMailClient();
                await client.SendAsync(message);
            }
        }

        private string ToHtmlEncodingString(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt))
                return string.Empty;

            return System.Web.HttpUtility.HtmlEncode(txt);
        }

        private MimeMessage? GetMessage(string mailBody, MessageType messageType, string emailAdress, string displayName, string? attachmentName = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_mailSettings.FromDisplayName, _mailSettings.FromEmail));

            if (string.IsNullOrEmpty(emailAdress))
            {
                _logger.LogWarning("Не указан адрес почты получателя уведомления.");
                return null;
            }

            message.Bcc.Add(new MailboxAddress(displayName, emailAdress));
            message.Subject = "";/* GetMailSubject(messageType);*/
            var body = new BodyBuilder()
            {
                HtmlBody = mailBody
            };
            if (!string.IsNullOrEmpty(attachmentName))
            {
                var attachPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, /*manualFolder,*/ attachmentName);
                if (File.Exists(attachPath))
                {
                    var fs = File.OpenRead(attachPath);
                    body.Attachments.Add(attachmentName, fs);
                }
                else
                    _logger.LogWarning("Не удалось загрузить файл вложения по пути {path}.", attachmentName);
            }
            message.Body = body.ToMessageBody();

            return message;
        }

        private SmtpClient GetMailClient()
        {
            SmtpClient smtpClient;
            try
            {
                smtpClient = new SmtpClient();
                if (_mailSettings.IgnoreCertificateValidation)
                    smtpClient.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                _logger.LogInformation("Сonnect to address {serverAddress}, port {serverPort}", _mailSettings.SmtpServerAddress, _mailSettings.SmtpServerPort);
                smtpClient.LocalDomain = _mailSettings.LocalDomain;
                smtpClient.SslProtocols = _mailSettings.SslProtocols;
                smtpClient.CheckCertificateRevocation = _mailSettings.CheckCertificateRevocation;

                if (!_mailSettings.UseSsl)
                    smtpClient.Connect(_mailSettings.SmtpServerAddress, _mailSettings.SmtpServerPort, false);
                else
                    smtpClient.Connect(_mailSettings.SmtpServerAddress, _mailSettings.SmtpServerPort, _mailSettings.SecureSocketOptions);

                smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
                if (!_mailSettings.SmtpDontUseAuthentication)
                    smtpClient.Authenticate(_mailSettings.SmtpAccountLogin, _mailSettings.SmtpAccountPassword);

                _logger.LogInformation("Connected");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Не удалось получить SMTP клиент.");
                throw;
            }
            return smtpClient;
        }
    }
}