using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application;

public static class DependancyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependancyInjection).Assembly);
        });
        return service;
    }
}