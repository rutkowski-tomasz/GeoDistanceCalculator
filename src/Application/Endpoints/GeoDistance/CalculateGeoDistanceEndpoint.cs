using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Application.Endpoints.GeoDistance;

public class CalculateGeoDistanceEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithResult<double>
{
    [HttpGet("calculate")]
    public override async Task<double> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return await Task.FromResult(0d);
    }
}