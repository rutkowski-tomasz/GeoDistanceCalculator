using Domain.Enums;

namespace Domain.Entities;

public class Distance
{
    public double Value { get; set; }
    public DistanceUnit Unit { get; set; }
}