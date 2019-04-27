using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogSolution.Shared.Options
{
    public static class Extensions
    {
        public static IServiceCollection AddOption<TClass>(this IServiceCollection services, string sectionName) where TClass : class
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            services.Configure<TClass>(configuration.GetSection(sectionName));
            return services;
        }
    }
}
