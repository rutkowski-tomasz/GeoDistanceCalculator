using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Application.Common.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.GeoDistanceCalculation;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddTransient<IGeoDistanceCalculator, GeoCurveDistanceCalculator>();
        services.AddTransient<IGeoDistanceCalculator, PythagorasDistanceCalculator>();
        services.AddTransient<IGeoDistanceCalculator, ThreeDimensionPointDistanceCalculator>();
        services.AddTransient<IDistanceConversionService, DistanceConversionService>();
        services.AddTransient<IGeoDistanceCalculationStrategy, GeoDistanceCalculationStrategy>();

        return services;
    }
}