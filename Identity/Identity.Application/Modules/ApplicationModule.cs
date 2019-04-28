using Autofac;
using Identity.Application.Contracts;
using Identity.Application.IntegrationEvents;
using Identity.Application.Services;

namespace Identity.Application.Modules
{
    public class ApplicationModule : Module
    {
        public ApplicationModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {  

            builder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RoleService>()
                .As<IRoleService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<IdentityService>()
              .As<IIdentityService>()
              .InstancePerLifetimeScope();

            builder.RegisterType<UserPasswordService>()
               .As<IUserPasswordService>()
               .InstancePerLifetimeScope();

            RegisterIntegrationEvents(builder);
        }


        private void RegisterIntegrationEvents(ContainerBuilder builder)
        {
            builder.RegisterType<IdentityIntegrationEventService>()
               .As<IIdentityIntegrationEventService>()
               .InstancePerLifetimeScope();
        }
    }
}
