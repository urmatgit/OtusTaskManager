using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Business.Services.Mail
{
    public interface IMailService
    {
         Task SendAsync(MailRequest request, CancellationToken ct);
    }
}
