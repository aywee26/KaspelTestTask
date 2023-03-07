using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace KaspelTestTask.Infrastructure;

internal class AppDbContextCreator<T>
    where T : DbContext
{
    private readonly Action<SqlServerDbContextOptionsBuilder> _sqlServerOptionsAction;
    private readonly Func<DbContextOptions<T>, T> _contextFactory;


    public AppDbContextCreator(Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction,
                               Func<DbContextOptions<T>, T> contextFactory)
    {
        _sqlServerOptionsAction = Guard.Against.Null(sqlServerOptionsAction, nameof(sqlServerOptionsAction));
        _contextFactory = Guard.Against.Null(contextFactory, nameof(contextFactory));
    }

    public T CreateDbContext(string[] args)
    {
        string? environmentName = Environment.GetEnvironmentVariable("Hosting:Environment");
        string basePath = AppContext.BaseDirectory;

        var instance = CreateUsingConfigurationVariables(basePath, environmentName);
        return instance;
    }


    private T CreateUsingConfigurationVariables(string basePath, string? environmentName)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environmentName}.json", true);
        var config = builder.Build();

        string connectionStringName = typeof(T).Name;
        string connectionStringValue =
            config.GetConnectionString(connectionStringName)
            ?? throw new InvalidOperationException($"The {connectionStringName} connection string is empty.");

        var optionsBuilder = new DbContextOptionsBuilder<T>()
            .UseSqlServer(connectionStringValue, _sqlServerOptionsAction);
        var options = optionsBuilder.Options;

        var instance = _contextFactory(options);
        return instance;
    }
}
