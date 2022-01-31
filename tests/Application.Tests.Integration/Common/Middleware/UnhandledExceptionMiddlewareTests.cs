using System;
using System.Net;
using System.Threading.Tasks;
using Application.Common.Middleware;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Xunit;

namespace Application.Tests.Integration.Common.Middleware;

public class UnhandledExceptionMiddlewareTests
{
    [Fact]
    public async Task ShouldReturnInternalServerErrorForUnhandedException()
    {
        using var host = await CreateHost(app =>
        {
            app.Run(_ => throw new Exception("Unhandled exception"));
        });
        
        var response = await host.GetTestClient().GetAsync("/");
        var content = await response.Content.ReadAsStringAsync();
        var deserialized = JsonConvert.DeserializeObject<ProblemDetails>(content);

        deserialized.Status.Should().Be(StatusCodes.Status500InternalServerError);
        deserialized.Title.Should().Be("An error occurred while processing your request.");
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
    
    [Fact]
    public async Task ShouldReturnUnmodifiedContentWhenNoExceptionsOccur()
    {
        using var host = await CreateHost(app =>
        {
            app.Run(x => x.Response.WriteAsync("Hello World!"));
        });
        
        var response = await host.GetTestClient().GetAsync("/");
        var content = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().Be("Hello World!");
    }

    private static async Task<IHost> CreateHost(Action<IApplicationBuilder> handleRequest)
    {
        return await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .Configure(app =>
                    {
                        app.UseApplication();
                        handleRequest(app);
                    });
            })
            .StartAsync();
    }
}