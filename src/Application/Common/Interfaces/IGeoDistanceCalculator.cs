using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IGeoDistanceCalculator
{
    Task<Distance> CalculateDistanceAsync(
        GeoLocation locationA,
        GeoLocation locationB
    );
}