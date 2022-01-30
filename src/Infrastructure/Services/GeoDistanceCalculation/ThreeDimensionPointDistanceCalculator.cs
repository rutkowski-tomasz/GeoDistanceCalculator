using System.Numerics;
using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Services.GeoDistanceCalculation;

public class ThreeDimensionPointDistanceCalculator : IGeoDistanceCalculator
{
    public GeoDistanceCalculationMethod Method => GeoDistanceCalculationMethod.ThreeDimensionPoint;
    
    public async Task<Distance> CalculateDistanceAsync(GeoLocation locationA, GeoLocation locationB)
    {
        var aVector = CalculatePositionVector(locationA);
        var bVector = CalculatePositionVector(locationB);

        var delta = aVector - bVector;
        var distanceValue = GeoConstants.EarthRadius * Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y + delta.Z * delta.Z);

        var distance = Distance.From(distanceValue, DistanceUnit.Kilometer);

        return await Task.FromResult(distance);
    }


    private Vector3 CalculatePositionVector(GeoLocation location)
    {
        var vector = new Vector3();

        vector.X = (float) (Math.Sin(location.Latitude.Value) * Math.Cos(location.Longitude.Value));
        vector.Y = (float) (Math.Sin(location.Latitude.Value) * Math.Sin(location.Longitude.Value));
        vector.Z = (float) (Math.Cos(location.Latitude.Value));

        return vector;
    }
}