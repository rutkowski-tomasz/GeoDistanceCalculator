using Domain.Enums;

namespace Application.GeoDistance.Calculate;

public class CalculateGeoDistanceRequest
{
    public double InitialLatitude { get; set; }
    public double InitialLongitude { get; set; }
    public double TargetLatitude { get; set; }
    public double TargetLongitude { get; set; }
    public DistanceUnit Unit { get; set; }
    public GeoDistanceCalculationMethod Method { get; set; }
}