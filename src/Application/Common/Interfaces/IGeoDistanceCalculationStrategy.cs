using Domain.Entities;
using Domain.Enums;

namespace Application.Common.Interfaces;

public interface IGeoDistanceCalculationStrategy
{
    Task<Distance> CalculateDistanceAsync(
        GeoLocation locationA,
        GeoLocation locationB,
        GeoDistanceCalculationMethod method
    );
}