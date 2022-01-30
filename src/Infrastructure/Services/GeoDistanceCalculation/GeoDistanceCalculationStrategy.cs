using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Services.GeoDistanceCalculation;

public class GeoDistanceCalculationStrategy : IGeoDistanceCalculationStrategy
{
    private readonly IEnumerable<IGeoDistanceCalculator> _geoDistanceCalculators;

    public GeoDistanceCalculationStrategy(IEnumerable<IGeoDistanceCalculator> geoDistanceCalculators)
    {
        _geoDistanceCalculators = geoDistanceCalculators;
    }

    public async Task<Distance> CalculateDistanceAsync(
        GeoLocation locationA,
        GeoLocation locationB,
        GeoDistanceCalculationMethod method
    )
    {
        var calculator =  _geoDistanceCalculators.First(x => x.Method == method);
        var distance = await calculator.CalculateDistanceAsync(locationA, locationB);

        return await Task.FromResult(distance);
    }
}