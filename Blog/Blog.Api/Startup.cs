using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blog.Application.Modules;
using Blog.Persistance;
using BlogSolution.Framework.Authentication;
using BlogSolution.Framework.Context;
using BlogSolution.Framework.Initializers;
using BlogSolution.Framework.Mvc;
using BlogSolution.Framework.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Api
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
            services.AddApiBehaviorOptions();
            services.AddJwt();
            services.AddOption<AppSettings>("app");
            services.AddCustomDbContext<BlogDbContext>();
            services.AddCustomMvc();
            services.AddScoped<IBlogDbContextInitializer, BlogDbContextInitializer>();
            services.AddInitializers(typeof(IBlogDbContextInitializer));
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
            return new AutofacServiceProvider(builder.Build());
        }
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