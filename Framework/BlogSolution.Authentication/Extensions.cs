using BlogSolution.Authentication.Handlers;
using BlogSolution.Authentication.Models;
using BlogSolution.Shared.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace BlogSolution.Authentication
{
    public static class Extensions
    {
        private static readonly string SectionName = "jwt";

        public static void AddJwt(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            var options = configuration.GetOptions<JwtOptions>(SectionName);
            services.Configure<JwtOptions>(configuration.GetSection(SectionName));
            services.AddSingleton(options);
            services.AddSingleton<IJwtHandler, JwtHandler>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = options.AuthenticationScheme;
            })
                .AddJwtBearer(options.AuthenticationScheme, cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                        ValidateIssuer = true,
                        ValidIssuer = options.Issuer,
                        ValidateAudience = options.ValidateAudience,
                        ValidAudience = options.ValidAudience,
                        ValidateLifetime = options.ValidateLifetime,
                        ClockSkew = TimeSpan.Zero,
                        RequireExpirationTime = true
                    };
                });
        }
    }

}
