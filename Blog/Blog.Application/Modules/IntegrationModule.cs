using Autofac;
using Blog.Application.IntegrationEvents;

namespace Blog.Application.Modules
{
    public class IntegrationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BlogIntegrationEventService>()
               .As<IBlogIntegrationEventService>()
               .InstancePerLifetimeScope();
        }
    }
}
