using BlogSolution.EventBus.Abstractions;
using BlogSolution.Shared.Mail;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ForgotPasswordIntegrationEventHandler> _logger;

        public ForgotPasswordIntegrationEventHandler(IMailService mailService, 
            MailOptions options, 
            ILogger<ForgotPasswordIntegrationEventHandler> logger)
        {
            _mailService = mailService;
            _options = options;
            _logger = logger;
        }

        public async Task Handle(ForgotPasswordIntegrationEvent @event)
        {
            _logger.LogInformation("Succesfully subscribed ForgotPasswordIntegrationEvent");
            string body = $"Your activation code is {@event.ActivationCode}";
            var mail = new MailMessage(_options.Email, @event.Email, "Forgot Password", body);
            await _mailService.SendAsync(mail);
            _logger.LogInformation("Succesfully finished the Forgot Password Integration Event");
        }
    }

}
