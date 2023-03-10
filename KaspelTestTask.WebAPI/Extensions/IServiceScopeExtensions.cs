using KaspelTestTask.Infrastructure;

namespace KaspelTestTask.WebAPI.Extensions;

public static class IServiceScopeExtensions
{
    /// <summary>
    /// This extension method applies migrations and, if necessary, fills Database with initial data.
    /// </summary>
    /// <param name="scope"></param>
    /// <returns></returns>
    public static async Task PrepareDatabase(this IServiceScope scope)
    {
        var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
        await initializer.ApplyMigrationsAsync();
        await initializer.SeedAsync();
    }
}
