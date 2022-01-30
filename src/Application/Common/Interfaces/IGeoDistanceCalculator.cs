using Domain.Entities;
using Domain.Enums;

namespace Application.Common.Interfaces;

public interface IGeoDistanceCalculator
{
    Task<Distance> CalculateDistanceAsync(
        GeoLocation initialLocation,
        GeoLocation targetLocation
    );
    
    GeoDistanceCalculationMethod Method { get; }
}