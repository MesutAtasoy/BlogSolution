using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using System.Linq;
using System;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using BlogSolution.Types;
using System.Net;
using BlogSolution.Shared.Initializers;

namespace BlogSolution.Mvc
{
    public static class Extensions
    {
        public static IMvcCoreBuilder AddCustomMvc(this IServiceCollection services)
        {           
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IStartupInitializer, StartupInitializer>();

            return services
                .AddMvcCore()
                .AddJsonFormatters()
                .AddDataAnnotations()
                .AddApiExplorer()
                .AddDefaultJsonOptions()
                .AddAuthorization()
                .AddFluentValidation();
        }

        public static IMvcCoreBuilder AddDefaultJsonOptions(this IMvcCoreBuilder builder)
           => builder.AddJsonOptions(o =>
           {
               o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
               o.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
               o.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
               o.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
               o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
               o.SerializerSettings.Formatting = Formatting.Indented;
           });

        public static IServiceCollection AddApiBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();                   
                    return new BadRequestObjectResult(new ApiBaseResponse(HttpStatusCode.BadRequest,ApplicationStatusCode.AnErrorHasOccured, errors, "Validation errors"));
                };
            });
            return services;
        }


        public static IServiceCollection AddInitializers(this IServiceCollection services, params Type[] initializers)
          => initializers == null
              ? services
              : services.AddTransient<IStartupInitializer, StartupInitializer>(c =>
              {
                  var startupInitializer = new StartupInitializer();
                  var validInitializers = initializers.Where(t => typeof(IInitializer).IsAssignableFrom(t));
                  foreach (var initializer in validInitializers)
                  {
                      startupInitializer.AddInitializer(c.GetService(initializer) as IInitializer);
                  }

                  return startupInitializer;
              });

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ErrorHandlerMiddleware>();

        public static IApplicationBuilder UseAllForwardedHeaders(this IApplicationBuilder builder)
            => builder.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

       


    }
}
