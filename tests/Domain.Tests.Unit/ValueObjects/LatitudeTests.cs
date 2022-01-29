using Domain.Exceptions;
using Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Unit.ValueObjects;

public class LatitudeTests
{
    [Fact]
    public void ShouldCreateValidInstanceGivenValidValue()
    {
        var latitude = Latitude.From(6.9);

        var value = latitude.Value;

        value.Should().Be(6.9);
    }

    [Theory]
    [InlineData(-90.01)]
    [InlineData(90.01)]
    public void ShouldThrowInvalidLatitudeExceptionGivenInvalidValue(double value)
    {
        var action = () => Latitude.From(value);

        action.Should()
            .Throw<InvalidLatitudeValue>()
            .WithMessage("*Invalid value for latitude given*");
    }
}