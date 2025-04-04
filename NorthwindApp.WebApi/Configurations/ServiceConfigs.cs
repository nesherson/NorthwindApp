using NorthwindApp.Infrastructure;

namespace NorthwindApp.WebApi
{
    public static class ServiceConfigs
    {
        public static IServiceCollection AddServiceConfigs(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddInfrastructureServices(builder.Configuration);

            return services;
        }
    }
}
