using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorthwindApp.Application;

namespace NorthwindApp.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfigurationManager configurationManager)
    {
        services.AddDbContext<NorthwindAppDbContext>(opts => 
            opts.UseNpgsql(configurationManager.GetConnectionString("NorthwindApp")));

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
