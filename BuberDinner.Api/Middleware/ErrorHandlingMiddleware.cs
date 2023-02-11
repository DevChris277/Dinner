using System.Net;
using System.Text.Json;

namespace BuberDinner.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    // The constructor takes in a RequestDelegate which contains 
    // a delegate to the next request handler for processing  
    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // Invoke is the main entry point for processing
    // errors encountered during the HTTP request
    public async Task Invoke(HttpContext context)
    {
        try
        {
            // call the next handler
            await _next(context);
        }
        catch (Exception ex)
        {
            // handle error according to exception type
            await HandleExceptionAsync(context, ex);
        }
    }

    // HandleExceptionAsync is responsible for setting the
    // response status code and writing a message to the response body. 
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError; // 500 if unexpected

        // set the response code according to exception type
        // if (exception is NotFoundException) code = HttpStatusCode.NotFound;
        // else if (exception is UnauthorizedException) code = HttpStatusCode.Unauthorized;
        // else if (exception is ForbiddenException) code = HttpStatusCode.Forbidden;
        // else if (exception is BadRequestException) code = HttpStatusCode.BadRequest;
        // else if (exception is ConflictException) code = HttpStatusCode.Conflict;

        // serialize an object with an error message
        var result = JsonSerializer.Serialize(new { error = exception.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}