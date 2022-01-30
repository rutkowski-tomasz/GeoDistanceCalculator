using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using Infrastructure.Services;
using Xunit;

namespace Infrastructure.Tests.Unit.Services;

public class GeoCurveDistanceServiceTests : IClassFixture<GeoCurveDistanceService>
{
    private readonly GeoCurveDistanceService _geoCurveDistanceService;

    public GeoCurveDistanceServiceTests(GeoCurveDistanceService geoCurveDistanceService)
    {
        _geoCurveDistanceService = geoCurveDistanceService;
    }
    
    [Fact]
    public async Task ShouldCalculateApproximatelyValidDistance()
    {
        var locationA = new GeoLocation
        {
            Latitude = Latitude.From(53.297975),
            Longitude = Longitude.From(-6.372663)
        };

        var locationB = new GeoLocation
        {
            Latitude = Latitude.From(41.385101),
            Longitude = Longitude.From(-81.440440)
        };

        var distance = await _geoCurveDistanceService.CalculateDistanceAsync(locationA, locationB);

        distance.Value.Should().BeApproximately(5536, 1);
    }
}