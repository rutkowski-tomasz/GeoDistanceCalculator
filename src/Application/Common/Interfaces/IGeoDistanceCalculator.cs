﻿using Domain.Entities;
using Domain.Enums;

namespace Application.Common.Interfaces;

public interface IGeoDistanceCalculator
{
    Task<Distance> CalculateDistanceAsync(
        GeoLocation locationA,
        GeoLocation locationB
    );
    
    GeoDistanceCalculationMethod Method { get; }
}