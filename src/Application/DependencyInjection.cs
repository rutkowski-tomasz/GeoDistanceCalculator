using Application.Common.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Application;

public static class DependencyInjection
{
    public static IApplicationBuilder UseApplication(this IApplicationBuilder app)
    {
        app.UseMiddleware<UnhandledExceptionMiddleware>();

        return app;
    }
}