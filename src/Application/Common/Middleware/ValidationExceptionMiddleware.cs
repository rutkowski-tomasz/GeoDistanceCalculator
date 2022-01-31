using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.Common.Middleware;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            var errorMessage = string.Join(", ", ex.Errors.Select(x => x.ErrorMessage));
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = errorMessage,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            };
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(details));
        }
    }
}