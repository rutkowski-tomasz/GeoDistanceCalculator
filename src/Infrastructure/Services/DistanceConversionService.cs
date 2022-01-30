using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Services;

public class DistanceConversionService : IDistanceConversionService
{
    public Distance Convert(Distance distance, DistanceUnit unit)
    {
        return distance;
    }
}