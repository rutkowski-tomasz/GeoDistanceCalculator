using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using UnitsNet;
using UnitsNet.Units;

namespace Infrastructure.Services;

public class DistanceConversionService : IDistanceConversionService
{
    private readonly IMapper _mapper;

    public DistanceConversionService(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public Distance Convert(Distance distance, DistanceUnit unit)
    {
        var sourceUnit = _mapper.Map<LengthUnit>(distance.Unit);
        var targetUnit = _mapper.Map<LengthUnit>(unit);

        var length = Length.From(distance.Value, sourceUnit).ToUnit(targetUnit);
        var convertedDistance = Distance.From(length.Value, unit);

        return convertedDistance;
    }
}