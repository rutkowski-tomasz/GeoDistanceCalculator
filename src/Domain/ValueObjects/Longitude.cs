using Domain.Exceptions;
using ValueOf;

namespace Domain.ValueObjects;

public class Longitude : ValueOf<double, Longitude>
{
    protected override void Validate()
    {
        if (Value is < -180 or > 180)
            throw new InvalidLongitudeValue(Value);
    }
}