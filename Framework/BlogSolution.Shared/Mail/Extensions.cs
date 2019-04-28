using Autofac;
using BlogSolution.Shared.Options;
using Microsoft.Extensions.Configuration;

namespace BlogSolution.Shared.Mail
{
    public static class Extensions
    {
        public static void AddMail(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<MailOptions>("mailOptions");

                return options;
            }).SingleInstance();
        }
    }
}
