using Ardalis.GuardClauses;
using KaspelTestTask.Application.Repositories.Abstractions;
using KaspelTestTask.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KaspelTestTask.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        Guard.Against.Null(services, nameof(services));

        services.AddScoped(
            provider =>
            {
                var configurationString = configuration.GetConnectionString(nameof(AppDbContext));
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(configurationString);
                var options = optionsBuilder.Options;
                return new AppDbContext(options);
            });

        services.AddScoped<AppDbContextInitializer>();

        services.AddScoped<IBooksRepository, BooksRepository>();
        services.AddScoped<IOrdersRepository, OrdersRepository>();

        return services;
    }
}
