using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using UnitsNet;
using UnitsNet.Units;

namespace Infrastructure.Services;

public class DistanceConversionService : IDistanceConversionService
{
    public Distance Convert(Distance distance, DistanceUnit unit)
    {
        // TODO: implement mapper
        var sourceUnit = distance.Unit == DistanceUnit.Kilometer ? LengthUnit.Kilometer : LengthUnit.Mile;
        var targetUnit = unit == DistanceUnit.Kilometer ? LengthUnit.Kilometer : LengthUnit.Mile;
        
        var length = Length.From(distance.Value, sourceUnit).ToUnit(targetUnit);
        var convertedDistance = Distance.From(length.Value, unit);

        return convertedDistance;
    }
}