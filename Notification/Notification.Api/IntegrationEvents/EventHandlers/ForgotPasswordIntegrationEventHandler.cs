using BlogSolution.EventBus.Abstractions;
using BlogSolution.Shared.Mail;
using Notification.Api.IntegrationEvents.Events;
using Notification.Application.Contracts;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Notification.Api.IntegrationEvents.EventHandlers
{
    public class ForgotPasswordIntegrationEventHandler : IIntegrationEventHandler<ForgotPasswordIntegrationEvent>
    {
        private readonly IMailService _mailService;
        private readonly MailOptions _options;
        public ForgotPasswordIntegrationEventHandler(IMailService mailService, MailOptions options)
        {
            _mailService = mailService;
            _options = options;
        }

        public async Task Handle(ForgotPasswordIntegrationEvent @event)
        {
            string body = $"Your activation code is {@event.ActivationCode}";
            var mail = new MailMessage(_options.Email, @event.Email, "Forgot Password", body);
            await _mailService.SendAsync(mail);
        }
    }

}
