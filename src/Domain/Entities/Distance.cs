using Domain.Enums;

namespace Domain.Entities;

public class Distance
{
    public double Value { get; set; }
    public DistanceUnit Unit { get; set; }

    public static Distance From(double value, DistanceUnit unit)
    {
        return new Distance
        {
            Value = value,
            Unit = unit
        };
    }
}