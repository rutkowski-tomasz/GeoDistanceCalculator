using Application.Common.Interfaces;
using Ardalis.ApiEndpoints;
using Domain.Entities;
using Domain.Enums;
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
    public DistanceUnit Unit { get; set; }
}

public class CalculateGeoDistanceResponse
{
    public double Value { get; set; }
    public DistanceUnit Unit { get; set; }
}

public class CalculateGeoDistanceEndpoint : EndpointBaseAsync
    .WithRequest<CalculateGeoDistanceRequest>
    .WithResult<CalculateGeoDistanceResponse>
{
    private readonly IGeoDistanceService _geoDistanceService;
    private readonly IDistanceConversionService _distanceConversionService;

    public CalculateGeoDistanceEndpoint(
        IGeoDistanceService geoDistanceService,
        IDistanceConversionService distanceConversionService
    )
    {
        _geoDistanceService = geoDistanceService;
        _distanceConversionService = distanceConversionService;
    }

    [HttpPost("geo-distance/calculate")]
    [SwaggerOperation(
        Summary = "Calculates a distance",
        Description = "Calculates a distance between given geographical points",
        OperationId = "GeoDistance.Calculate",
        Tags = new [] { "GeoDistance" }
    )]
    public override async Task<CalculateGeoDistanceResponse> HandleAsync([FromBody] CalculateGeoDistanceRequest request, CancellationToken cancellationToken = new CancellationToken())
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

        var convertedDistance = _distanceConversionService.Convert(distance, request.Unit);

        var response = new CalculateGeoDistanceResponse
        {
            Unit = convertedDistance.Unit,
            Value = convertedDistance.Value
        };
        
        return response;
    }
}