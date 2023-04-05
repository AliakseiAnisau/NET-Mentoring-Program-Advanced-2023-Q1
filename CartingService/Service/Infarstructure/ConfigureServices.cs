using CartingService.Application.Common.Interfaces;
using CartingService.Infrastructure.Persistence;
using CartingService.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CartingService.Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICartingDbContext, CartingDbContext>();
        services.AddTransient<IDateTime, DateTimeService>();

        return services;
    }
}
