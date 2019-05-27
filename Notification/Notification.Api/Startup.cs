using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogSolution.EventBus.Abstractions;
using BlogSolution.EventBusRabbitMQ;
using BlogSolution.Mvc;
using BlogSolution.Shared.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Api.IntegrationEvents.EventHandlers;
using Notification.Api.IntegrationEvents.Events;
using Notification.Application.Modules;
using Notification.Api.IntegrationEvents;

namespace Notification.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddCustomMvc();
            services.AddApiBehaviorOptions();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors =>
                        cors.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
            });

            services.AddIntegrationServices();
            services.AddEventBus();
            services.AddEventHandlers();
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.AddMail();
            builder.RegisterModule(new ApplicationModule());
            Container = builder.Build();
            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime)
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
            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<ForgotPasswordIntegrationEvent, ForgotPasswordIntegrationEventHandler>();
        }


    }
  
}
