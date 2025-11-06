using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NorthwindApp.Application;
using NorthwindApp.Infrastructure;
using System.Text;
using Microsoft.AspNetCore.Identity;
using NorthwindApp.Domain;

namespace NorthwindApp.WebApi
{
    public static class ServiceConfigs
    {
        public static void AddServiceConfigs(this IServiceCollection services, WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<NorthwindAppDbContext>()
                .AddRoles<IdentityRole<int>>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddRoleManager<RoleManager<IdentityRole<int>>>()
                .AddDefaultTokenProviders();
            
            services.AddInfrastructureServices(builder.Configuration);
            services.AddApplicationServices();
            
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Events.OnRedirectToLogin = context =>
                {
                    if (context.Request.Path.StartsWithSegments("/api"))
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    }
                    
                    context.Response.Redirect(context.RedirectUri);
                    return Task.CompletedTask;
                };
                
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    if (context.Request.Path.StartsWithSegments("/api"))
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return Task.CompletedTask;
                    }

                    context.Response.Redirect(context.RedirectUri);
                    return Task.CompletedTask;
                };
            });
        }
    }
}