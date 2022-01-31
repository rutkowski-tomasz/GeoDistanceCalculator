using Domain.Enums;

namespace Application.GeoDistance.Calculate;

public class CalculateGeoDistanceResponse
{
    public double Value { get; set; }
    public DistanceUnit Unit { get; set; }
    public GeoDistanceCalculationMethod Method { get; set; }
}