using Domain.Exceptions;
using ValueOf;

namespace Domain.ValueObjects;

public class Latitude : ValueOf<double, Latitude>
{
    protected override void Validate()
    {
        if (Value is < -90 or > 90)
            throw new InvalidLatitudeValue(Value);
    }
}