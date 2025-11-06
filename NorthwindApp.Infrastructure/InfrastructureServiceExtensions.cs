using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NorthwindApp.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfigurationManager configurationManager)
    {
        services.AddDbContext<NorthwindAppDbContext>(opts => 
            opts.UseNpgsql(configurationManager.GetConnectionString("NorthwindApp")));
        
        return services;
    }
}
