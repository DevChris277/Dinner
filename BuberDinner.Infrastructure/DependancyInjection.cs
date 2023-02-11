using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Common.Interfaces.Services;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Persistence;
using BuberDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Infrastructure;

public static class DependancyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        
        service.AddSingleton<IJwtTokenGenerator, jwtTokenGenerator>();
        service.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        service.AddScoped<IUserRepository, UserRepository>();
        return service;
    }
}