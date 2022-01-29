using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IGeoDistanceService
{
    Task<double> CalculateDistanceAsync(
        GeoLocation locationA,
        GeoLocation locationB
    );
}