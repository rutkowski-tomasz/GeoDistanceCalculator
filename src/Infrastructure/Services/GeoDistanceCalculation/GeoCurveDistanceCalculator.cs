using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Services.GeoDistanceCalculation;

public class GeoCurveDistanceCalculator : IGeoDistanceCalculator
{
    public GeoDistanceCalculationMethod Method => GeoDistanceCalculationMethod.GeoCurve;
    
    public async Task<Distance> CalculateDistanceAsync(GeoLocation locationA, GeoLocation locationB)
    {
        var a = DegreesToRadians(90 - locationB.Latitude.Value);
        var b = DegreesToRadians(90 - locationA.Latitude.Value);
        var fi = DegreesToRadians(locationA.Longitude.Value - locationB.Longitude.Value);

        var acos = Math.Acos(Math.Cos(a) * Math.Cos(b) + Math.Sin(a) * Math.Sin(b) * Math.Cos(fi));
        var distanceValue = acos * GeoConstants.EarthRadius;

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