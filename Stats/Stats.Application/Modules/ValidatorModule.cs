using Autofac;
using FluentValidation;
using Stats.Application.Models;
using Stats.Application.Models.Validators;

namespace Stats.Application.Modules
{
    public class ValidatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<CommentRequestModelValidator>()
                .As<IValidator<CommentRequestModel>>()
                .SingleInstance();

        }
    }
}
