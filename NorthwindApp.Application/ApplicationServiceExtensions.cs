using Microsoft.Extensions.DependencyInjection;

namespace NorthwindApp.Application
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}