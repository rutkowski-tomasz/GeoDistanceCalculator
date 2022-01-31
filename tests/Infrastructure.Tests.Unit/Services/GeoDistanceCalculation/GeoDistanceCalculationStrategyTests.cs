using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Infrastructure.Services.GeoDistanceCalculation;
using Moq;
using Xunit;

namespace Infrastructure.Tests.Unit.Services.GeoDistanceCalculation;

public class GeoDistanceCalculationStrategyTests
{
    private GeoDistanceCalculationStrategy _calculationStrategy;

    public GeoDistanceCalculationStrategyTests()
    {
        var firstStrategyMock = new Mock<IGeoDistanceCalculator>();
        firstStrategyMock.Setup(x => x.Method).Returns(GeoDistanceCalculationMethod.ThreeDimensionPoint);
        firstStrategyMock
            .Setup(x => x.CalculateDistanceAsync(It.IsAny<GeoLocation>(), It.IsAny<GeoLocation>()))
            .ReturnsAsync(new Distance { Unit = DistanceUnit.Kilometer, Value = 1d });
        
        var secondStrategyMock = new Mock<IGeoDistanceCalculator>();
        secondStrategyMock.Setup(x => x.Method).Returns(GeoDistanceCalculationMethod.Pythagoras);
        secondStrategyMock
            .Setup(x => x.CalculateDistanceAsync(It.IsAny<GeoLocation>(), It.IsAny<GeoLocation>()))
            .ReturnsAsync(new Distance { Unit = DistanceUnit.Kilometer, Value = 2d });

        _calculationStrategy = new GeoDistanceCalculationStrategy(new[] { firstStrategyMock.Object, secondStrategyMock.Object });
    }

    [Theory]
    [InlineData(GeoDistanceCalculationMethod.Pythagoras, 2d)]
    [InlineData(GeoDistanceCalculationMethod.ThreeDimensionPoint, 1d)]
    public async Task ShouldChooseCorrectStrategyBasingOnProvidedMethod(GeoDistanceCalculationMethod method, double expectedDistanceValue)
    {
        var initialLocation = new GeoLocation();
        var targetLocation = new GeoLocation();
        
        var distance = await _calculationStrategy.CalculateDistanceAsync(initialLocation, targetLocation, method);

        distance.Value.Should().Be(expectedDistanceValue);
    }
}