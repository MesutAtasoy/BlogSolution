using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogSolution.Authentication;
using BlogSolution.Context;
using BlogSolution.Mvc;
using BlogSolution.Types.Settings;
using BlogSolution.Shared.Options;
using Identity.Application.Modules;
using Identity.Application.Settings;
using Identity.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using BlogSolution.Shared.Initializers;
using BlogSolution.EventBusRabbitMQ;

namespace Identity.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }


        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddOption<AppSettings>("app");
            services.AddOption<IdentitySettings>("identitySettings");
            services.AddJwt();
            services.AddCustomMvc();
            services.AddApiBehaviorOptions();
            services.AddResponseCaching();
            services.AddCustomDbContext<IdentityDbContext>();
            services.AddTransient<IIdentityDbContextInitilizer, IdentityDbContextInitilizer>();
            services.AddInitializers(typeof(IIdentityDbContextInitilizer));
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
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new ValidatorModule());
            builder.Populate(services);
            Container = builder.Build();
            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IStartupInitializer startupInitializer,
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
            startupInitializer.InitializeAsync();
        }
    }
}
