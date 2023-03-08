using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;

namespace KaspelTestTask.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        return services;
    }
}