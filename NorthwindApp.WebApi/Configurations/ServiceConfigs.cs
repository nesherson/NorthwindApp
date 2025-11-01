using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NorthwindApp.Application;
using NorthwindApp.Infrastructure;
using System.Text;

namespace NorthwindApp.WebApi
{
    public static class ServiceConfigs
    {
        public static void AddServiceConfigs(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddInfrastructureServices(builder.Configuration);
            services.AddApplicationServices();
        }

        public static void AddJwtAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opts =>
            {
                opts.SaveToken = true;
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
                };
            });
        }
    }
}