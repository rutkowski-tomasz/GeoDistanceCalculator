using Domain.Entities;
using Domain.Enums;

namespace Application.Common.Interfaces;

public interface IGeoDistanceCalculationStrategy
{
    Task<Distance> CalculateDistanceAsync(
        GeoLocation initialLocation,
        GeoLocation targetLocation,
        GeoDistanceCalculationMethod method
    );
}