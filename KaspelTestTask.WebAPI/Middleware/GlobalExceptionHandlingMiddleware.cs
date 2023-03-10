using KaspelTestTask.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace KaspelTestTask.WebAPI.Middleware;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    public GlobalExceptionHandlingMiddleware()
    {
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await WriteProblemDetailsAsync(ex, context);
        }
    }

    private async Task WriteProblemDetailsAsync(Exception exception, HttpContext context)
    {
        string title;
        int status;
        string type;
        string traceId = Activity.Current?.Id ?? context.TraceIdentifier;

        switch (exception)
        {
            case BookNotFoundException:
            case InvalidQuantityException:
                title = "Unprocessable Entity";
                status = (int)HttpStatusCode.UnprocessableEntity;
                type = "https://tools.ietf.org/html/rfc4918#section-11.2";
                break;
            default:
                title = "Internal Server Error";
                status = (int)HttpStatusCode.InternalServerError;
                type = "http://tools.ietf.org/html/rfc7231#section-6.6.1";
                break;
        }

        var problemDetails = new ProblemDetails()
        {
            Title = title,
            Status = status,
            Type = type,
            Detail = exception.Message,
        };

        problemDetails.Extensions["traceId"] = traceId;

        string json = JsonSerializer.Serialize(problemDetails);

        context!.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsync(json);
    }
}
