using System.Net.Mail;
using System.Threading.Tasks;

namespace Notification.Application.Contracts
{
    public interface IMailService
    {
        void Send(MailMessage mail);
        Task SendAsync(MailMessage mail);

    }
}
