using Domain.Exceptions;
using Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Unit.ValueObjects;

public class LongitudeTests
{
    [Fact]
    public void ShouldCreateValidInstanceGivenValidValue()
    {
        var longitude = Longitude.From(6.9);

        var value = longitude.Value;

        value.Should().Be(6.9);
    }

    [Theory]
    [InlineData(-180.01)]
    [InlineData(180.01)]
    public void ShouldThrowInvalidLongitudeExceptionGivenInvalidValue(double value)
    {
        var action = () => Longitude.From(value);

        action.Should()
            .Throw<InvalidLongitudeValue>()
            .WithMessage("*Invalid value for longitude given*");
    }
}