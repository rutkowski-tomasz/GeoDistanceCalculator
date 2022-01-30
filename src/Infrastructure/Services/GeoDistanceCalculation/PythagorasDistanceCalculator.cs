using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Services.GeoDistanceCalculation;

public class PythagorasDistanceCalculator : IGeoDistanceCalculator
{
    public GeoDistanceCalculationMethod Method => GeoDistanceCalculationMethod.Pythagoras;
    
    public async Task<Distance> CalculateDistanceAsync(GeoLocation initialLocation, GeoLocation targetLocation)
    {
        var eastWestDelta = DegreesToRadians(targetLocation.Longitude.Value - initialLocation.Longitude.Value) * Math.Cos(DegreesToRadians(initialLocation.Latitude.Value));
        var northSouthDelta = DegreesToRadians(90 - targetLocation.Latitude.Value) - DegreesToRadians(90 - initialLocation.Latitude.Value);

        var distanceValue = Math.Sqrt(eastWestDelta * eastWestDelta + northSouthDelta * northSouthDelta) * GeoConstants.EarthRadius;
        
        var distance = new Distance
        {
            Unit = DistanceUnit.Kilometer,
            Value = distanceValue
        };
        
        return await Task.FromResult(distance);
    }

    private double DegreesToRadians(double degrees)
    {
        return degrees * (Math.PI / 180);
    }
}