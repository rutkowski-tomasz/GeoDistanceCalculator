using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace Application.Tests.Integration.Common.Middleware;

public class TestHostFactory
{
    public static async Task<IHost> CreateHost(Action<IApplicationBuilder> handleRequest)
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