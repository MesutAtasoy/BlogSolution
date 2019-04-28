using BlogSolution.EventBus.Events;
using System;

namespace Notification.Api.IntegrationEvents.Events
{
    public class ForgotPasswordIntegrationEvent : IntegrationEvent
    {
        public Guid ActivationCode { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }


        public ForgotPasswordIntegrationEvent(Guid activationCode, string username,string email)
        {
            ActivationCode = activationCode;
            Username = username;
            Email = email;
        }
    }
}
