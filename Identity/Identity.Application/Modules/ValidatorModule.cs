using Autofac;
using FluentValidation;
using Identity.Application.Models;
using Identity.Application.Models.Validators;

namespace Identity.Application.Modules
{
    public class ValidatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<LoginRequestModelValidator>()
                .As<IValidator<LoginRequestModel>>()
                .SingleInstance();

            builder.RegisterType<RegisterRequestModelValidator>()
               .As<IValidator<RegisterRequestModel>>()
               .SingleInstance();

            builder.RegisterType<ForgotPasswordModelValidator>()
             .As<IValidator<ForgotPasswordRequestModel>>()
             .SingleInstance();

            builder.RegisterType<ChangePasswordRequestModelValidator>()
             .As<IValidator<ChangePasswordRequestModel>>()
             .SingleInstance();

        }
    }
}
