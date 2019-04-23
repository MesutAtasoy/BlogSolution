using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogSolution.Framework.Authentication;
using BlogSolution.Framework.Context;
using BlogSolution.Framework.Initializers;
using BlogSolution.Framework.Mvc;
using BlogSolution.Framework.Options;
using Identity.Application.Modules;
using Identity.Application.Settings;
using Identity.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Identity.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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

            var builder = new ContainerBuilder();
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new ValidatorModule());
            builder.Populate(services);
            return new AutofacServiceProvider(builder.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IStartupInitializer startupInitializer)
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
