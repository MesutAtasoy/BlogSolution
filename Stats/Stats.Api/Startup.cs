using BlogSolution.Framework.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;
using System;
using BlogSolution.Framework.Authentication;
using BlogSolution.Framework.Mvc;
using BlogSolution.Framework.Options;
using Stats.Application.Modules;
using Autofac;

namespace Stats.Api
{
    public class Startup
    {
        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddApiBehaviorOptions();
            services.AddJwt();
            services.AddOption<AppSettings>("app");
            services.AddCustomMvc();
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
            builder.AddMongo();
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new ValidatorModule());
            Container = builder.Build();

            return new AutofacServiceProvider(Container);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
        }
    }
}
