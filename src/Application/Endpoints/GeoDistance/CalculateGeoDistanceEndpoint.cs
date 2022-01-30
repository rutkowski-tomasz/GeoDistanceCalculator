﻿using Application.Common.Interfaces;
using Ardalis.ApiEndpoints;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace Application.Endpoints.GeoDistance;

public class CalculateGeoDistanceRequest
{
    public double InitialLatitude { get; set; }
    public double InitialLongitude { get; set; }
    public double TargetLatitude { get; set; }
    public double TargetLongitude { get; set; }
    public DistanceUnit Unit { get; set; }
    public GeoDistanceCalculationMethod Method { get; set; }
}

public class CalculateGeoDistanceResponse
{
    public double Value { get; set; }
    public DistanceUnit Unit { get; set; }
    public GeoDistanceCalculationMethod Method { get; set; }
}

public class CalculateGeoDistanceEndpointProcessor : IOperationProcessor
{
    public bool Process(OperationProcessorContext context)
    {
        context.OperationDescription.Operation.Summary = "Calculates the distance";
        context.OperationDescription.Operation.Description = "Calculates a distance between given geographical points";
        return true;
    }
}

public class CalculateGeoDistanceEndpoint : EndpointBaseAsync
    .WithRequest<CalculateGeoDistanceRequest>
    .WithResult<CalculateGeoDistanceResponse>
{
    private readonly IGeoDistanceCalculationStrategy _distanceCalculationStrategy;
    private readonly IDistanceConversionService _distanceConversionService;

    public CalculateGeoDistanceEndpoint(
        IGeoDistanceCalculationStrategy distanceCalculationStrategy,
        IDistanceConversionService distanceConversionService
    )
    {
        _distanceCalculationStrategy = distanceCalculationStrategy;
        _distanceConversionService = distanceConversionService;
    }

    [HttpPost("geo-distance/calculate")]
    [OpenApiTag("GeoDistance")]
    [OpenApiOperationProcessor(typeof(CalculateGeoDistanceEndpointProcessor))]
    public override async Task<CalculateGeoDistanceResponse> HandleAsync([FromBody] CalculateGeoDistanceRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        var initialLocation = new GeoLocation
        {
            Latitude = Latitude.From(request.InitialLatitude),
            Longitude = Longitude.From(request.InitialLongitude)
        };
        
        var targetLocation = new GeoLocation
        {
            Latitude = Latitude.From(request.TargetLatitude),
            Longitude = Longitude.From(request.TargetLongitude)
        };
        
        var distance = await _distanceCalculationStrategy.CalculateDistanceAsync(initialLocation, targetLocation, request.Method);
        
        var convertedDistance = _distanceConversionService.Convert(distance, request.Unit);
        
        var response = new CalculateGeoDistanceResponse
        {
            Unit = convertedDistance.Unit,
            Value = convertedDistance.Value,
            Method = request.Method,
        };
        
        return response;
    }
}