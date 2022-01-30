using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Services.GeoDistanceCalculation;

public class PythagorasDistanceCalculator : IGeoDistanceCalculator
{
    public async Task<Distance> CalculateDistanceAsync(GeoLocation locationA, GeoLocation locationB)
    {
        var eastWestDelta = DegreesToRadians(locationB.Longitude.Value - locationA.Longitude.Value) * Math.Cos(DegreesToRadians(locationA.Latitude.Value));
        var northSouthDelta = DegreesToRadians(90 - locationB.Latitude.Value) - DegreesToRadians(90 - locationA.Latitude.Value);

        var distanceValue = Math.Sqrt(eastWestDelta * eastWestDelta + northSouthDelta * northSouthDelta) * GeoConstants.EarthRadius;
        
        var distance = new Distance
        {
            Unit = DistanceUnit.Kilometer,
            Value = distanceValue
        };
        
        return distance;
    }

    private double DegreesToRadians(double degrees)
    {
        return degrees * (Math.PI / 180);
    }
}