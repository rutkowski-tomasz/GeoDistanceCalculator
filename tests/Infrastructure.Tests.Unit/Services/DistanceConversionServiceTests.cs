using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Infrastructure.Services;
using Xunit;

namespace Infrastructure.Tests.Unit.Services;


public class DistanceConversionServiceTests : IClassFixture<DistanceConversionService>
{
    private readonly DistanceConversionService _geoCurveDistanceService;

    public DistanceConversionServiceTests(DistanceConversionService geoCurveDistanceService)
    {
        _geoCurveDistanceService = geoCurveDistanceService;
    }

    [Fact]
    public void ShouldCorrectlyConvertDistanceFromKilometersToMiles()
    {
        var distance = new Distance
        {
            Unit = DistanceUnit.Kilometer,
            Value = 10
        };

        var converted = _geoCurveDistanceService.Convert(distance, DistanceUnit.Mile);

        converted.Unit.Should().Be(DistanceUnit.Mile);
        converted.Value.Should().BeApproximately(6.2137, 0.0001);
    }
    
    [Fact]
    public void ShouldCorrectlyConvertDistanceFromMilesToKilometers()
    {
        var distance = new Distance
        {
            Unit = DistanceUnit.Mile,
            Value = 5
        };

        var converted = _geoCurveDistanceService.Convert(distance, DistanceUnit.Kilometer);

        converted.Unit.Should().Be(DistanceUnit.Kilometer);
        converted.Value.Should().BeApproximately(8.0467, 0.0001);
    }
}