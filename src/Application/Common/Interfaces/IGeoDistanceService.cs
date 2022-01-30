using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IGeoDistanceService
{
    Task<Distance> CalculateDistanceAsync(
        GeoLocation locationA,
        GeoLocation locationB
    );
}