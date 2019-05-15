using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blog.Application.Modules;
using Blog.Persistance;
using BlogSolution.Authentication;
using BlogSolution.Consul;
using BlogSolution.Context;
using BlogSolution.EventBusRabbitMQ;
using BlogSolution.Mvc;
using BlogSolution.Shared.Initializers;
using BlogSolution.Shared.Options;
using BlogSolution.Types.Settings;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddApiBehaviorOptions();
            services.AddJwt();
            services.AddOption<AppSettings>("app");
            services.AddCustomDbContext<BlogDbContext>();
            services.AddCustomMvc();
            services.AddScoped<IBlogDbContextInitializer, BlogDbContextInitializer>();
            services.AddInitializers(typeof(IBlogDbContextInitializer));
            services.AddIntegrationServices();
            services.AddEventBus();
            services.AddServiceDiscovery();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors =>
                        cors.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
            });

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new MapperModule());
            builder.RegisterModule(new IntegrationModule());
            Container = builder.Build();
            return new AutofacServiceProvider(Container);
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IStartupInitializer startupInitializer, 
            IApplicationLifetime applicationLifetime, IConsulClient consulClient)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseAllForwardedHeaders();
            app.UseErrorHandler();
            app.UseAuthentication();
            app.UseMvc();

            startupInitializer.InitializeAsync();
            var serviceId = app.UseConsulRegisterService();

            applicationLifetime.ApplicationStopped.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(serviceId);
                Container.Dispose();
            });
        }
    }
}