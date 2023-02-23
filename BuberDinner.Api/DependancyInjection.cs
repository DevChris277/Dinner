using BuberDinner.Api.Common.Errors;
using BuberDinner.Api.Common.Mapping.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BuberDinner.Api;

public static class DependancyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();
        services.AddMappings();
        return services;
    }
}