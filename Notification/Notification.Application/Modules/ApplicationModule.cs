using Autofac;
using Notification.Application.Contracts;
using Notification.Application.Services;

namespace Notification.Application.Modules
{
    public class ApplicationModule : Module
    {
        public ApplicationModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<MailService>()
                .As<IMailService>()
                .InstancePerLifetimeScope();
        }
    }
}
