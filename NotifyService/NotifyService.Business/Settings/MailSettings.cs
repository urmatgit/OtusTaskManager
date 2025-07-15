using System.Security.Authentication;

using MailKit.Security;

namespace NotifyService.Business.Settings
{
    public class MailSettings
    {
        public bool IgnoreCertificateValidation { get; set; }
        public string SmtpServerAddress { get; set; } = default!;
        public int SmtpServerPort { get; set; }
        public string SmtpAccountLogin { get; set; } = default!;
        public string SmtpAccountPassword { get; set; } = default!;
        public string FromEmail { get; set; } = default!;
        public string FromDisplayName { get; set; } = default!;
        public bool SmtpDontUseAuthentication { get; set; }
        public bool SmtpUseSsl { get; set; }
        public string LocalDomain { get; set; } = default!;
        public bool CheckCertificateRevocation { get; set; }
        public SslProtocols SslProtocols { get; set; }
        public SecureSocketOptions SecureSocketOptions { get; set; }
        public string ServerAddress { get; set; } = default!;
        public int ServerPort { get; set; }
        public bool UseSsl { get; set; }
    }
}