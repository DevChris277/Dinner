using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace BuberDinner.Api.Common.Errors;

public class CustomProblemDetailsFactory : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options;

    // Constructor to create a new instance of _options to use the ApiBehaviorOptions
    public CustomProblemDetailsFactory(IOptions<ApiBehaviorOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    // Override the CreateProblemDetails method
    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        statusCode ??= 500;

        // Create a new instance of problemDetail and set the neccessary data
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        // Apply the defaults
        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    // Override the CreateValidationProblemDetails Method
    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        if (modelStateDictionary == null)
        {
            throw new ArgumentNullException(nameof(modelStateDictionary));
        }

        statusCode ??= 400;

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        // If title is not null, dont overwrite the default title with null
        if (title != null)
        {
            problemDetails.Title = title;
        }

        // Apply the defaults
        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    // Private method to apply the defaults
    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        // Set the Status code
        problemDetails.Status ??= statusCode;

        // Get client error data from the repository
        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            // Set the title if not provided
            problemDetails.Title ??= clientErrorData.Title;

            // Set the type if not provided
            problemDetails.Type ??= clientErrorData.Link;
        }

        // Get the traceId 
        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId != null)
        {
            // Add the traceId to the problem details 
            problemDetails.Extensions["traceId"] = traceId;
        }

        // Add a custom property to the problemDetails object
        problemDetails.Extensions.Add("customProperty", "customValue");
    }
}
