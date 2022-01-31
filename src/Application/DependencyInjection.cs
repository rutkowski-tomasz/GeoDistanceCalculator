using System.Reflection;
using Application.Common.Middleware;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
    
    public static IApplicationBuilder UseApplication(this IApplicationBuilder app)
    {
        app.UseMiddleware<UnhandledExceptionMiddleware>();
        app.UseMiddleware<ValidationExceptionMiddleware>();

        return app;
    }
}