using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace Application.Tests.Integration.Common.Middleware;

public class ValidationExceptionMiddlewareTests
{
    [Fact]
    public async Task ShouldReturnBadRequestErrorWhenValidationExceptionWasThrown()
    {
        using var host = await TestHostFactory.CreateHost(app =>
        {
            app.Run(_ => throw new ValidationException(
                "Error message",
                new List<ValidationFailure>
                {
                    new ("PropertyName", "Property name can not be null", null)
                })
            );
        });
        
        var response = await host.GetTestClient().GetAsync("/");
        var content = await response.Content.ReadAsStringAsync();
        var deserialized = JsonConvert.DeserializeObject<ProblemDetails>(content);
    
        deserialized.Status.Should().Be(StatusCodes.Status400BadRequest);
        deserialized.Title.Should().Be("Property name can not be null");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}