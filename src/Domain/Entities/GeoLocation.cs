using Domain.ValueObjects;

namespace Domain.Entities;

public class GeoLocation
{
    public Latitude Latitude { get; set; } = Latitude.From(0);
    public Longitude Longitude { get; set; } = Longitude.From(0);
}