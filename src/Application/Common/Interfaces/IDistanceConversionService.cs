using Domain.Entities;
using Domain.Enums;

namespace Application.Common.Interfaces;

public interface IDistanceConversionService
{
    Distance Convert(Distance distance, DistanceUnit unit);
}