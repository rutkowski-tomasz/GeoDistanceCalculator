using Application.Common.Interfaces;
using Ardalis.ApiEndpoints;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Application.Endpoints.GeoDistance;

public class CalculateGeoDistanceRequest
{
    [FromBody]
    public double LocationALatitude { get; set; }
    
    [FromBody]
    public double LocationALongitude { get; set; }
    
    [FromBody]
    public double LocationBLatitude { get; set; }
    
    [FromBody]
    public double LocationBLongitude { get; set; }
}

public class CalculateGeoDistanceEndpoint : EndpointBaseAsync
    .WithRequest<CalculateGeoDistanceRequest>
    .WithResult<double>
{
    private readonly IGeoDistanceService _geoDistanceService;

    public CalculateGeoDistanceEndpoint(IGeoDistanceService geoDistanceService)
    {
        _geoDistanceService = geoDistanceService;
    }

    [HttpPost("geo-distance/calculate")]
    public override async Task<double> HandleAsync(CalculateGeoDistanceRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        var locationA = new GeoLocation
        {
            Latitude = Latitude.From(request.LocationALatitude),
            Longitude = Longitude.From(request.LocationALongitude)
        };
        
        var locationB = new GeoLocation
        {
            Latitude = Latitude.From(request.LocationBLatitude),
            Longitude = Longitude.From(request.LocationBLongitude)
        };
        
        var distance = await _geoDistanceService.CalculateDistanceAsync(locationA, locationB);

        return distance;
    }
}