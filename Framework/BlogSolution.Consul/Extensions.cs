using BlogSolution.Shared.Options;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

//ToDo: Logger will  implement.


namespace BlogSolution.Consul
{

    public static class ApplicationBuilderExtensions
    {
        public static string UseConsulRegisterService(this IApplicationBuilder app)
        {

            var appLife = app.ApplicationServices.GetRequiredService<IApplicationLifetime>() ?? throw new ArgumentException("Missing dependency", nameof(IApplicationLifetime));
            var serviceOptionsValue = app.ApplicationServices.GetRequiredService<IOptions<ServiceDiscoveryOptions>>() ?? throw new ArgumentException("Missing dependency", nameof(IOptions<ServiceDiscoveryOptions>));
            var consul = app.ApplicationServices.GetRequiredService<IConsulClient>() ?? throw new ArgumentException("Missing dependency", nameof(IConsulClient));


            var serviceOptions = serviceOptionsValue.Value;

            if (!serviceOptions.Enabled)
                return string.Empty;

            if (string.IsNullOrEmpty(serviceOptions.ServiceName))
                throw new ArgumentException("Service Name must be configured", nameof(serviceOptions.ServiceName));

            var serviceChecks = new List<AgentServiceCheck>();

            var serviceId = Guid.NewGuid().ToString();

            
            var registration = new AgentServiceRegistration()
            {
                Checks = serviceChecks.ToArray(),
                Address = serviceOptions.Consul.Address,
                ID = serviceId,
                Name = serviceOptions.ServiceName,
                Port = serviceOptions.Consul.Port
            };


            consul.Agent.ServiceRegister(registration).GetAwaiter().GetResult();

            return serviceId;
        }
    }

    public static class ServiceCollectionExtensions
    {
        private static readonly string SectionName = "serviceDiscovery";

        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services)
        {

            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            services.AddOption<ServiceDiscoveryOptions>("serviceDiscovery");
            services.Configure<ServiceDiscoveryOptions>(configuration);

            var options = configuration.GetOptions<ServiceDiscoveryOptions>(SectionName);

            return services.AddSingleton<IConsulClient>(c => new ConsulClient(cfg =>
            {
                if (!string.IsNullOrEmpty(options.Consul.HttpEndpoint))
                {
                    cfg.Address = new Uri(options.Consul.HttpEndpoint);
                }
            }));
        }
    }

}




