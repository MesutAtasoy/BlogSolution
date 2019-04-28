using BlogSolution.Shared.Mail;
using Notification.Application.Contracts;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Notification.Application.Services
{
    public class MailService : IMailService
    {
        private readonly MailOptions _options;


        public MailService(MailOptions options)
        {
            this._options = options;
        }
        public void Send(MailMessage mail)
        {

            SmtpClient client = new SmtpClient()
            {
                Host = _options.SmtpHost,
                Port = _options.Port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_options.Email, _options.Password)
            };

            client.Send(mail);
        }

        public async Task SendAsync(MailMessage mail)
        {
            SmtpClient client = new SmtpClient()
            {
                Host = _options.SmtpHost,
                Port = _options.Port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_options.Email, _options.Password)
            };

            await client.SendMailAsync(mail);
        }
    }
}
