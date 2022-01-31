using System;
using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Domain.Enums;
using FluentAssertions;
using Xunit;

namespace Infrastructure.Tests.Unit.Services.GeoDistanceCalculation;

public class GeoDistanceCalculatorTests
{
    private readonly IEnumerable<IGeoDistanceCalculator> _distanceCalculators;

    public GeoDistanceCalculatorTests(IEnumerable<IGeoDistanceCalculator> distanceCalculators)
    {
        _distanceCalculators = distanceCalculators;
    }
    
    [Fact]
    public void ShouldImplementEachGeoDistanceCalculationMethodExactlyOnce()
    {
        var implementedMethods = _distanceCalculators
            .Select(x => x.Method);

        var availableMethods = Enum
            .GetValues(typeof(GeoDistanceCalculationMethod))
            .OfType<GeoDistanceCalculationMethod>()
            .ToList();

        implementedMethods.Should().BeEquivalentTo(availableMethods);
    }
}