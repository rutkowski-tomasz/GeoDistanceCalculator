using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Entities;

namespace Infrastructure.Services;

public class GeoCurveDistanceService : IGeoDistanceService
{
    public async Task<double> CalculateDistanceAsync(GeoLocation locationA, GeoLocation locationB)
    {
        var a = DegreesToRadians(90 - locationB.Latitude.Value);
        var b = DegreesToRadians(90 - locationA.Latitude.Value);
        var fi = DegreesToRadians(locationA.Longitude.Value - locationB.Longitude.Value);

        var distance = Math.Acos(Math.Cos(a) * Math.Cos(b) + Math.Sin(a) * Math.Sin(b) * Math.Cos(fi)) * GeoConstants.EarthRadius;
        
        return await Task.FromResult(distance);
    }

    private double DegreesToRadians(double degrees)
    {
        return degrees * (Math.PI / 180);
    }
}