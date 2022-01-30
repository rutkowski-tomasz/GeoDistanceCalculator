using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using Infrastructure.Services.GeoDistanceCalculation;
using Xunit;

namespace Infrastructure.Tests.Unit.Services.GeoDistanceCalculation;

public class GeoCurveDistanceCalculatorTests : IClassFixture<GeoCurveDistanceCalculator>
{
    private readonly GeoCurveDistanceCalculator _distanceCalculator;

    public GeoCurveDistanceCalculatorTests(GeoCurveDistanceCalculator distanceCalculator)
    {
        _distanceCalculator = distanceCalculator;
    }
    
    [Fact]
    public async Task ShouldCalculateApproximatelyValidDistance()
    {
        var initialLocation = new GeoLocation
        {
            Latitude = Latitude.From(53.297975),
            Longitude = Longitude.From(-6.372663)
        };

        var targetLocation = new GeoLocation
        {
            Latitude = Latitude.From(41.385101),
            Longitude = Longitude.From(-81.440440)
        };

        var distance = await _distanceCalculator.CalculateDistanceAsync(initialLocation, targetLocation);

        distance.Value.Should().BeApproximately(5536, 1);
    }
}