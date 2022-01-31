using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.GeoDistance.Calculate;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using FluentValidation;
using Moq;
using Xunit;

namespace Application.Tests.Unit.GeoDistance.Calculate;

public class CalculateGeoDistanceEndpointTests
{
    private readonly CalculateGeoDistanceEndpoint _endpoint;
    private readonly Mock<IGeoDistanceCalculationStrategy> _distanceCalculationStrategyMock;
    private readonly Mock<IDistanceConversionService> _distanceConversionServiceMock;

    public CalculateGeoDistanceEndpointTests()
    {
        _distanceCalculationStrategyMock = new Mock<IGeoDistanceCalculationStrategy>();
        _distanceConversionServiceMock = new Mock<IDistanceConversionService>();
        var validator = new Mock<IValidator<CalculateGeoDistanceRequest>>();

        _endpoint = new CalculateGeoDistanceEndpoint(
            _distanceCalculationStrategyMock.Object,
            _distanceConversionServiceMock.Object,
            validator.Object
        );
    }

    [Fact]
    public async Task ShouldCorrectlyHandleRequest()
    {
        var calculatedDistance = new Distance { Unit = DistanceUnit.Kilometer, Value = 31d };
        var convertedDistance = new Distance { Unit = DistanceUnit.Mile, Value = 41d };
        
        _distanceCalculationStrategyMock
            .Setup(x => x.CalculateDistanceAsync(
                It.Is<GeoLocation>(y => y.Latitude.Value == 1d && y.Longitude.Value == 2d),
                It.Is<GeoLocation>(y => y.Latitude.Value == 11d && y.Longitude.Value == 12d),
                GeoDistanceCalculationMethod.Pythagoras
            ))
            .ReturnsAsync(calculatedDistance);

        _distanceConversionServiceMock
            .Setup(x => x.Convert(
                calculatedDistance,
                DistanceUnit.Mile
            ))
            .Returns(convertedDistance);
            
        var request = new CalculateGeoDistanceRequest
        {
            InitialLatitude = 1,
            InitialLongitude = 2,
            TargetLatitude = 11,
            TargetLongitude = 12,
            Method = GeoDistanceCalculationMethod.Pythagoras,
            Unit = DistanceUnit.Mile
        };

        var response = await _endpoint.HandleAsync(request);

        response.Value.Should().Be(41d);
        response.Unit.Should().Be(DistanceUnit.Mile);
        response.Method.Should().Be(GeoDistanceCalculationMethod.Pythagoras);
    }
}