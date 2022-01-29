using Domain.ValueObjects;

namespace Domain.Entities;

public class GeoLocation
{
    public Latitude Latitude { get; set; }
    public Longitude Longitude { get; set; }
}