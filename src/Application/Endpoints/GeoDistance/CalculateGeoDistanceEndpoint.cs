using Application.Common.Interfaces;
using Ardalis.ApiEndpoints;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Endpoints.GeoDistance;

public class CalculateGeoDistanceRequest
{
    public double LocationALatitude { get; set; }
    public double LocationALongitude { get; set; }
    public double LocationBLatitude { get; set; }
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
    [SwaggerOperation(
        Summary = "Calculates a distance",
        Description = "Calculates a distance between given geographical points",
        OperationId = "GeoDistance.Calculate",
        Tags = new [] { "GeoDistance" }
    )]
    public override async Task<double> HandleAsync([FromBody] CalculateGeoDistanceRequest request, CancellationToken cancellationToken = new CancellationToken())
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