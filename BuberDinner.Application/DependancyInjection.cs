using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Authentication.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application;

public static class DependancyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
        service.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        return service;
    }
}