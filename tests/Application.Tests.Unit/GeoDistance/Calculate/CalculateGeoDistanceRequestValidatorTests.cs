using System.Collections.Generic;
using Application.GeoDistance.Calculate;
using Domain.Enums;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace Application.Tests.Unit.GeoDistance.Calculate;

public class CalculateGeoDistanceRequestValidatorTests
{
    private readonly IValidator<CalculateGeoDistanceRequest> _validator;

    public CalculateGeoDistanceRequestValidatorTests()
    {
        _validator = new CalculateGeoDistanceRequestValidator();
    }

    [Theory]
    [MemberData(nameof(TestCases))]
    public void ShouldCorrectlyValidate(CalculateGeoDistanceRequest request, bool isExpectedValid)
    {
        var validationResult = _validator.Validate(request);

        validationResult.IsValid.Should().Be(isExpectedValid);
    }
    
    public static IEnumerable<object[]> TestCases =>
        new List<object[]>
        {
            new object[]
            {
                new CalculateGeoDistanceRequest
                {
                    Method = GeoDistanceCalculationMethod.Pythagoras,
                    Unit = DistanceUnit.Kilometer,
                    InitialLatitude = 1,
                    InitialLongitude = 2,
                    TargetLatitude = 11,
                    TargetLongitude = 1200
                },
                false
            },
            new object[]
            {
                new CalculateGeoDistanceRequest
                {
                    Method = GeoDistanceCalculationMethod.Pythagoras,
                    Unit = DistanceUnit.Kilometer,
                    InitialLatitude = 1,
                    InitialLongitude = 2,
                    TargetLatitude = 11,
                    TargetLongitude = 12
                },
                true
            },
        };
}