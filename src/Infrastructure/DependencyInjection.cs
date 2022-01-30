using System.Reflection;
using Application.Common.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddTransient<IGeoDistanceService, GeoCurveDistanceService>();
        services.AddTransient<IDistanceConversionService, DistanceConversionService>();

        return services;
    }
}